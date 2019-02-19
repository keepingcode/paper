using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Paper.Api.Commons;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Data;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;
using Toolset.Net;
using Toolset.Reflection;
using static Toolset.Ret;

namespace Paper.Api.Extensions.Papers
{
  [Expose]
  public class PaperPipeline : IPipeline
  {
    private readonly IPaperCatalog paperCatalog;
    private readonly IObjectFactory objectFactory;

    public string Route { get; } = "";

    public PaperPipeline(IObjectFactory objectFactory, IPaperCatalog paperCatalog)
    {
      this.objectFactory = objectFactory;
      this.paperCatalog = paperCatalog;
    }

    public async Task RenderAsync(Request request, Response response, NextAsync next)
    {
      var path = request.Path.Substring(Route.Length);
      var paper = paperCatalog.FindExact(path).FirstOrDefault();
      if (paper != null)
      {
        await RenderPaperAsync(path, paper, request, response, next);
      }
      else
      {
        await next.Invoke();
      }
    }

    private async Task RenderPaperAsync(string path, PaperDescriptor paper, Request req, Response res, NextAsync next)
    {
      var pathArgs = ParseArgValues(path, paper, req, res).ToArray();

      Ret<Result> ret = CallPaperMethod(path, paper, req, res, pathArgs);

      var isFailOrRedirect = (!ret.Ok || (ret.Status.CodeClass != HttpStatusClass.Redirection));
      if (isFailOrRedirect)
      {
        res.Status = ret.Status.Code;
        res.Headers[HeaderNames.Location] = ret.Status.Data[HeaderNames.Location];
        var entity = HttpEntity.CreateFromRet(req.RequestUri, ret);
        await res.WriteEntityAsync(entity);
        return;
      }

      Result result = ret.Value;

      var noResult = (result.ValueType == typeof(void));
      if (noResult)
      {
        res.Status = HttpStatusCode.Redirect;
        res.Headers[HeaderNames.Location] = req.RequestUri;
        var entity = HttpEntity.CreateFromRet(req.RequestUri, ret);
        await res.WriteEntityAsync(entity);
        return;
      }

      var isUri = typeof(string).IsAssignableFrom(result.ValueType)
               || typeof(Uri).IsAssignableFrom(result.ValueType)
               || typeof(Href).IsAssignableFrom(result.ValueType)
               || typeof(UriString).IsAssignableFrom(result.ValueType);
      if (isUri)
      {
        var pathBase = new UriString(req.PathBase, Route);

        var href = result.Value as Href ?? result.Value?.ToString();
        href.ExpandUri(req.RequestUri, pathBase);

        res.Status = HttpStatusCode.Found;
        res.Headers[HeaderNames.Location] = result.ToString();

        var entity = HttpEntity.CreateFromRet(req.RequestUri, ret);
        await res.WriteEntityAsync(entity);
        return;
      }

      {
        var entity = RenderEntity(path, paper, req, res, result, pathArgs);
        res.Status = ret.Status.Code;
        res.Headers[HeaderNames.Location] = ret.Status.Data[HeaderNames.Location];
        await res.WriteEntityAsync(entity);
      }
    }

    private Ret<Result> CallPaperMethod(string path, PaperDescriptor paper, Request req, Response res, object[] pathArgs)
    {
      object result = null;
      try
      {
        // TODO escolher entre Index e outros metodos
        var method = paper.IndexMethod;

        result = objectFactory.Invoke(paper.Paper, method, pathArgs);
      }
      catch (Exception ex)
      {
        result = Ret.Fail(ex);
      }

      var resultType = paper.IndexMethod.ReturnType;
      if (Is.Ret(resultType))
      {
        resultType = TypeOf.Ret(resultType);
      }

      Ret<Result> ret;
      if (result == null)
      {
        ret = Ret.Create(HttpStatusCode.NotFound);
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

    private IEnumerable<object> ParseArgValues(string path, PaperDescriptor paper, Request req, Response res)
    {
      var pathArgValues = new PathArgs(path, paper.PathTemplate);
      foreach (var arg in paper.PathArgs)
      {
        object argValue;
        var type = arg.ParameterType;
        if (typeof(Sort).IsAssignableFrom(type))
        {
          argValue = null;
        }
        else if (typeof(Page).IsAssignableFrom(type))
        {
          var page = new Page();
          page.CopyFrom(req.RequestUri);
          argValue = page;
        }
        else if (typeof(IFilter).IsAssignableFrom(type))
        {
          argValue = null;
        }
        else
        {
          object rawValue = pathArgValues[arg.Name];
          if (rawValue is Var var)
          {
            rawValue = var.RawValue;
          }
          argValue = Change.To(rawValue, type);
        }
        yield return argValue;
      }
    }

    private Entity RenderEntity(string path, PaperDescriptor paper, Request req, Response res, Result result, object[] pathArgs)
    {
      var entity = new Entity();

      var isList = typeof(IEnumerable).IsAssignableFrom(result.ValueType);
      if (isList)
      {
        var list = (IEnumerable)result.Value;
        entity.AddEntities(list, (item, e) =>
        {
          e.AddClass(ClassNames.Record);
          e.AddClass(item.GetType());
          e.AddProperties(item);
          Format(path, paper, req, res, e, pathArgs, item);
          Format(path, paper, req, res, e, new object[0], item);
        });
      }
      else
      {
        entity.AddClass(ClassNames.Record);
        entity.AddClass(result.ValueType);
        entity.AddProperties(result.Value);
        Format(path, paper, req, res, entity, pathArgs, result);
        Format(path, paper, req, res, entity, new object[0], result);
      }

      Format(path, paper, req, res, entity, pathArgs);
      Format(path, paper, req, res, entity, new object[0]);

      return entity;
    }

    private void Format(string path, PaperDescriptor paper, Request req, Response res, Entity entity, object[] pathArgs, object graph = null)
    {
      if (paper.Formatters == null)
        return;

      var argCount = pathArgs.Length;
      if (graph != null)
      {
        argCount++;
      }

      var args = pathArgs.Append(graph).ToArray();

      var type = graph?.GetType();
      var isArg = (graph != null);
      var isList = isArg && typeof(IEnumerable).IsAssignableFrom(graph.GetType());
      var isGraph = isArg && !isList;

      var formatters =
        from f in paper.Formatters
        where f.GetParameters().Length == argCount
        let lastType = f.GetParameters().LastOrDefault()?.ParameterType
        where !isArg
           || (isList && lastType.IsAssignableFrom(type) && typeof(IEnumerable).IsAssignableFrom(lastType))
           || (isGraph && lastType.IsAssignableFrom(type) && !typeof(IEnumerable).IsAssignableFrom(lastType))
        select f;

      foreach (var method in formatters)
      {
        IEnumerable callers;

        var terms = objectFactory.Invoke(paper.Paper, method, args);
        if (terms == null)
          continue;

        if (terms is Format || terms is PaperLink)
        {
          callers = new[] { terms };
        }
        else if (terms is ICollection collection)
        {
          callers = collection.Cast<object>();
        }
        else if (terms is IList list)
        {
          callers = list.Cast<object>();
        }
        else
        {
          callers = (IEnumerable)terms;
        }

        foreach (var caller in callers)
        {
          objectFactory.Invoke((Delegate)caller, objectFactory, entity);
        }
      }
    }

    private struct Result
    {
      public Type ValueType { get; set; }
      public object Value { get; set; }
    }
  }
}