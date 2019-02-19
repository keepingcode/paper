using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Api.Rendering;
using Paper.Media;
using Toolset;
using Toolset.Collections;
using Toolset.Reflection;

namespace Paper.Api.Commons
{
  public class Catalog<T> : ICatalog<T>
    where T : class
  {
    private readonly object synclock = new object();

    private readonly Func<T, string> pathGetter;

    private PathIndex<List<Entry<T>>> index = new PathIndex<List<Entry<T>>>();
    private Map<string, List<string>> collections = new Map<string, List<string>>();

    public Catalog(Func<T, string> pathGetter)
    {
      this.pathGetter = pathGetter;
    }

    public ICollection<string> CollectionNames
    {
      get
      {
        lock (synclock)
        {
          return collections.Keys;
        }
      }
    }

    public void AddCollection(ICatalogCollection<T> collection)
    {
      DoAddCollection(collection.Name, collection.Items);
    }

    public void AddCollection(string collectionName, params T[] items)
    {
      DoAddCollection(collectionName, items);
    }

    public void AddCollection(string collectionName, IEnumerable<T> items)
    {
      DoAddCollection(collectionName, items);
    }

    private void DoAddCollection(string collectionName, IEnumerable<T> items)
    {
      lock (synclock)
      {
        var paths = collections[collectionName];
        if (paths == null)
        {
          collections[collectionName] = paths = new List<string>();
        }

        foreach (var item in items)
        {
          var path = GetItemPath(item);
          var entries = index.FindExact(path).FirstOrDefault();
          if (entries == null)
          {
            entries = new List<Entry<T>>();
            index.Set(path, entries);
          }

          entries.Add(new Entry<T>
          {
            CollectionName = collectionName,
            Item = item
          });

          paths.Add(path);
        }
      }
    }

    public void RemoveCollection(ICatalogCollection<T> collection)
    {
      DoRemoveCollection(collection.Name);
    }

    public void RemoveCollection(string collectionName)
    {
      DoRemoveCollection(collectionName);
    }

    private void DoRemoveCollection(string collectionName)
    {
      lock (synclock)
      {
        var paths = collections[collectionName];
        if (paths == null)
          return;

        collections[collectionName] = null;

        foreach (var path in paths)
        {
          var entries = index.FindExact(path).FirstOrDefault();
          if (entries == null)
            continue;

          entries.RemoveAll(x => x.CollectionName == collectionName);
        }
      }
    }

    public bool HasCollection(ICatalogCollection<T> collection)
    {
      return DoHasCollection(collection.Name);
    }

    public bool HasCollection(string collectionName)
    {
      return DoHasCollection(collectionName);
    }

    private bool DoHasCollection(string collectionName)
    {
      lock (synclock)
      {
        return collections.ContainsKey(collectionName);
      }
    }

    public ICollection<string> GetPaths()
    {
      return index.GetPaths().ToArray();
    }

    public ICollection<string> GetPathsInCollection(ICatalogCollection<T> collection)
    {
      return GetPathsInCollection(collection.Name);
    }

    public ICollection<string> GetPathsInCollection(string collectionName)
    {
      var paths = new List<string>();
      index.Visit((path, entry) =>
      {
        if (entry.Any(x => x.CollectionName.EqualsIgnoreCase(collectionName)))
        {
          paths.Add(path);
        }
      });
      return paths;
    }

    public IEnumerable<T> Find(string path)
    {
      lock (synclock)
      {
        var items =
          from entries in index.Find(path)
          from entry in entries
          select entry.Item;
        return items.Reverse().ToArray();
      }
    }

    public IEnumerable<T> FindExact(string path)
    {
      lock (synclock)
      {
        var items =
          from entries in index.FindExact(path)
          from entry in entries
          select entry.Item;
        return items.Reverse().ToArray();
      }
    }

    public IEnumerable<T> FindInCollection(ICatalogCollection<T> collection, string path)
    {
      return DoFindInCollection(collection.Name, path);
    }

    public IEnumerable<T> FindInCollection(string collectionName, string path)
    {
      return DoFindInCollection(collectionName, path);
    }

    private IEnumerable<T> DoFindInCollection(string collectionName, string path)
    {
      lock (synclock)
      {
        var items =
          from entries in index.Find(path)
          from entry in entries
          where (collectionName == null)
             || (collectionName == entry.CollectionName)
          select entry.Item;
        return items.Reverse().ToArray();
      }
    }

    private string GetItemPath(T item) => pathGetter.Invoke(item);

    #region Importação de itens por composição.

    public virtual void ImportExposedCollections(IObjectFactory factory)
    {
      AddExposedCollectionFactories<T>(factory, item => item);
      AddExposedCollections<T>(factory, item => item);
      AddExposedItems<T>(factory, item => item);
    }

    public virtual void ImportExposedCollections<TContract>(IObjectFactory factory, Func<TContract, T> converter)
      where TContract : class
    {
      AddExposedCollectionFactories<TContract>(factory, converter);
      AddExposedCollections<TContract>(factory, converter);
      AddExposedItems<TContract>(factory, converter);
    }

    private void AddExposedCollectionFactories<TContract>(IObjectFactory factory, Func<TContract, T> converter)
      where TContract : class
    {
      var types = ExposedTypes.GetTypes<ICatalogCollectionFactory<TContract>>();
      foreach (var type in types)
      {
        try
        {
          var collectionFactory = (ICatalogCollectionFactory<TContract>)factory.CreateObject(type);
          var collection = collectionFactory.CreateCollection();
          var items = collection.Items.Select(converter);
          AddCollection(collection.Name, items);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }

    private void AddExposedCollections<TContract>(IObjectFactory factory, Func<TContract, T> converter)
      where TContract : class
    {
      var types = ExposedTypes.GetTypes<ICatalogCollection<TContract>>();
      foreach (var type in types)
      {
        try
        {
          var collection = (ICatalogCollection<TContract>)factory.CreateObject(type);
          var items = collection.Items.Select(converter);
          AddCollection(collection.Name, items);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }

    private void AddExposedItems<TContract>(IObjectFactory factory, Func<TContract, T> converter)
      where TContract : class
    {
      var types = ExposedTypes.GetTypes<TContract>();
      foreach (var type in types)
      {
        try
        {
          var contract = (TContract)factory.CreateObject(type);
          var collection = contract._GetAttribute<CatalogAttribute>();
          var collectionName = collection?.CollectionName ?? type.Assembly.FullName.Split(',').First();
          var item = converter.Invoke(contract);
          AddCollection(collectionName, item);
        }
        catch (Exception ex)
        {
          ex.Trace();
          Console.WriteLine(ex.GetStackTrace());
        }
      }
    }

    #endregion

    private class Entry<TItem>
    {
      public string CollectionName { get; set; }

      public TItem Item { get; set; }
    }
  }
}