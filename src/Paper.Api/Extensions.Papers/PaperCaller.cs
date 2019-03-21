using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Data;
using Toolset;
using Toolset.Collections;
using Toolset.Net;
using Toolset.Reflection;
using static Toolset.Ret;

namespace Paper.Api.Extensions.Papers
{
  internal class PaperCaller
  {
    private readonly IObjectFactory objectFactory;

    public PaperCaller(IObjectFactory objectFactory)
    {
      this.objectFactory = objectFactory;
    }

    public async Task<Ret<Result>> CallAsync(PaperContext context)
    {
      var descriptor = context.Paper;
      var req = context.Request;
      var res = context.Response;

      Entity form = null;
      if (!req.Method.EqualsAnyIgnoreCase(MethodNames.Get, MethodNames.Delete))
      {
        form = req.ReadEntityAsync().RunSync();

        // TODO: Uma entidade diferente de "form" não está sendo suportada, mas poderia,
        // se houvesse um algoritmo de conversão.
        var isValid = form.Class.Has(ClassNames.Form);
        if (!isValid)
        {
          return Ret.Fail(HttpStatusCode.BadRequest, "Formato de dados não suportado. Os dados devem ser enviados em uma entidade do tipo \"form\".");
        }
      }

      MethodInfo method = descriptor.GetMethod(context.Action);
      Ret<Result> ret = CallPaperMethod(context, descriptor.Paper, method, context.Args, form);

      var isFailure = (ret.Status.CodeClass != HttpStatusClass.Success);
      if (isFailure)
      {
        return await Task.FromResult(ret);
      }

      Result result = ret.Value;

      var isUri = typeof(string).IsAssignableFrom(result.ValueType)
               || typeof(Uri).IsAssignableFrom(result.ValueType)
               || typeof(Href).IsAssignableFrom(result.ValueType)
               || typeof(UriString).IsAssignableFrom(result.ValueType);
      if (isUri)
      {
        var href = (result.Value as Href)?.ToString() ?? result.Value?.ToString();
        ret = Ret.Create(HttpStatusCode.Found);
        ret.Status.Headers[HeaderNames.Location] = href;
        return await Task.FromResult(ret);
      }

      return await Task.FromResult(ret);
    }

    private Ret<Result> CallPaperMethod(PaperContext context, IPaper paper, MethodInfo method, Args args, Entity form)
    {
      object result = null;
      try
      {
        var methodArgs = CreateParameters(context, paper, method, args, form);

        context.RenderContext.Sort = methodArgs.OfType<Sort>().FirstOrDefault();
        context.RenderContext.Page = methodArgs.OfType<Page>().FirstOrDefault();
        context.RenderContext.Filter = methodArgs.OfType<IFilter>().FirstOrDefault();

        context.RenderContext.Page?.IncreaseLimit();

        result = objectFactory.Invoke(paper, method, methodArgs);
      }
      catch (Exception ex)
      {
        result = Ret.Fail(ex);
      }

      var resultType = method.ReturnType;
      if (Is.Ret(resultType))
      {
        resultType = TypeOf.Ret(resultType);
      }

      Ret<Result> ret;
      if (result == null)
      {
        // Um método que resulta "void" é considerado OK quando não emite exceção.
        // Um método que resulta nulo é considerado NotFound (Não encontrado).
        var isVoid = method.ReturnType == typeof(void);
        if (isVoid)
        {
          ret = Ret.OK(new Result
          {
            Value = result,
            ValueType = resultType
          });
        }
        else
        {
          ret = Ret.NotFound(new Result
          {
            Value = result,
            ValueType = resultType
          });
        }
      }
      else if (Is.Ret(result))
      {
        ret = new Ret<Result>();
        ret.Status = (RetStatus)result._Get(nameof(ret.Status));
        ret.Fault = (RetFault)result._Get(nameof(ret.Fault));
        ret.Value = new Result
        {
          Value = result._Get(nameof(ret.Value)),
          ValueType = resultType
        };
      }
      else
      {
        ret = Ret.OK(new Result
        {
          Value = result,
          ValueType = resultType
        });
      }

      return ret;
    }

    private object[] CreateParameters(PaperContext context, IPaper paper, MethodInfo method, Args args, Entity form)
    {
      var methodArgs = new List<object>();

      var argKeys = args?.Keys.ToList() ?? new List<string>();
      var formKeys = form?.Properties?.Keys.ToList() ?? new List<string>();
      foreach (var parameter in method.GetParameters())
      {
        var name = parameter.Name;

        if (typeof(Sort).IsAssignableFrom(parameter.ParameterType))
        {
          var sort = (Sort)context.Paper.Create(objectFactory, parameter.ParameterType);
          sort.CopyFrom(args);
          methodArgs.Add(sort);
          continue;
        }

        if (typeof(Page).IsAssignableFrom(parameter.ParameterType))
        {
          var page = (Page)context.Paper.Create(objectFactory, parameter.ParameterType);
          page.CopyFrom(args);
          methodArgs.Add(page);
          continue;
        }

        if (typeof(IFilter).IsAssignableFrom(parameter.ParameterType))
        {
          var filter = CreateCompatibleFilter(context, args, parameter.ParameterType);
          methodArgs.Add(filter);
          continue;
        }

        string key = null;

        key = argKeys.FirstOrDefault(x => x.EqualsIgnoreCase(name));
        if (key != null)
        {
          var value = args[key];
          var compatibleValue = CreateCompatibleValue(context, value, parameter.ParameterType);
          methodArgs.Add(compatibleValue);
          argKeys.Remove(key);
          continue;
        }

        key = formKeys.FirstOrDefault(x => x.EqualsIgnoreCase(name));
        if (key != null)
        {
          var value = form.Properties[key];
          var compatibleValue = CreateCompatibleValue(context, value, parameter.ParameterType);
          methodArgs.Add(compatibleValue);
          formKeys.Remove(key);
          continue;
        }

        if (Is.Collection(parameter.ParameterType))
        {
          var records = form.Children();
          var itemType = TypeOf.CollectionElement(parameter.ParameterType);
          var items = records.Select(record => 
            CreateCompatibleValue(context, record.Properties, itemType)
          ).ToArray();
          var compatibleValue = CreateCompatibleValue(context, items, parameter.ParameterType);
          methodArgs.Add(compatibleValue);
        }
        else
        {
          var record = form?.Children().FirstOrDefault();
          var compatibleValue = CreateCompatibleValue(context, record.Properties, parameter.ParameterType);
          methodArgs.Add(compatibleValue);
        }
      }

      return methodArgs.ToArray();
    }

    private object CreateCompatibleValue(PaperContext context, object sourceValue, Type targetType)
    {
      if (sourceValue is PropertyMap map)
      {
        var instance = objectFactory.CreateObject(targetType);

        foreach (var key in map.Keys)
        {
          var property = targetType.GetProperties().FirstOrDefault(x => x.Name.EqualsIgnoreCase(key));
          if (property == null)
            continue;

          var value = map[key];
          var compatibleValue = CreateCompatibleValue(context, value, property.PropertyType);
          property.SetValue(instance, compatibleValue);
        }

        return instance;
      }
      else
      {
        return Change.To(sourceValue, targetType);
      }
    }

    private IFilter CreateCompatibleFilter(PaperContext context, HashMap<Var> args, Type filterType)
    {
      var filter = (IFilter)context.Paper.Create(objectFactory, filterType);
      foreach (var property in filter._GetPropertyNames())
      {
        var value = args[property];
        if (value != null)
        {
          filter._Set(property, value);
        }
      }
      return filter;
    }
  }
}