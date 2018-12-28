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
      => new MainMenu();

    public IEnumerable<TicketModel> GetRows()
    {
      var tickets =
        from ticket in DataStore.Current.All<Ticket>()
        select new TicketModel(ticket);

      return tickets.FilterBy(Filter).SortBy(Sort).PaginateBy(Page);
    }

    public IEnumerable<HeaderInfo> GetRowHeaders(IEnumerable<TicketModel> rows)
      => null;

    public IEnumerable<ILink> GetRowLinks(TicketModel row)
      => new TicketMenu(row);

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
