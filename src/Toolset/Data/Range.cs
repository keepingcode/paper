﻿using System;
using System.Collections.Generic;
using System.Text;
using Toolset.Reflection;

namespace Toolset.Data
{
  public struct Range
  {
    public static readonly Range Empty = default(Range);

    public Range(object min, object max)
    {
      this.Min = min;
      this.Max = max;
    }

    public object Min { get; }
    public object Max { get; }

    public override string ToString() => $"{{{Min ?? "*"}, {Max ?? "*"}}}";

    public static Range<T> Between<T>(T? min, T? max)
      where T : struct
    {
      return new Range<T>(min, max);
    }

    public static Range<T> Between<T>(T min, T? max)
      where T : struct
    {
      return new Range<T>(min, max);
    }

    public static Range<T> Between<T>(T? min, T max)
      where T : struct
    {
      return new Range<T>(min, max);
    }

    public static Range<T> Between<T>(T min, T max)
      where T : struct
    {
      return new Range<T>(min, max);
    }

    public static Range<T> Above<T>(T min)
      where T : struct
    {
      return new Range<T>(min, null);
    }

    public static Range<T> Below<T>(T max)
      where T : struct
    {
      return new Range<T>(null, max);
    }

    public static RangeEx<string> Between(string min, string max)
    {
      return new RangeEx<string>(min, max);
    }

    public static RangeEx<string> Above(string min)
    {
      return new RangeEx<string>(min, null);
    }

    public static RangeEx<string> Below(string max)
    {
      return new RangeEx<string>(null, max);
    }

    public static bool IsRangeCompatible(object value)
    {
      return value?._Has("min") == true || value?._Has("max") == true;
    }

    public static Range CreateCompatibleRange(object value)
    {
      var min = value._Get("min");
      var max = value._Get("max");
      return new Range(min, max);
    }
  }
}