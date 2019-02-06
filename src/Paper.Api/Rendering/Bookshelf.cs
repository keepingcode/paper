using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public class Bookshelf : IBookshelf
  {
    private readonly object synclock = new object();

    private PathIndex<List<Entry>> index = new PathIndex<List<Entry>>();
    private Map<string, List<string>> paperCollections = new Map<string, List<string>>();

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

    public void AddCollection(PaperCollection collection)
    {
      AddCollection(collection.Name, collection.Papers);
    }

    public void AddCollection(string collectionName, params IPaper[] papers)
    {
      AddCollection(collectionName, (IEnumerable<IPaper>)papers);
    }

    public void AddCollection(string collectionName, IEnumerable<IPaper> papers)
    {
      lock (synclock)
      {
        var paths = paperCollections[collectionName];
        if (paths == null)
        {
          paperCollections[collectionName] = paths = new List<string>();
        }

        foreach (var paper in papers)
        {
          var entries = index.Get(paper.Route);
          if (entries == null)
          {
            entries = new List<Entry>();
            index.Set(paper.Route, entries);
          }

          entries.Add(new Entry
          {
            CollectionName = collectionName,
            Paper = paper
          });

          paths.Add(paper.Route);
        }
      }
    }

    public void RemoveCollection(PaperCollection collection)
    {
      RemoveCollection(collection.Name);
    }

    public void RemoveCollection(string collectionName)
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

    public bool HasCollection(PaperCollection collection)
    {
      lock (synclock)
      {
        return HasCollection(collection.Name);
      }
    }

    public bool HasCollection(string collectionName)
    {
      lock (synclock)
      {
        return paperCollections.ContainsKey(collectionName);
      }
    }

    public IEnumerable<IPaper> FindPapers(string path)
    {
      lock (synclock)
      {
        var papers =
          from entries in index.Find(path)
          from entry in entries
          select entry.Paper;
        return papers.Reverse().ToArray();
      }
    }

    public IEnumerable<IPaper> FindPapersInCollection(string collectionName)
    {
      lock (synclock)
      {
        var paths = paperCollections[collectionName];
        if (paths == null)
          yield break;

        foreach (var path in paths)
        {
          var entries = index.Get(path);
          foreach (var entry in entries.Where(x => x.CollectionName == collectionName))
          {
            yield return entry.Paper;
          }
        }
      }
    }

    public IEnumerable<IPaper> FindPapersInCollection(string collectionName, string path)
    {
      lock (synclock)
      {
        var papers =
          from entries in index.Find(path)
          from entry in entries
          where entry.CollectionName == collectionName
          select entry.Paper;
        return papers.Reverse().ToArray();
      }
    }

    private class Entry
    {
      public string CollectionName { get; set; }

      public IPaper Paper { get; set; }
    }
  }
}