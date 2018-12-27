using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Papers.Model;
using Toolset.Collections;

namespace Paper.Host.Server.Demo.Store
{
  public class DataStore
  {
    public static DataStore Current { get; }

    private readonly List<Node> cache = new List<Node>();

    private readonly object synclock = new object();

    static DataStore()
    {
      var current = new DataStore();
      FillUp(current);
      Current = current;
    }

    private DataStore()
    {
    }

    private static void FillUp(DataStore current)
    {
      var menu = new MenuModel[]
      {
        new MenuModel
        {
          Id = 1,
          Nome = "Tickets",
          Descricao = "Gerencie seus tickets",
          Icon = "forum",
          Link = "/Tickets"
        },
        new MenuModel
        {
          Id = 2,
          Nome = "Usuários",
          Descricao = "Crie e edite usuários",
          Icon = "people",
          Link = "/Usuarios"
        }
      };
      menu.ForEach(x => current.Save(x, x.Id));

      var usuarios = new Usuario[]
      {
        new Usuario
        {
          Id = 1,
          Login = "admin",
          Nome = "Administrador",
        },
        new Usuario
        {
          Id = 2,
          Login = "guest",
          Nome = "Convidado",
        }
      };
      usuarios.ForEach(x => current.Save(x, x.Id));

      var tickets = new Ticket[]
      {
        new Ticket
        {
          Id = 1,
          Titulo = "Lorem ipsum dolor sit amet",
          Descricao = "Consectetur adipiscing elit. Donec et dapibus purus. Donec velit diam, eleifend auctor quam accumsan, dignissim consequat nisi. Nulla eros urna, auctor eget purus eget, ornare aliquet quam. Ut velit felis, aliquet vitae consectetur eu, luctus mattis orci. Nam ut justo velit. Cras auctor turpis a nibh pellentesque rhoncus. Nunc accumsan fermentum tortor, quis dictum dui vestibulum id. Morbi finibus elit vitae enim tempus, sit amet porta enim rhoncus. Pellentesque id sapien ultricies, sagittis libero vitae, hendrerit urna. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Pellentesque convallis, nunc sed luctus aliquet, augue ligula tristique mauris, ac rhoncus lorem erat eu justo. Fusce iaculis nunc ac lectus posuere, ac efficitur quam dapibus. Phasellus vel convallis leo, ut facilisis metus. Nullam feugiat nisl et velit vehicula, eget iaculis neque commodo. Pellentesque consectetur, purus nec condimentum tempor, eros est varius erat, sed vehicula diam quam ut neque.",
          CriadoEm = new DateTime(2018, 11, 16),
          AutorId = 1,
          ResponsavelId = 2,
          Status = Status.Aberto
        },
        new Ticket
        {
          Id = 2,
          Titulo = "Aenean eget facilisis tortor",
          Descricao = "Praesent sed ex ipsum. Mauris eget sem sit amet augue blandit dapibus. Aenean volutpat justo vulputate risus faucibus, ultricies facilisis velit venenatis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Morbi at sem non tellus vulputate commodo a quis justo. Quisque arcu mauris, egestas ac quam nec, efficitur finibus ligula. Curabitur ut sapien mi. Morbi facilisis vestibulum enim volutpat pellentesque. Suspendisse metus nunc, rutrum id porttitor a, accumsan quis ipsum. Donec congue condimentum metus, nec finibus nisl bibendum et.",
          CriadoEm = new DateTime(2018, 11, 16),
          AutorId = 2,
          ResponsavelId = 1,
          Status = Status.EmAndamento
        },
        new Ticket
        {
          Id = 3,
          Titulo = "Proin tristique est sed mauris rhoncus",
          Descricao = "Vitae commodo augue pulvinar. Aenean aliquam lectus interdum ex finibus efficitur. In at enim nunc. Nunc eu scelerisque magna. Cras iaculis nisi vitae nibh laoreet, vitae volutpat tortor suscipit. Donec quis elit eu augue tincidunt lacinia quis finibus quam. Vivamus accumsan est eget dignissim hendrerit. Maecenas vehicula vel enim et ornare. Ut commodo mollis metus at vehicula.",
          CriadoEm = new DateTime(2018, 11, 16),
          AutorId = 2,
          ResponsavelId = 1,
          Status = Status.Cancelado
        },
        new Ticket
        {
          Id = 4,
          Titulo = "Nulla facilisi",
          Descricao = "Maecenas non leo eget nisi dapibus dignissim. Nam sit amet varius nisl. Vestibulum sed tincidunt dui, eget fringilla urna. Morbi ligula sem, venenatis non dignissim nec, suscipit nec ex. In hac habitasse platea dictumst. Duis sit amet tellus quis sem finibus lacinia. Nam consectetur eros imperdiet semper sollicitudin. Maecenas hendrerit, sem vitae ullamcorper interdum, lorem elit ultricies dolor, in posuere neque ipsum quis leo. Aenean dictum quis felis quis tincidunt.",
          CriadoEm = new DateTime(2018, 11, 16),
          AutorId = 2,
          ResponsavelId = 1,
          Status = Status.Resolvido
        }
      };
      tickets.ForEach(x => current.Save(x, x.Id));
    }

    public IEnumerable<T> All<T>()
      where T : class
    {
      lock (synclock)
      {
        return
          from node in cache
          where node.Item is T
          select (T)node.Item;
      }
    }

    public T Get<T>(object id)
      where T : class
    {
      lock (synclock)
      {
        var item = (
          from node in cache
          where node.Item is T
          where node.Id.Equals(id)
          select (T)node.Item
        ).FirstOrDefault();
        return item;
      }
    }

    public IEnumerable<T> Find<T>(Func<T, bool> filter)
      where T : class
    {
      lock (synclock)
      {
        return
          from node in cache
          where node.Item is T
          where filter.Invoke((T)node.Item)
          select (T)node.Item;
      }
    }

    public T FindOne<T>(Func<T, bool> filter)
      where T : class
    {
      lock (synclock)
      {
        return Find(filter).FirstOrDefault();
      }
    }

    public void Save<T>(T item, object id)
    {
      lock (synclock)
      {
        var target = (
          from node in cache
          where node.Item is T
          where node.Id.Equals(id)
          select node
        ).FirstOrDefault();

        if (target == null)
        {
          target = new Node();
          target.Id = id;
          cache.Add(target);
        }

        target.Item = item;
      }
    }

    public void Remove<T>(params object[] ids)
    {
      lock (synclock)
      {
        cache.RemoveAll(node => node.Item is T || ids.Contains(node.Id));
      }
    }

    class Node
    {
      public object Id { get; set; }

      public object Item { get; set; }
    }
  }
}
