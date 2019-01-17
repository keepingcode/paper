using System;
using System.Collections.Generic;
using System.Text;
using Toolset.Reflection;

namespace Toolset
{
  public class Range
  {
    private object _min;
    private object _max;

    public Range()
      : this(typeof(object), null, null)
    {
    }

    public Range(object min, object max)
      : this(typeof(object), min, max)
    {
    }

    public Range(Range range)
      : this(typeof(object), range.Min, range.Max)
    {
    }

    protected Range(Type rawType, object min, object max)
    {
      RawType = rawType;
      Min = min;
      Max = max;
    }

    public bool IsMinSet => Min != null;
    public bool IsMaxSet => Max != null;

    public Type RawType { get; }

    public object Min
    {
      get => _min;
      set => _min = Change.To(value, RawType);
    }

    public object Max
    {
      get => _max;
      set => _max = Change.To(value, RawType);
    }

    public override string ToString()
    {
      var min = Change.To<string>(Min);
      var max = Change.To<string>(Max);
      return $"{{min={min ?? "*"}, max={max ?? "*"}}}";
    }

    #region Create<T?>

    public static Range<T?> Between<T>(T min, T max)
      where T : struct
    {
      return new Range<T?>(min, max);
    }

    public static Range<T?> Below<T>(T max)
      where T : struct
    {
      return new Range<T?>(null, max);
    }

    public static Range<T?> Above<T>(T min)
      where T : struct
    {
      return new Range<T?>(min, null);
    }

    #endregion

    #region Create<string>

    public static Range<string> Between(string min, string max)
    {
      return new Range<string>(min, max);
    }

    public static Range<string> Below(string max)
    {
      return new Range<string>(null, max);
    }

    public static Range<string> Above(string min)
    {
      return new Range<string>(min, null);
    }

    #endregion

    #region Factory

    public static bool IsRangeCompatible(object graph)
    {
      return graph?._Has("min") == true || graph?._Has("max") == true;
    }

    public static Range CreateCompatibleRange(object graph)
    {
      var min = graph._Get("min");
      var max = graph._Get("max");
      return new Range(min, max);
    }

    #endregion
  }
}