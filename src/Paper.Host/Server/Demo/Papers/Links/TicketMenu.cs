using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Papers.Model;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design.Papers;
using Toolset.Reflection;

namespace Paper.Host.Server.Demo.Papers.Links
{
  public class TicketMenu : List<ILink>
  {
    public TicketMenu(int ticketId, IEnumerable<ILink> links = null)
      : base(links ?? Enumerable.Empty<ILink>())
    {
      var ticket = DataStore.Current.FindOne<Ticket>(ticketId);
      var model = new TicketModel(ticket);
      FillUp(model);
    }

    public TicketMenu(TicketModel ticket, IEnumerable<ILink> links = null)
      : base(links ?? Enumerable.Empty<ILink>())
    {
      FillUp(ticket);
    }

    private void FillUp(TicketModel ticket)
    {
      AddRange(new ILink[]
      {
        new LinkSelf<TicketPaper>(paper => paper.Id = ticket.Id),
        new LinkTo<ComentariosPaper>(
          paper => paper.TicketId = ticket.Id,
          "Comentários",
          new[] { nameof(ticket.Comentarios), nameof(ticket.Id), nameof(ticket.Titulo) }
        ),
        new LinkTo<UsuarioPaper>(
          paper => paper.Id = ticket.AutorId,
          "Autor do Ticket",
          new[] { nameof(ticket.Autor), nameof(ticket.AutorId) }
        ),
        new LinkTo<UsuarioPaper>(
          paper => paper.Id = ticket.ResponsavelId,
          "Responsável do Ticket",
          new[] { nameof(ticket.Responsavel), nameof(ticket.ResponsavelId) }
        )
      });
    }
  }
}