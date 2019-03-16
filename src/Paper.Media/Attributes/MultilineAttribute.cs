using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class MultilineAttribute : Attribute
  {
    public MultilineAttribute()
    {
      this.Multiline = true;
    }

    public MultilineAttribute(bool multiline)
    {
      this.Multiline = multiline;
    }

    public bool Multiline { get; }
  }
}
