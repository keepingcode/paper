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
  [Table(Schema = "dbo")]
  public class TBusuario : Table<TBusuario, int, TBusuario.Filter>
  {
    [Pk(AutoIncrement.Yes)]
    public int DFid_usuario { get; set; }
    public string DFsenha { get; set; }
    public string DFnome_usuario { get; set; }
    public int DFnivel_usuario { get; set; }
    public int DFcod_empresa { get; set; }
    public int? DFid_pessoa { get; set; }
    public bool DFativo_inativo { get; set; }
    public int? DFid_centro_distribuicao { get; set; }
    public int? DFcod_setor_usuario { get; set; }
    public DateTime? DFdata_expiracao_senha { get; set; }
    public bool DFsenha_expira { get; set; }
    public string DFpapel { get; set; }
    public int? DFcod_papel { get; set; }

    public class Filter
    {
      public Var<int> DFid_usuario { get; set; }
      public Var<string> DFnome_usuario { get; set; }
      public Var<int> DFnivel_usuario { get; set; }
      public Var<int> DFcod_empresa { get; set; }
      public Var<bool> DFativo_inativo { get; set; }

      [RowNumber]
      public Var<int> DFrow_number { get; set; }
    }
  }
}