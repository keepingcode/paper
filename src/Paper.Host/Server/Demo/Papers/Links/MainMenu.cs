using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Media.Design.Papers;

namespace Paper.Host.Server.Demo.Papers.Links
{
  public class MainMenu : List<ILink>
  {
    public MainMenu(IEnumerable<ILink> links = null)
      : base(links ?? Enumerable.Empty<ILink>())
    {
      AddRange(new ILink[]
      {
        new LinkTo<MenuPaper>(),
        new LinkTo<TicketsPaper>(null, builder => builder.Title = "Todos os Tickets"),
        new LinkTo<UsuariosPaper>(null, builder => builder.Title = "Todos os Usuários")
      });
    }
  }
}