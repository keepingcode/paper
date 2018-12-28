using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;

namespace Paper.Host.Server.Demo.Papers.Model
{
  public class ComentarioModel : Comentario
  {
    public string Autor { get; set; }
  }
}
