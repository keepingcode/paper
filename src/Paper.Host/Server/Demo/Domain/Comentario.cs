using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paper.Host.Server.Demo.Domain
{
  public class Comentario
  {
    public int Id { get; set; }

    public int TicketId { get; set; }

    public int AutorId { get; set; }

    public string Descricao { get; set; }

    public DateTime CriadoEm { get; set; }
  }
}
