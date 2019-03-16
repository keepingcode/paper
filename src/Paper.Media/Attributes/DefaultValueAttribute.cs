using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class DefaultValueAttribute : Attribute
  {
    public DefaultValueAttribute(object defaultValue)
    {
      this.DefaultValue = defaultValue;
    }

    public object DefaultValue { get; }
  }
}
