using System;
using System.Collections.Generic;
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
using static Paper.Host.Server.Demo.Papers.TicketsPaper;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Comentarios/{TicketId}")]
  public class ComentariosPaper : IPaperRows<ComentariosPaper.CustomFilter, ComentarioModel>
  {
    public int TicketId { get; set; }

    public string GetTitle() => $"Comentários do ticket #{TicketId}";

    public Page Page { get; } = new Page();

    public Sort Sort { get; } = new Sort().AddFieldsFrom<ComentarioModel>();

    public CustomFilter Filter { get; } = new CustomFilter();

    public IEnumerable<ILink> GetLinks()
      => new MainMenu();

    public IEnumerable<ComentarioModel> GetRows()
    {
      var store = DataStore.Current;
      var comentarios =
        from comentario in store.Find<Comentario>(x => x.TicketId.Equals(TicketId))
        orderby comentario.CriadoEm
        select new ComentarioModel
        {
          Autor = store.FindOne<Usuario>(x => x.Id.Equals(comentario.AutorId))?.Nome
        }._CopyFrom(comentario);

      return
        comentarios
          .FilterBy(this.Filter)
          .SortBy(this.Sort)
          .PaginateBy(this.Page);
    }

    public IEnumerable<HeaderInfo> GetRowHeaders(IEnumerable<ComentarioModel> rows)
      => null;

    public IEnumerable<ILink> GetRowLinks(ComentarioModel row)
      => Enumerable.Concat(
        new TicketMenu(row.TicketId).ExceptSelf(),
        new UsuarioMenu(row.AutorId).ExceptSelf()
      );

    public class CustomFilter : IFilter
    {
      public int? TicketId { get; set; }

      public int? AutorId { get; set; }

      public string Autor { get; set; }

      public string Descricao { get; set; }
    }
  }
}