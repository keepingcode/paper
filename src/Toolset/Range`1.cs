using System;
using System.Collections.Generic;
using System.Text;

namespace Toolset
{
  public class Range<T> : Range
  {
    public Range()
      : base(typeof(T), null, null)
    {
    }

    public Range(T min, T max)
      : base(typeof(T), min, max)
    {
    }

    public Range(Range range)
      : base(typeof(T), range.Min, range.Max)
    {
    }

    public new T Min
    {
      get => (T)base.Min;
      set => base.Min = value;
    }

    public new T Max
    {
      get => (T)base.Max;
      set => base.Max = value;
    }
  }
}
