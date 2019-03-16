using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class RangeAttribute : Attribute
  {
    public RangeAttribute()
    {
    }

    public RangeAttribute(int min, int max)
    {
      this.Min = min;
      this.Max = max;
    }

    public int? Min { get; set; }

    public int? Max { get; set; }
  }
}