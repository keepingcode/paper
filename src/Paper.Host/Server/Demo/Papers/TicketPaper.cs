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
  [Expose, Paper("/Ticket/{Id}")]
  public class TicketPaper : IPaperData<TicketModel>
  {
    public int Id { get; set; }

    public string GetTitle() => $"Ticket #{Id}";

    public TicketModel GetData()
    {
      var store = DataStore.Current;
      var ticket = store.Get<Ticket>(Id);
      if (ticket == null)
        return null;

      var model = new TicketModel()._CopyFrom(ticket);
      model.Autor = store.Get<Usuario>(ticket.AutorId)?.Nome;
      model.Responsavel = store.Get<Usuario>(ticket.ResponsavelId)?.Nome;
      return model;
    }

    public IEnumerable<HeaderInfo> GetDataHeaders(TicketModel data)
      => null;

    public IEnumerable<ILink> GetDataLinks(TicketModel data)
      => new ILink[]
      {
        new LinkTo<MenuPaper>(),
        new LinkTo<TicketsPaper>(
          null,
          builder => builder.Title= "Todos os Tickets"
        ),
        new LinkTo<UsuarioPaper>(
          paper => paper.Id = data.AutorId,
          builder =>
          {
            builder.Title = "Autor do Ticket";
            builder.Rel = new Media.NameCollection
            {
              nameof(data.Autor),
              nameof(data.AutorId)
            };
          }
        ),
        new LinkTo<UsuarioPaper>(
          paper => paper.Id = data.ResponsavelId,
          builder =>
          {
            builder.Title = "Responsável do Ticket";
            builder.Rel = new Media.NameCollection
            {
              nameof(data.Responsavel),
              nameof(data.ResponsavelId)
            };
          }
        )
      };
  }
}
