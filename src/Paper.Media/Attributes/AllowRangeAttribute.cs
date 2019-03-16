using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class AllowRangeAttribute : Attribute
  {
    public AllowRangeAttribute()
    {
      this.AllowRange = true;
    }

    public AllowRangeAttribute(bool allowRange)
    {
      this.AllowRange = allowRange;
    }

    public bool AllowRange { get; }
  }
}
