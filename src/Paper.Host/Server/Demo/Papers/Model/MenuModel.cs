using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Media.Design.Papers;

namespace Paper.Host.Server.Demo.Papers.Model
{
  public class MenuModel
  {
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public string Icon { get; set; }

    public string Link { get; set; }
  }
}
