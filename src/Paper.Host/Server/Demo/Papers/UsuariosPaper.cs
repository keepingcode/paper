using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Toolset;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Usuarios")]
  public class UsuariosPaper : IPaperRows<UsuariosPaper.CustomFilter, Usuario>
  {
    public string GetTitle() => "Usuários";

    public Page Page { get; } = new Page();

    public Sort Sort { get; } = new Sort().AddFieldsFrom<Usuario>();

    public CustomFilter Filter { get; } = new CustomFilter();

    public IEnumerable<ILink> GetLinks()
      => new ILink[]
      {
        new LinkTo<MenuPaper>()
      };

    public IEnumerable<Usuario> GetRows()
    {
      return
        DataStore.Current
          .All<Usuario>()
          .FilterBy(this.Filter)
          .SortBy(this.Sort)
          .PaginateBy(this.Page);
    }

    public IEnumerable<HeaderInfo> GetRowHeaders(IEnumerable<Usuario> rows)
      => null;

    public IEnumerable<ILink> GetRowLinks(Usuario row)
      => new ILink[]
      {
        new LinkSelf($"/Usuario/{row.Id}")
      };

    public class CustomFilter : IFilter
    {
      public int? Id { get; set; }

      public string Login { get; set; }

      public string Nome { get; set; }
    }
  }
}
