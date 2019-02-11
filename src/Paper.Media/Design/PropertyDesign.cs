using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Toolset;

namespace Paper.Media.Design
{
  public abstract class PropertyDesign
  {
    private IPropertyMap map;

    public PropertyDesign(IPropertyMap map)
    {
      this.map = map;
    }

    protected virtual T Get<T>(T defaultValue = default(T), [CallerMemberName] string property = null)
    {
      return Change.To<T>(map.Properties?[property] ?? defaultValue);
    }

    protected virtual void Set<T>(T value, [CallerMemberName] string property = null)
    {
      map.WithProperties().SetProperty(property, value);
    }

    protected virtual void Remove([CallerMemberName] string property = null)
    {
      map.Properties?.Remove(property);
    }
  }
}
