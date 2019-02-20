using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;

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
      var entity = RenderEntity(context, result);
      return await Task.FromResult(entity);
    }

    public Entity RenderEntity(PaperContext context, Result result)
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

      return entity;
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
      if (args.Length > 0)
      {
        RunFormatters(context, entity, new object[0], graph);
      }
    }

    private void RunFormatters(PaperContext context, Entity entity, object[] args, object graph = null)
    {
      var path = context.Path;
      var paper = context.Paper;
      var req = context.Request;
      var res = context.Response;

      var allArgs = args.Append(graph).NonNull().ToArray();

      var callers = MatchCallers(paper, allArgs);
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

    private ICollection<Caller> MatchCallers(PaperDescriptor paper, object[] args)
    {
      var callers = (
        from method in paper.Formatters
        let caller = MatchCaller(method, args)
        where caller != null
        select caller.Value
      ).ToArray();
      return callers;
    }

    private Caller? MatchCaller(MethodInfo method, object[] args)
    {
      var parameters = method.GetParameters();
      if (parameters.Length != args.Length)
        return null;

      var values = new object[parameters.Length];
      var cache = args.ToArray();
      for (int i = 0; i < parameters.Length; i++)
      {
        var parameter = parameters[i];
        var type = parameter.ParameterType;

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