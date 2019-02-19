using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Design;
using Toolset.Collections;

namespace Paper.Api.Extensions.Papers
{
  internal class PaperRenderer2
  {
    private readonly IPaperCatalog paperCatalog;
    private readonly IObjectFactory objectFactory;

    public PaperRenderer2(IObjectFactory objectFactory, IPaperCatalog paperCatalog)
    {
      this.objectFactory = objectFactory;
      this.paperCatalog = paperCatalog;
    }

    public Entity RenderEntity(string path, PaperDescriptor paper, Request req, Response res, Result result, object[] pathArgs)
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

          FormatEntity(path, paper, req, res, e, pathArgs, item);
          if (pathArgs.Length > 0)
          {
            FormatEntity(path, paper, req, res, e, new object[0], item);
          }
        });
      }
      else
      {
        entity.AddClass(ClassNames.Record);
        entity.AddClass(result.ValueType);
        entity.AddProperties(result.Value);

        FormatEntity(path, paper, req, res, entity, pathArgs, result.Value);
        if (pathArgs.Length > 0)
        {
          FormatEntity(path, paper, req, res, entity, new object[0], result.Value);
        }
      }

      FormatEntity(path, paper, req, res, entity, pathArgs);
      if (pathArgs.Length > 0)
      {
        FormatEntity(path, paper, req, res, entity, new object[0]);
      }

      return entity;
    }

    private void FormatEntity(string path, PaperDescriptor paper, Request req, Response res, Entity entity, object[] pathArgs, object graph = null)
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

        var argTypes = method.GetParameters().Select(x => x.ParameterType);
        var convertedArgs = ConvertArgs(argTypes, args).ToArray();
        if (convertedArgs.All(x => x == null))
          continue;

        var terms = objectFactory.Invoke(paper.Paper, method, convertedArgs);
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

    private IEnumerable<object> ConvertArgs(IEnumerable<Type> types, object[] args)
    {
      var cache = args.ToArray();
      foreach (var type in types)
      {
        int? index;

        index = (
          cache.Select((arg, i) => new { arg, i })
              .Where(x => x.arg != null)
              .Where(x => x.arg.GetType() == type)
              .FirstOrDefault()?.i
        ) ?? (
          cache.Select((arg, i) => new { arg, i })
              .Where(x => x.arg != null)
              .Where(x => type.IsAssignableFrom(x.arg.GetType()))
              .FirstOrDefault()?.i
        );

        if (index == null)
        {
          yield return null;
          continue;
        }

        yield return cache[index.Value];
        cache[index.Value] = null;
      }
    }
  }
}
