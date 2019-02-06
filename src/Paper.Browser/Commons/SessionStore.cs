using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolset.Collections;

namespace Paper.Browser.Commons
{
  public static class SessionStore
  {
    private static Map<Type, object> cache = new Map<Type, object>();

    public static T Get<T>()
      where T : new()
    {
      var graph = cache[typeof(T)];
      if (graph == null)
      {
        cache[typeof(T)] = graph;
      }
      return (T)graph;
    }
  }
}