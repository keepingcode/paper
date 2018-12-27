using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Papers.Model;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Toolset;
using Toolset.Reflection;

namespace Paper.Host.Server.Demo.Papers
{
  [Expose, Paper("/Usuario/{Id}")]
  public class UsuarioPaper : IPaperData<UsuarioModel>
  {
    public int Id { get; set; }

    public string GetTitle() => $"Usuário #{Id}";

    public UsuarioModel GetData()
    {
      var store = DataStore.Current;

      var usuario= store.Get<Usuario>(Id);
      if (usuario == null)
        return null;

      var model = new UsuarioModel()._CopyFrom(usuario);
      model.TicketsAbertos = store.Find<Ticket>(x => x.AutorId.Equals(Id)).Count();
      model.TicketsDestinados = store.Find<Ticket>(x => x.ResponsavelId.Equals(Id)).Count();

      return model;
    }

    public IEnumerable<HeaderInfo> GetDataHeaders(UsuarioModel data)
      => null;

    public IEnumerable<ILink> GetDataLinks(UsuarioModel data)
      => new ILink[]
      {
        new LinkTo<MenuPaper>(),
        new LinkTo<UsuariosPaper>(
          null,
          builder => builder.Title= "Todos os Usuários"
        ),
        new LinkTo<TicketsPaper>(
          paper => paper.Filter.AutorId = data.Id,
          builder =>
          {
            builder.Title = $"Tickets abertos por {data.Nome}";
            builder.Rel = new Media.NameCollection{ nameof(data.TicketsAbertos) };
          }
        ),
        new LinkTo<TicketsPaper>(
          paper => paper.Filter.ResponsavelId = data.Id,
          builder =>
          {
            builder.Title = $"Tickets destinados a {data.Nome}";
            builder.Rel = new Media.NameCollection{ nameof(data.TicketsDestinados) };
          }
        )
      };
  }
}
