using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paper.Host.Server.Demo.Domain
{
  public class Ticket
  {
    public int Id { get; set; }

    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public DateTime CriadoEm { get; set; }

    public int AutorId { get; set; }

    public int ResponsavelId { get; set; }

    public Status Status { get; set; }
  }
}
