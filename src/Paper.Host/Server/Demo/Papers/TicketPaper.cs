using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Papers.Links;
using Paper.Host.Server.Demo.Papers.Model;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Toolset;
using Toolset.Reflection;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Ticket/{Id}")]
  public class TicketPaper : IPaperData<TicketModel>
  {
    public int Id { get; set; }

    public string GetTitle() => $"Ticket #{Id}";

    public IEnumerable<ILink> GetLinks()
      => new MainMenu();

    public TicketModel GetData()
    {
      var ticket = DataStore.Current.FindOne<Ticket>(Id);
      return (ticket != null) ? new TicketModel(ticket) : null;
    }

    public IEnumerable<HeaderInfo> GetDataHeaders(TicketModel data)
      => null;

    public IEnumerable<ILink> GetDataLinks(TicketModel data)
      => new TicketMenu(data);
  }
}