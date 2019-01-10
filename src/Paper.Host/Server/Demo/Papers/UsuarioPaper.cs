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
  [Expose, Paper("/Usuario/{Id}")]
  public class UsuarioPaper : IPaperData<UsuarioModel>
  {
    public int Id { get; set; }

    public string GetTitle() => $"Usuário #{Id}";

    public IEnumerable<ILink> GetLinks()
      => new MainMenu();

    public UsuarioModel GetData()
    {
      var usuario = DataStore.Current.FindOne<Usuario>(Id);
      return (usuario != null) ? new UsuarioModel(usuario) : null;
    }

    public IEnumerable<HeaderInfo> GetDataHeaders(UsuarioModel data)
      => null;

    public IEnumerable<ILink> GetDataLinks(UsuarioModel data)
      => new UsuarioMenu(data);

    public void Save(Usuario usuario)
    {
    }
  }
}
