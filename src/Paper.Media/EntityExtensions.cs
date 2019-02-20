using System;
using System.Linq;
using System.Collections.Generic;
using Toolset.Collections;

namespace Paper.Media
{
  public static class EntityExtensions
  {
    public static IEnumerable<Entity> Children(this Entity entity)
    {
      return entity.Entities ?? Enumerable.Empty<Entity>();
    }

    public static IEnumerable<Entity> ChildrenAndSelf(this Entity entity)
    {
      return entity.AsSingle().Concat(entity.Entities ?? Enumerable.Empty<Entity>());
    }
  }
}