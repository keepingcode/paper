using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Store;
using Toolset.Reflection;

namespace Paper.Host.Server.Demo.Papers.Model
{
  public class UsuarioModel : Usuario
  {
    public UsuarioModel(Usuario usuario)
    {
      this._CopyFrom(usuario);

      var store = DataStore.Current;
      this.TicketsAbertos = store.Find<Ticket>(x => x.AutorId.Equals(Id)).Count();
      this.TicketsDestinados = store.Find<Ticket>(x => x.ResponsavelId.Equals(Id)).Count();
    }

    public int TicketsAbertos { get; set; }

    public int TicketsDestinados { get; set; }
  }
}
