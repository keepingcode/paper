using System;
using System.Linq;
using System.Collections.Generic;
using Toolset.Collections;

namespace Paper.Media
{
  public static class EntityExtensions
  {
    public static IEnumerable<Entity> EntitiesAndSelf(this Entity entity)
    {
      return entity.AsSingle().Concat(entity.Entities);
    }
  }
}