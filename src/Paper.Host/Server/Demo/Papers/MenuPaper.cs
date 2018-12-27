using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Papers.Model;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Toolset;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Menu")]
  public class MenuPaper : IPaperCards<Card<MenuModel>>
  {
    public string GetTitle() => "Menu";

    public IEnumerable<Card<MenuModel>> GetCards()
    {
      return
        from item in DataStore.Current.All<MenuModel>()
        select new Card<MenuModel>
        {
          Data = item,
          Title = item.Nome,
          Description = item.Descricao,
          Icon = item.Icon
        };
    }

    public IEnumerable<HeaderInfo> GetCardHeaders(Card<MenuModel> card)
      => null;

    public IEnumerable<ILink> GetCardLinks(Card<MenuModel> card)
      => new[] { new LinkTo(card.Data.Link) };
  }
}