using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Sandbox.Lib.Domain.SmallApi
{
  public abstract class Filter
  {
    [RowNumber]
    public Var<int?> Row { get; set; }
  }
}
