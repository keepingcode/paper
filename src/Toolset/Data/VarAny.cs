using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Toolset.Collections;
using Toolset.Reflection;

namespace Toolset.Data
{
  public class VarAny : VarAny<object>
  {
    public VarAny()
    {
    }

    public VarAny(object rawValue)
    {
      ((IVar)this).RawValue = rawValue;
    }

    public bool HasWildcards => ((IVar)this).HasWildcards;

    public string TextPattern => ((IVar)this).TextPattern;
  }
}
