using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolset;
using Toolset.Collections;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;

namespace Sandbox.Lib.Domain.SmallApi
{
  public class FilterInfo
  {
    internal FilterInfo()
    {
    }

    public Type FilterType { get; set; }

    public string RowNumberName { get; set; }

    public string[] FieldNames { get; set; }
  }
}