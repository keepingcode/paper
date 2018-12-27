using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paper.Host.Server.Demo.Domain
{
  public class Comentario
  {
    public int TicketId { get; set; }

    public int UsuarioId { get; set; }

    public string Descricao { get; set; }
  }
}
