using System;
using System.Collections.Generic;
using System.Text;

namespace Toolset
{
  public static class NumberExtensions
  {
    public static bool InRange(this IComparable number, int min, int max)
    {
      return number.CompareTo(min) >= 0 && number.CompareTo(max) <= 0;
    }
  }
}
