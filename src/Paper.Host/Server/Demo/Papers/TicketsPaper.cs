using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Papers.Model;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Toolset;
using Toolset.Reflection;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Tickets")]
  public class TicketsPaper : IPaperRows<TicketsPaper.CustomFilter, TicketModel>
  {
    public string GetTitle() => "Tickets";

    public Page Page { get; } = new Page();

    public Sort Sort { get; } = new Sort().AddFieldsFrom<TicketModel>();

    public CustomFilter Filter { get; } = new CustomFilter();

    public IEnumerable<ILink> GetLinks()
      => new ILink[]
      {
        new LinkTo<MenuPaper>()
      };

    public IEnumerable<TicketModel> GetRows()
    {
      var store = DataStore.Current;
      return (
        from ticket in store.All<Ticket>()
        select new TicketModel
        {
          Autor = store.Get<Usuario>(ticket.AutorId)?.Nome,
          Responsavel = store.Get<Usuario>(ticket.ResponsavelId)?.Nome,
        }._CopyFrom(ticket)
      ).FilterBy(this.Filter).SortBy(this.Sort).PaginateBy(this.Page);
    }

    public IEnumerable<HeaderInfo> GetRowHeaders(IEnumerable<TicketModel> rows)
      => new HeaderInfo[]
      {
        new HeaderInfo("Id", "Id", "int"),
        new HeaderInfo("Titulo", "Titulo", "string"),
        new HeaderInfo("Descricao", "Descrição", "string"),
        new HeaderInfo("CriadoEm", "Criado Em", "datetime"),
        new HeaderInfo("Autor", "Autor", "string"),
        new HeaderInfo("Responsavel", "Responsavel", "string"),
        new HeaderInfo("Status", "Status", "string"),
      };

    public IEnumerable<ILink> GetRowLinks(TicketModel row)
      => new ILink[]
      {
        new LinkSelf($"/Ticket/{row.Id}"),
        new LinkTo<UsuarioPaper>(
          paper => paper.Id = row.AutorId,
          builder =>
          {
            builder.Title = "Autor do Ticket";
            builder.Rel = new Media.NameCollection
            {
              nameof(row.Autor),
              nameof(row.AutorId)
            };
          }
        ),
        new LinkTo<UsuarioPaper>(
          paper => paper.Id = row.ResponsavelId,
          builder =>
          {
            builder.Title = "Responsável do Ticket";
            builder.Rel = new Media.NameCollection
            {
              nameof(row.Responsavel),
              nameof(row.ResponsavelId)
            };
          }
        )
      };

    public class CustomFilter : IFilter
    {
      public int? Id { get; set; }

      public string Titulo { get; set; }

      public string Descricao { get; set; }

      public DateTime? CriadoEm { get; set; }

      public int? AutorId { get; set; }

      public string Autor { get; set; }

      public int? ResponsavelId { get; set; }

      public string Responsavel { get; set; }

      public Status? Status { get; set; }
    }
  }
}
