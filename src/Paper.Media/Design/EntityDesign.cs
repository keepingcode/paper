using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Toolset;

namespace Paper.Media.Design
{
  public class EntityDesign
  {
    private Entity entity;

    public EntityDesign(Entity entity)
    {
      this.entity = entity;
    }

    protected virtual T Get<T>(T defaultValue = default(T), [CallerMemberName] string property = null)
    {
      var value = entity.GetProperty(property) ?? defaultValue;
      return Change.To<T>(value);
    }

    protected virtual void Set<T>(T value, [CallerMemberName] string property = null)
    {
      entity.SetProperty(property, value);
    }

    protected virtual void Remove([CallerMemberName] string property = null)
    {
      entity.RemoveProperty(property);
    }
  }
}
