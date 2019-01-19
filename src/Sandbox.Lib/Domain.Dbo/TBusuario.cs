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
  public class TBusuario : Table<TBusuario, int, TBusuario.Filter>
  {
    [Pk(AutoIncrement.Yes)]
    public int DFid_usuario { get; set; }
    public string DFlogin { get; set; }
    public string DFnome { get; set; }
    public bool DFativo { get; set; }
    [Secret]
    public string DFsenha { get; set; }
    [Fk(typeof(TBempresa))]
    public int? DFid_empresa_padrao { get; set; }
    [Fk(typeof(TBpapel_usuario))]
    public int? DFid_papel_usuario { get; set; }

    public class Filter : SmallApi.Filter
    {
      public Var<int?> DFid_usuario { get; set; }
      public Var<string> DFlogin { get; set; }
      public Var<string> DFnome { get; set; }
      public Var<bool?> DFativo { get; set; }
      public Var<string> DFsenha { get; set; }
      public Var<int?> DFid_empresa_padrao { get; set; }
      public Var<int?> DFid_papel_usuario { get; set; }
    }
  }
}