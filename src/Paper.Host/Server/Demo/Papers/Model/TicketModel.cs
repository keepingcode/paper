using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Store;
using Toolset.Reflection;

namespace Paper.Host.Server.Demo.Papers.Model
{
  public class TicketModel : Ticket
  {
    public TicketModel(Ticket ticket)
    {
      this._CopyFrom(ticket);

      var store = DataStore.Current;
      this.Autor = store.FindOne<Usuario>(AutorId)?.Nome;
      this.Responsavel = store.FindOne<Usuario>(ResponsavelId)?.Nome;
      this.Comentarios =
        store
          .Find<Comentario>(x => x.TicketId.Equals(ticket.Id))
          .Count();
    }

    public string Autor { get; set; }

    public string Responsavel { get; set; }

    public int Comentarios { get; set; }
  }
}
