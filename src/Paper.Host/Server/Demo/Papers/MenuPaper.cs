using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Papers.Links;
using Paper.Host.Server.Demo.Papers.Model;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Toolset;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Menu")]
  public class MenuPaper : IPaperCards<Card<ILink>>
  {
    private static readonly Card<ILink>[] Menu =
    {
      new Card<ILink>
      {
        Title = "Tickets",
        Icon = "forum",
        Data = new LinkTo<TicketsPaper>(),
        Description = "Gerencie seus tickets"
      },
      new Card<ILink>
      {
        Title = "Usuários",
        Icon = "people",
        Data = new LinkTo<UsuariosPaper>(),
        Description = "Crie e edite usuários"
      }
    };

    public string GetTitle() => "Menu";

    public IEnumerable<ILink> GetLinks() 
      => new MainMenu();

    public IEnumerable<Card<ILink>> GetCards()
      => Menu;

    public IEnumerable<HeaderInfo> GetCardHeaders(Card<ILink> card)
      => null;

    public IEnumerable<ILink> GetCardLinks(Card<ILink> card)
      => new[] { card.Data };
  }
}