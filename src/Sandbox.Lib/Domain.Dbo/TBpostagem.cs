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
  public class TBpostagem : Table<TBpostagem, int, TBpostagem.Filter>
  {
    [Pk(AutoIncrement.Yes)]
    public int DFid_postagem { get; set; }
    [Fk(typeof(TBusuario))]
    public int DFid_autor { get; set; }
    public string DFtitulo { get; set; }
    public string DFtexto { get; set; }
    public DateTime DFdata_publicacao { get; set; }
    [Fk(typeof(TBpostagem))]
    public int? DFid_postagem_referente { get; set; }
    public char DFtipo { get; set; }

    public class Filter : SmallApi.Filter
    {
      public Var<int?> DFid_postagem { get; set; }
      public Var<int?> DFid_autor { get; set; }
      public Var<string> DFtitulo { get; set; }
      public Var<string> DFtexto { get; set; }
      public Var<DateTime?> DFdata_publicacao { get; set; }
      public Var<int?> DFid_postagem_referente { get; set; }
      public Var<char?> DFtipo { get; set; }
    }
  }
}