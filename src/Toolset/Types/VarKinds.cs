using System;
using System.Collections.Generic;
using System.Text;

namespace Toolset.Types
{
  [Flags]
  public enum VarKinds
  {
    Null = 0,

    Value = 1,

    Primitive = Value | 2,
    String = Value | 4,
    Graph = Value | 8,

    List = 16,
    Range = 32
  }
}
