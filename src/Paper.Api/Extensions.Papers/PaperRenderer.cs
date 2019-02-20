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

      var isList = typeof(IEnumerable).IsAssignableFrom(result.ValueType);
      if (isList)
      {
        var list = (IEnumerable)result.Value;
        entity.AddEntities(list, (item, e) => BuildRecord(context, e, item, args));
      }
      else
      {
        BuildRecord(context, entity, result, args);
      }

      LinkAndFormat(context, entity, args);

      return entity;
    }

    private void BuildRecord(PaperContext context, Entity entity, object record, object[] args)
    {
      entity.AddClass(ClassNames.Record);
      entity.AddClass(record.GetType());
      entity.AddProperties(record);

      LinkAndFormat(context, entity, args, record);

      if (entity.Properties[HeaderDesign.BagName] == null)
      {
        entity.AddHeaders(record, RelNames.Record);
      }
    }

    private void LinkAndFormat(PaperContext context, Entity entity, object[] args, object graph = null)
    {
      AppendMedia(context, entity, args, graph);
      if (args.Length > 0)
      {
        AppendMedia(context, entity, new object[0], graph);
      }
    }

    private void AppendMedia(PaperContext context, Entity entity, object[] args, object graph = null)
    {
      var path = context.Path;
      var paper = context.Paper;
      var req = context.Request;
      var res = context.Response;

      var allArgs = args.Append(graph).NonNull().ToArray();

      var appenders = MatchAppenders(paper, allArgs);
      foreach (var appender in appenders)
      {
        var result = objectFactory.Invoke(paper.Paper, appender.Method, appender.Args);
        if (result == null)
          continue;

        IEnumerable builders;

        if (result is IEnumerable items)
        {
          builders = items;
        }
        else if (result is ICollection list)
        {
          builders = list;
        }
        else
        {
          builders = new[] { result };
        }

        foreach (var builder in builders)
        {
          if (builder is IFormatter formatter)
          {
            formatter.Format(objectFactory, entity);
          }
          else if (builder is Format format)
          {
            format.Invoke(objectFactory, entity);
          }

          if (builder is Link link)
          {
            entity.AddLink(link);
          }
        }
      }
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

    private ICollection<Caller> MatchAppenders(PaperDescriptor paper, object[] args)
    {
      var callers = (
        from method in paper.Linkers.Concat(paper.Formatters)
        let caller = MatchCaller(method, args)
        where caller != null
        select caller.Value
      ).ToArray();
      return callers;
    }
  }
}