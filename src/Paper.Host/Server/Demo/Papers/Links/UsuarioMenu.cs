using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Store;
using Paper.Media.Design.Papers;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Papers.Model;
using Toolset.Reflection;

namespace Paper.Host.Server.Demo.Papers.Links
{
  public class UsuarioMenu : List<ILink>
  {
    public UsuarioMenu(int usuarioId, IEnumerable<ILink> links = null)
      : base(links ?? Enumerable.Empty<ILink>())
    {
      var usuario = DataStore.Current.FindOne<Usuario>(usuarioId);
      var model = new UsuarioModel(usuario);
      FillUp(model);
    }

    public UsuarioMenu(UsuarioModel usuario, IEnumerable<ILink> links = null)
      : base(links ?? Enumerable.Empty<ILink>())
    {
      FillUp(usuario);
    }

    private void FillUp(UsuarioModel usuario)
    {
      AddRange(new ILink[]
      {
        new LinkSelf<UsuarioPaper>(paper => paper.Id = usuario.Id),
        new LinkTo<TicketsPaper>(
          paper => paper.Filter.AutorId = usuario.Id,
          link =>
          {
            link.Title = $"Tickets abertos por {usuario.Nome}";
            link.Rel = new Media.NameCollection{ nameof(usuario.TicketsAbertos) };
          }
        ),
        new LinkTo<TicketsPaper>(
          paper => paper.Filter.ResponsavelId = usuario.Id,
          link =>
          {
            link.Title = $"Tickets destinados a {usuario.Nome}";
            link.Rel = new Media.NameCollection{ nameof(usuario.TicketsDestinados) };
          }
        )
      });
    }
  }
}