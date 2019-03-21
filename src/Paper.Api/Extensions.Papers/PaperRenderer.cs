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
using Paper.Media.Data;
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

    public async Task<Ret<Entity>> RenderAsync(PaperContext context, Ret<Result> result)
    {
      var value = result.Value.Value;

      if (value == null)
      {
        var status = HttpEntity.CreateFromRet(context.Request.RequestUri, result);
        return await Task.FromResult(status);
      }

      var entity = new Entity();
      var args = context.Args.Values.ToArray();

      var valueType = result.Value.ValueType;
      var isList = typeof(IEnumerable).IsAssignableFrom(valueType);
      if (isList)
      {
        var list = ((IEnumerable)value).Cast<object>();

        if (context.RenderContext.Page is Page page)
        {
          context.RenderContext.HasMorePages = (list.Count() >= page.Limit);
          page.DecreaseLimit();

          if (context.RenderContext.HasMorePages)
          {
            list = list.SkipLast(1);
          }
        }

        entity.SetTitle(Conventions.MakeTitle(context.Paper.PaperType));
        entity.AddEntities(list, (item, e) =>
        {
          RenderEntity(context, e, args, item);
          FormatEntity(context, e, args, item);
        });

        FormatEntity(context, entity, args, value);
      }
      else
      {
        RenderEntity(context, entity, args, value);
        FormatEntity(context, entity, args, value);
      }

      FormatEntity(context, entity, args);
      LinkEntity(context, entity);

      return await Task.FromResult(entity);
    }

    private void RenderEntity(PaperContext context, Entity entity, object[] args, object graph)
    {
      entity.AddClass(ClassNames.Record);
      entity.AddClass(graph.GetType());
      entity.SetTitle(Conventions.MakeTitle(graph.GetType()));
      entity.AddProperties(graph);
      entity.AddHeaders(graph, ClassNames.Record);
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

    private void LinkEntity(PaperContext context, Entity entity)
    {
      var uri = new UriString(context.Request.RequestUri);

      if (context.RenderContext.Filter is IFilter filter)
      {
        uri = filter.CreateUri(uri);
      }

      if (context.RenderContext.Sort is Sort sort)
      {
        uri = sort.CreateUri(uri);

        var headers = entity.Headers().OfType<HeaderDesign>();
        foreach (var fieldName in sort.FieldNames)
        {
          var matches = headers.Where(x => x.Name.EqualsIgnoreCase(fieldName));
          foreach (var header in matches)
          {
            header.Order = sort.GetSortOrder(fieldName);

            var order = header.Order == SortOrder.Ascending ? ":desc" : "";
            var href = uri.SetArg("sort", $"{header.Name}{order}");
            header.HeaderEntity.AddLink(href, opt => opt.AddRel(RelNames.Sort));
          }
        }
      }

      if (context.RenderContext.Page is Page page)
      {
        uri = page.CreateUri(uri);

        if (page.Offset > 0)
        {
          var href = page.GetFirstPage().CreateUri(uri);
          entity.AddLink(href, opt => opt.AddRel(RelNames.First));
        }

        if (page.Offset > page.Limit)
        {
          var href = page.GetPreviousPage().CreateUri(uri);
          entity.AddLink(href, opt => opt.AddRel(RelNames.Prev));
        }

        if (context.RenderContext.HasMorePages)
        {
          var href = page.GetNextPage().CreateUri(uri);
          entity.AddLink(href, opt => opt.AddRel(RelNames.Next));
        }
      }

      entity.SetSelfLink(uri);
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
      var href = new UriString(context.Path.Substring(1)).Append($"-{caller.Method.Name}");

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
          action.AddField($"Form.{name}", opt =>
          {
            opt.SetDefaults(parameter);
            opt.SetHidden(true);
            if (parameterValue != null)
            {
              opt.SetValue(parameterValue);
            }
          });
        }
        else if (isForm)
        {
          var properties = parameter.ParameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
          foreach (var property in properties)
          {
            var fieldName = Conventions.MakeName(property.Name);
            var fieldValue = (parameterValue != null) ? property.GetValue(parameterValue) : null;
            action.AddField($"Form.{name}.{fieldName}", opt =>
            {
              opt.SetDefaults(property);
              if (fieldValue != null)
              {
                opt.SetValue(fieldValue);
              }
            });
          }
        }
        else if (isArray)
        {
          var elementType = TypeOf.CollectionElement(parameter.ParameterType);
          var properties = elementType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
          var keys = (
            from property in properties
            select Conventions.MakeName(property.Name)
          ).ToArray();

          action.AddField("Records", opt => opt
            .SetTitle("Registros Afetados")
            .SetPlaceholder("Selecione os registros afetados")
            .SetType(FieldTypeNames.SelectRecord)
            .SetDataType(DataTypeNames.Record)
            .SetProvider(provider => provider
              .AddRel(RelNames.Self)
              .SetKeys(keys)
            )
            .SetAllowMany()
            .SetRequired()
          );
        }
        else
        {
          foreach (var propertyName in parameterValue._GetPropertyNames())
          {
            var property = parameterValue._GetPropertyInfo(propertyName);
            var fieldName = Conventions.MakeName(propertyName);
            var fieldValue = parameterValue._Get(propertyName);
            action.AddField($"Record.{fieldName}", opt =>
            {
              opt.SetDefaults(property);
              opt.SetHidden(true);
              if (fieldValue != null)
              {
                opt.SetValue(fieldValue);
              }
            });
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