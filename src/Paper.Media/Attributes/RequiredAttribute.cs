using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class RequiredAttribute : Attribute
  {
    public RequiredAttribute()
    {
      this.Required = true;
    }

    public RequiredAttribute(bool required)
    {
      this.Required = required;
    }

    public bool Required { get; }
  }
}
