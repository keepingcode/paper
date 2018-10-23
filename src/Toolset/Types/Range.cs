using System;
using System.Collections.Generic;
using System.Text;

namespace Toolset.Types
{
  public struct Range
  {
    public object Min { get; }
    public object Max { get; }

    public Range(object min, object max)
    {
      this.Min = min;
      this.Max = max;
    }
  }
}