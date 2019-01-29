using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Paper.Media;
using Toolset;

namespace Sandbox.Lib
{
  public static class EntityExtensions
  {
    public static Entity Canonicalize(this Entity entity, string baseRoute)
    {
      return Canonicalize(entity, (UriString)baseRoute);
    }

    public static Entity Canonicalize(this Entity entity, UriString baseRoute)
    {
      ForEachHyperLink(entity, hyperLink =>
      {
        if (hyperLink.Href?.Value.StartsWith("^/") == true)
        {
          hyperLink.Href = (string)baseRoute.Combine(hyperLink.Href.Value.Substring(2));
        }
      });
      return entity;
    }

    private static void ForEachHyperLink(object graph, Action<IHyperLink> visitor)
    {
      if (graph == null || graph.GetType().FullName.StartsWith("System."))
        return;

      if (graph is IHyperLink hyperLink)
      {
        visitor.Invoke(hyperLink);
      }

      foreach (var property in graph.GetType().GetProperties())
      {
        if (property.GetIndexParameters().Length > 0)
          continue;

        var value = property.GetValue(graph);

        if (value == null || value is string || value.GetType().IsPrimitive)
          continue;

        if (value is IEnumerable list)
        {
          foreach (var item in list)
          {
            ForEachHyperLink(item, visitor);
          }
        }
        else
        {
          ForEachHyperLink(value, visitor);
        }
      }
    }

    public static Entity FindHeader(this Entity entity, string headerName, Rel relation)
    {
      return FindHeader(entity, headerName, relation.GetName());
    }

    public static Entity FindHeader(this Entity entity, string headerName, string relation)
    {
      if (entity?.Entities == null)
        return null;

      var header = (
        from child in entity.Entities
        where child.Class.Has(ClassNames.Header)
           && child.Rel.Has(relation)
        from property in child.Properties
        where property.Name.EqualsIgnoreCase("name")
           && property.Value?.ToString().EqualsIgnoreCase(headerName) == true
        select child
      ).FirstOrDefault();

      return header;
    }
  }
}