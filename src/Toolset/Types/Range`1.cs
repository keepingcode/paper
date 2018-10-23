using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Toolset.Types
{
  public struct Range<T>
  {
    public T Min { get; set; }
    public T Max { get; set; }

    public Range(T min, T max)
    {
      this.Min = min;
      this.Max = max;
    }

    public static implicit operator Range<T>(Range range)
    {
      var type = typeof(T);
      var min = Change.To(range.Min, type);
      var max = Change.To(range.Max, type);
      var target = (Range<T>)Activator.CreateInstance(typeof(Range<T>), min, max);
      return target;
    }

    public static implicit operator Range(Range<T> range)
    {
      var target = new Range(range.Min, range.Max);
      return target;
    }
  }
}
