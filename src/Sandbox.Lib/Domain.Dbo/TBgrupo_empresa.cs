using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sandbox.Lib.Domain.SmallApi;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;

namespace Sandbox.Lib.Domain.Dbo
{
  public class TBgrupo_empresa : Table<TBgrupo_empresa, int, TBgrupo_empresa.Filter>
  {
    [Pk(AutoIncrement.Yes)]
    public int DFid_grupo_empresa { get; set; }
    public string DFnome { get; set; }

    public class Filter : SmallApi.Filter
    {
      public Var<string> DFnome { get; set; }
    }
  }
}