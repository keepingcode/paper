using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Toolset;
using Toolset.Collections;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Blueprint")]
  public class BlueprintPaper : IPaperBlueprint
  {
    public string GetTitle() => "TicketApp";

    public ILink GetIndex() => new LinkTo<MenuPaper>();

    public Blueprint GetBlueprint()
    {
      return new Blueprint
      {
        HasNavBox = false,
        Theme = "purple",
        Info = new Blueprint.Details
        {
          Guid = new Guid("B0AA1246-1CA4-4600-BDF1-BF48A2580965"),
          Name = "TicketApp",
          Title = "TicketApp",
          Version = new Version("0.0.1"),
          Description = "Sistema de atendimento e suporte",
        }
      };
    }
  }
}
