using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Toolset.Collections;

namespace Toolset
{
  public static class Is
  {
    public static bool Collection(object graph)
    {
      var type = graph is Type t ? t : graph?.GetType();
      if (type == null)
        return false;

      return typeof(IList).IsAssignableFrom(type)
          || typeof(IList<>).IsAssignableFrom(type)
          || typeof(ICollection).IsAssignableFrom(type)
          || typeof(ICollection<>).IsAssignableFrom(type);
    }

    public static bool Dictionary(object graph)
    {
      var type = graph is Type t ? t : graph?.GetType();
      if (type == null)
        return false;

      return typeof(IDictionary).IsAssignableFrom(type)
          || typeof(IDictionary<,>).IsAssignableFrom(type)
          //|| typeof(IKeyValueCollection<,>).IsAssignableFrom(type)
          ;
    }

    public static bool Ret(object graph)
    {
      var type = graph is Type t ? t : graph?.GetType();
      if (type == null)
        return false;

      if (type == typeof(Ret))
        return true;

      if (!type.IsGenericType)
        return false;

      return type.GetGenericTypeDefinition() == typeof(Ret<>);
    }

    public static bool Var(object graph)
    {
      var type = graph is Type t ? t : graph?.GetType();
      if (type == null)
        return false;

      if (type == typeof(Var))
        return true;

      if (!type.IsGenericType)
        return false;

      return type.GetGenericTypeDefinition() == typeof(Var<>);
    }

    public static bool Range(object graph)
    {
      var type = graph is Type t ? t : graph?.GetType();
      if (type == null)
        return false;

      if (type == typeof(Range))
        return true;

      if (!type.IsGenericType)
        return false;

      return type.GetGenericTypeDefinition() == typeof(Range<>);
    }
  }
}
