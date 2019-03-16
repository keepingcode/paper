using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class HiddenAttribute : Attribute
  {
    public HiddenAttribute()
    {
      this.Hidden = true;
    }

    public HiddenAttribute(bool hidden)
    {
      this.Hidden = hidden;
    }

    public bool Hidden { get; }
  }
}
