using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Papers.Links;
using Paper.Host.Server.Demo.Papers.Model;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Toolset;
using Toolset.Reflection;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Usuarios")]
  public class UsuariosPaper : IPaperRows<UsuariosPaper.CustomFilter, UsuarioModel>
  {
    public string GetTitle() => "Usuários";

    public Page Page { get; } = new Page();

    public Sort Sort { get; } = new Sort().AddFieldsFrom<UsuarioModel>();

    public CustomFilter Filter { get; } = new CustomFilter();

    public IEnumerable<ILink> GetLinks()
      => new MainMenu();

    public IEnumerable<UsuarioModel> GetRows()
    {
      var usuarios =
        from usuario in DataStore.Current.All<Usuario>()
        select new UsuarioModel(usuario);

      return
        usuarios
          .FilterBy(this.Filter)
          .SortBy(this.Sort)
          .PaginateBy(this.Page);
    }

    public IEnumerable<HeaderInfo> GetRowHeaders(IEnumerable<UsuarioModel> rows)
      => null;

    public IEnumerable<ILink> GetRowLinks(UsuarioModel row)
      => new UsuarioMenu(row);

    public class CustomFilter : IFilter
    {
      public int? Id { get; set; }

      public string Login { get; set; }

      public string Nome { get; set; }
    }

    public Usuario Create()
    {
    }
  }
}