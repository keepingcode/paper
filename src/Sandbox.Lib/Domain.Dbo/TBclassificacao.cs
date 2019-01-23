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
  public class TBclassificacao : Table<TBclassificacao, int, TBclassificacao.Filter>
  {
    [Pk(AutoIncrement.Yes)]
    public int DFid_classificacao { get; set; }

    [Fk(typeof(TBusuario))]
    public int DFid_autor { get; set; }

    [Fk(typeof(TBpostagem))]
    public int DFid_postagem_referente { get; set; }

    public int DFclassificacao { get; set; }

    public class Filter : SmallApi.Filter
    {
      public Var<int?> DFid_autor { get; set; }
      public Var<int?> DFid_postagem_referente { get; set; }
    }
  }
}