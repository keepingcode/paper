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
  public class TBempresa : Table<TBempresa, int, TBempresa.Filter>
  {
    [Pk(AutoIncrement.No)]
    public int DFid_empresa { get; set; }
    public string DFrazao_social { get; set; }
    public string DFnome_fantasia { get; set; }
    public string DFcnpj { get; set; }
    public bool DFativo { get; set; }
    [Fk(typeof(TBgrupo_empresa))]
    public int? DFid_grupo_empresa { get; set; }

    public class Filter : SmallApi.Filter
    {
      public Var<int?> DFid_empresa { get; set; }
      public Var<string> DFrazao_social { get; set; }
      public Var<string> DFnome_fantasia { get; set; }
      public Var<string> DFcnpj { get; set; }
      public Var<bool?> DFativo { get; set; }
      public Var<int?> DFid_grupo_empresa { get; set; }
    }

    public class Form
    {
      [Hidden]
      public int DFid_empresa { get; set; }

      public string DFrazao_social { get; set; }

      public string DFnome_fantasia { get; set; }

      public string DFcnpj { get; set; }

      public bool DFativo { get; set; }
    }
  }
}