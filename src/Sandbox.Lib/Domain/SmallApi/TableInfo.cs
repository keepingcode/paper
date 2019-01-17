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
  public class TableInfo
  {
    internal TableInfo()
    {
    }

    public Type TableType { get; set; }

    public string TableName { get; set; }

    public Type PkType { get; set; }

    public string PkName { get; set; }

    public bool PkAutoIncrement { get; set; }

    public string[] FieldNames { get; set; }
  }
}