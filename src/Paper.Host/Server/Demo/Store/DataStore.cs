using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Paper.Host.Server.Demo.Domain;
using Paper.Host.Server.Demo.Papers.Model;
using Toolset.Collections;

namespace Paper.Host.Server.Demo.Store
{
  public class DataStore
  {
    private string[] SmallStore;
    private string[] LargeStore;

    public static DataStore Current { get; } = new DataStore();

    private readonly List<Node> cache = new List<Node>();

    private readonly object synclock = new object();

    private DataStore()
    {
      LoadEmbeddedStore();
      FillUp();
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

    public T FindOne<T>(object id)
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

    private void LoadEmbeddedStore()
    {
      this.SmallStore = LoadEmbeddedLines("data-store.small.txt").ToArray();
      this.LargeStore = LoadEmbeddedLines("data-store.large.txt").ToArray();
    }

    private IEnumerable<string> LoadEmbeddedLines(string filename)
    {
      var assembly = GetType().Assembly;
      var manifest = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(filename));
      using (var stream = assembly.GetManifestResourceStream(manifest))
      using (var reader = new StreamReader(stream))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          if (!string.IsNullOrWhiteSpace(line))
          {
            yield return line;
          }
        }
      }
    }

    private void FillUp()
    {
      int key;
      var rnd = new Random(0);

      //
      // Usuários
      //
      new[]
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
          Login = "fulano",
          Nome = "Fulano de Tal",
        },
        new Usuario
        {
          Id = 3,
          Login = "betrano",
          Nome = "Beltrano das Neves",
        },
        new Usuario
        {
          Id = 4,
          Login = "sicrano",
          Nome = "Sicrano Rico",
        }
      }.ForEach(usuario => Save(usuario, usuario.Id));

      var usuarios = this.All<Usuario>().ToArray();

      //
      // Tickets
      //
      key = 0;
      var statuses = Enum.GetValues(typeof(Status));
      var maxTickets = this.LargeStore.Length;
      for (int i = 0; i < maxTickets; i++)
      {
        var day = (i % 20) + 5;
        var seconds = (7 * 60 * 60) + rnd.Next(9 * 60 * 60);

        var status = (Status)statuses.GetValue(rnd.Next(statuses.Length));
        var autor = usuarios[rnd.Next(usuarios.Length)];
        var responsavel = usuarios.Except(autor).ElementAt(rnd.Next(usuarios.Length - 1));
        
        var ticket = new Ticket
        {
          Id = ++key,
          Titulo = this.SmallStore[i],
          Descricao = this.LargeStore[i],
          CriadoEm = new DateTime(2018, 11, day).AddSeconds(seconds),
          AutorId = autor.Id,
          ResponsavelId = responsavel.Id,
          Status = status
        };

        Save(ticket, ticket.Id);
      }

      //
      // Comentarios
      //
      key = 0;
      var tickets = this.All<Ticket>().ToArray();
      var ticketsAbertosCount = tickets.Count(ticket => ticket.Status == Status.Aberto);
      foreach (var ticket in tickets.Where(ticket => ticket.Status != Status.Aberto))
      {
        var criadoEm = ticket.CriadoEm;

        var maxComentarios = rnd.Next(30);
        for (int i = 0; i < maxComentarios; i++)
        {
          var seconds = rnd.Next(8 * 60 * 60);
          criadoEm = criadoEm.AddSeconds(seconds);
          var autor = usuarios[rnd.Next(usuarios.Length)];

          var odd = rnd.Next(3);
          var descricao =
            (odd == 0)
              ? this.LargeStore[rnd.Next(this.LargeStore.Length)]
              : this.SmallStore[rnd.Next(this.SmallStore.Length)];

          var comentario = new Comentario
          {
            Id = ++key,
            TicketId = ticket.Id,
            Descricao = descricao,
            CriadoEm = criadoEm,
            AutorId = autor.Id
          };

          Save(comentario, comentario.Id);
        }
      }
    }

    class Node
    {
      public object Id { get; set; }

      public object Item { get; set; }
    }
  }
}
