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
    private Map<string, List<string>> paperCollections = new Map<string, List<string>>();

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
          return paperCollections.Keys;
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
        var paths = paperCollections[collectionName];
        if (paths == null)
        {
          paperCollections[collectionName] = paths = new List<string>();
        }

        foreach (var item in items)
        {
          var path = GetItemPath(item);
          var entries = index.Get(path);
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
        var paths = paperCollections[collectionName];
        if (paths == null)
          return;

        paperCollections[collectionName] = null;

        foreach (var path in paths)
        {
          var entries = index.Get(path);
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
        return paperCollections.ContainsKey(collectionName);
      }
    }

    public IEnumerable<T> Find(string path)
    {
      lock (synclock)
      {
        var papers =
          from entries in index.Find(path)
          from entry in entries
          select entry.Item;
        return papers.Reverse().ToArray();
      }
    }

    public IEnumerable<T> FindInCollection(ICatalogCollection<T> collection)
    {
      return DoFindInCollection(collection.Name, null);
    }

    public IEnumerable<T> FindInCollection(string collectionName)
    {
      return DoFindInCollection(collectionName, null);
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
        var papers =
          from entries in index.Find(path)
          from entry in entries
          where (collectionName == null)
             || (collectionName == entry.CollectionName)
          select entry.Item;
        return papers.Reverse().ToArray();
      }
    }

    private string GetItemPath(T item) => pathGetter.Invoke(item);

    #region Importação de itens por composição.

    public void AddExposedPapers(IFactory factory)
    {
      AddExposedCollectionFactories(factory);
      AddExposedCollections(factory);
      AddExposedItems(factory);
    }

    private void AddExposedCollectionFactories(IFactory factory)
    {
      var types = ExposedTypes.GetTypes<ICatalogCollectionFactory<T>>();
      foreach (var type in types)
      {
        try
        {
          var collectionFactory = (ICatalogCollectionFactory<T>)factory.CreateObject(type);
          var collection = collectionFactory.CreateCollection();
          AddCollection(collection);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }

    private void AddExposedCollections(IFactory factory)
    {
      var types = ExposedTypes.GetTypes<ICatalogCollection<T>>();
      foreach (var type in types)
      {
        try
        {
          var collection = (ICatalogCollection<T>)factory.CreateObject(type);
          AddCollection(collection);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }

    private void AddExposedItems(IFactory factory)
    {
      var types = ExposedTypes.GetTypes<T>();
      foreach (var type in types)
      {
        try
        {
          var item = (T)factory.CreateObject(type);
          var collection = item._GetAttribute<CatalogAttribute>();
          var collectionName = collection?.CollectionName ?? type.Assembly.FullName.Split(',').First();
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