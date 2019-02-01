﻿using System;
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
  }
}
