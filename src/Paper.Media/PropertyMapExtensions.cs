using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Media
{
  public static class PropertyMapExtensions
  {
    public static PropertyMap WithMap(this PropertyMap map, string property)
    {
      var value = map[property];
      if (value == null)
      {
        map[property] = value = new PropertyMap();
      }
      return (PropertyMap)value;
    }

    public static PropertyValueCollection WithCollection(this PropertyMap map, string property)
    {
      var value = map[property];
      if (value == null)
      {
        map[property] = value = new PropertyValueCollection();
      }
      return (PropertyValueCollection)value;
    }
  }
}