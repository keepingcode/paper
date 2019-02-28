using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Collections;
using Toolset.Net;
using Toolset.Reflection;

namespace Paper.Api.Extensions.Papers
{
  internal class PaperRenderer
  {
    private struct Caller
    {
      public MethodInfo Method { get; set; }
      public object[] Args { get; set; }
    }

    private readonly IObjectFactory objectFactory;
    private readonly IPaperCatalog paperCatalog;

    public PaperRenderer(IObjectFactory objectFactory, IPaperCatalog paperCatalog)
    {
      this.objectFactory = objectFactory;
      this.paperCatalog = paperCatalog;
    }

    public async Task<Ret<Entity>> RenderAsync(PaperContext context, Result result)
    {
      var args = context.Args.Values.ToArray();

      var entity = new Entity();

      var value = result.Value;
      var valueType = result.ValueType;

      var isList = typeof(IEnumerable).IsAssignableFrom(valueType);
      if (isList)
      {
        var list = (IEnumerable)value;

        entity.SetTitle(Conventions.MakeTitle(context.Paper.PaperType));
        entity.AddEntities(list, (item, e) => RenderRecord(context, e, args, item));

        FormatEntity(context, entity, args, value);
      }
      else
      {
        RenderRecord(context, entity, args, value);
      }

      FormatEntity(context, entity, args);

      return await Task.FromResult(entity);
    }

    private void RenderRecord(PaperContext context, Entity entity, object[] args, object record)
    {
      entity.AddClass(ClassNames.Record);
      entity.AddClass(record.GetType());
      entity.SetTitle(Conventions.MakeTitle(record.GetType()));
      entity.AddProperties(record);
      entity.AddHeaders(record, ClassNames.Record);

      FormatEntity(context, entity, args, record);
    }

    private void FormatEntity(PaperContext context, Entity entity, object[] args, object graph = null)
    {
      RunFormatters(context, entity, args, graph);
      RunActionBuilders(context, entity, args, graph);
      if (args.Length > 0)
      {
        RunFormatters(context, entity, new object[0], graph);
        RunActionBuilders(context, entity, new object[0], graph);
      }
    }

    private void RunFormatters(PaperContext context, Entity entity, object[] args, object graph = null)
    {
      var path = context.Path;
      var paper = context.Paper;
      var req = context.Request;
      var res = context.Response;

      var allArgs = args.Append(graph).NonNull().ToArray();

      var callers = MatchCallers(paper.Formatters, allArgs);
      foreach (var caller in callers)
      {
        var result = objectFactory.Invoke(paper.Paper, caller.Method, caller.Args);
        if (result == null)
          continue;

        IEnumerable items;

        if (result is IEnumerable enumerable)
        {
          items = enumerable;
        }
        else if (result is ICollection collection)
        {
          items = collection;
        }
        else
        {
          items = new[] { result };
        }

        foreach (var item in items)
        {
          if (item is IFormatter formatter)
          {
            formatter.Format(context, objectFactory, entity);
          }
          else if (item is Format format)
          {
            format.Invoke(context, objectFactory, entity);
          }

          if (item is Link link)
          {
            entity.AddLink(link);
          }
        }
      }
    }

    private void RunActionBuilders(PaperContext context, Entity entity, object[] args, object graph = null)
    {
      var path = context.Path;
      var paper = context.Paper;
      var req = context.Request;
      var res = context.Response;

      var allArgs = args.Append(graph).NonNull().ToArray();

      var callers = MatchCallers(paper.Actions, allArgs, ignore: new[] { typeof(IForm) });
      foreach (var caller in callers)
      {
        RenderForm(caller, context, entity, args, graph);
      }
    }

    private void RenderForm(Caller caller, PaperContext context, Entity entity, object[] args, object graph = null)
    {
      var href = new UriString(context.Path.Substring(1)).Append($":{caller.Method.Name}");

      var action = new EntityAction();
      action.Name = caller.Method.Name;
      action.Title = caller.Method.Name.ChangeCase(TextCase.ProperCase);
      action.Href = href;
      action.Method = MethodNames.Post;

      var parameters = caller.Method.GetParameters();
      for (var i = 0; i < parameters.Length; i++)
      {
        var parameter = parameters[i];
        var parameterValue = caller.Args[i];

        var name = Conventions.MakeName(parameter.Name);

        var isValue = IsValue(parameter.ParameterType);
        var isArray = !isValue && typeof(IEnumerable).IsAssignableFrom(parameter.ParameterType);
        var isForm = !isValue && !isArray && typeof(IForm).IsAssignableFrom(parameter.ParameterType);

        if (isValue)
        {
          action.AddField($"Form.{name}", opt => opt
            .SetTitle(name.ChangeCase(TextCase.ProperCase))
            .SetDataType(parameter.ParameterType)
            .SetHidden(true)
            .SetValue(parameterValue)
          );
        }
        else if (isArray)
        {
          var elementType = TypeOf.CollectionElement(parameter.ParameterType);
          var properties = elementType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
          var keys = (
            from property in properties
            select Conventions.MakeName(property.Name)
          ).ToArray();

          action.AddField("__records__", opt => opt
            .SetTitle("Registros Afetados")
            .SetPlaceholder("Selecione os registros afetados")
            .SetDataType(DataTypeNames.ArrayOfRecords)
            .SetProvider(provider => provider
              .AddRel(RelNames.Self)
              .SetKeys(keys)
            )
            .SetAllowMany()
            .SetRequired()
          );
        }
        else if (isForm)
        {
          var properties = parameter.ParameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
          foreach (var property in properties)
          {
            var subname = Conventions.MakeName(property.Name);
            action.AddField($"Form.{name}.{subname}", opt => opt
              .SetTitle(subname.ChangeCase(TextCase.ProperCase))
              .SetDataType(property.PropertyType)
            );
          }
        }
        else
        {
          foreach (var propertyName in parameterValue._GetPropertyNames())
          {
            var property = parameterValue._GetPropertyInfo(propertyName);
            var propertyValue = property.GetValue(parameterValue);

            var subname = Conventions.MakeName(propertyName);
            action.AddField($"Data.{subname}", opt => opt
              .SetTitle(subname.ChangeCase(TextCase.ProperCase))
              .SetDataType(property.PropertyType)
              .SetHidden(true)
              .SetValue(propertyValue)
            );
          }
        }
      }

      entity.AddAction(action);
    }

    private bool IsValue(Type type)
    {
      return type.IsValueType
          || type == typeof(string)
          || type == typeof(Href)
          || type == typeof(Uri)
          || type == typeof(UriString);
    }

    private ICollection<Caller> MatchCallers(ICollection<MethodInfo> methods, object[] args, Type[] ignore = null)
    {
      var callers = (
        from method in methods
        let caller = MatchCaller(method, args, ignore)
        where caller != null
        select caller.Value
      ).ToArray();
      return callers;
    }

    private Caller? MatchCaller(MethodInfo method, object[] args, Type[] ignore)
    {
      var parameters = method.GetParameters();

      var parameterCount = (ignore != null)
          ? parameters.Count(x => !IsIgnored(x.ParameterType, ignore))
          : parameters.Length;

      if (parameterCount != args.Length)
        return null;

      var values = new object[parameters.Length];
      var cache = args.ToArray();
      for (int i = 0; i < parameters.Length; i++)
      {
        var parameter = parameters[i];
        var type = parameter.ParameterType;

        if (IsIgnored(type, ignore))
        {
          values[i] = null;
          continue;
        }

        int? argIndex;

        argIndex = cache
          .Select((arg, index) => new { arg, index })
          .Where(x => x.arg != null)
          .Where(x => x.arg.GetType() == type)
          .FirstOrDefault()?.index;

        if (argIndex == null)
        {
          argIndex = cache
            .Select((arg, index) => new { arg, index })
            .Where(x => x.arg != null)
            .Where(x => IsTypeMatch(x.arg.GetType(), type))
            .FirstOrDefault()?.index;
        }

        if (argIndex == null)
          return null;

        values[i] = cache[argIndex.Value];
        cache[argIndex.Value] = null;
      }
      return new Caller { Method = method, Args = values };
    }

    private bool IsIgnored(Type type, Type[] ignore)
    {
      return ignore?.Any(x => x.IsAssignableFrom(type)) == true;
    }

    private bool IsTypeMatch(Type source, Type target)
    {
      if (source == null || target == null)
        return false;

      var sourceIsList = typeof(IEnumerable).IsAssignableFrom(source) || typeof(ICollection).IsAssignableFrom(source);
      var targetIsList = typeof(IEnumerable).IsAssignableFrom(target) || typeof(ICollection).IsAssignableFrom(target);
      if (sourceIsList != targetIsList)
        return false;

      return target.IsAssignableFrom(source);
    }
  }
}