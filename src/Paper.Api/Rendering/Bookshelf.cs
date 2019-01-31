using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public class Bookshelf
  {
    private readonly object synclock = new object();

    private PathIndex<List<Entry>> index = new PathIndex<List<Entry>>();
    private Map<string, List<string>> catalogs = new Map<string, List<string>>();

    public void AddCatalog(Catalog catalog)
    {
      AddCatalog(catalog.Name, catalog.Papers);
    }

    public void AddCatalog(string catalog, params IPaper[] papers)
    {
      AddCatalog(catalog, (IEnumerable<IPaper>)papers);
    }

    public void AddCatalog(string catalog, IEnumerable<IPaper> papers)
    {
      lock (synclock)
      {
        var paths = catalogs[catalog];
        if (paths == null)
        {
          catalogs[catalog] = paths = new List<string>();
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
            Catalog = catalog,
            Paper = paper
          });

          paths.Add(paper.Route);
        }
      }
    }

    public void RemoveCatalog(Catalog catalog)
    {
      RemoveCatalog(catalog.Name);
    }

    public void RemoveCatalog(string catalog)
    {
      lock (synclock)
      {
        var paths = catalogs[catalog];
        if (paths == null)
          return;

        catalogs[catalog] = null;

        foreach (var path in paths)
        {
          var entries = index.Get(path);
          if (entries == null)
            continue;

          entries.RemoveAll(x => x.Catalog == catalog);
        }
      }
    }

    public bool HasCatalog(Catalog catalog)
    {
      lock (synclock)
      {
        return HasCatalog(catalog.Name);
      }
    }

    public bool HasCatalog(string catalog)
    {
      lock (synclock)
      {
        return catalogs.ContainsKey(catalog);
      }
    }

    public ICollection<string> GetCatalogNames()
    {
      lock (synclock)
      {
        return catalogs.Keys;
      }
    }

    public IEnumerable<IPaper> GetCatalogPapers(string catalog)
    {
      lock (synclock)
      {
        var paths = catalogs[catalog];
        if (paths == null)
          yield break;

        foreach (var path in paths)
        {
          var entries = index.Get(path);
          foreach (var entry in entries.Where(x => x.Catalog == catalog))
          {
            yield return entry.Paper;
          }
        }
      }
    }

    public IEnumerable<IPaper> FindPaper(string path)
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

    public IEnumerable<IPaper> FindPaper(string path, string catalog)
    {
      lock (synclock)
      {
        var papers =
          from entries in index.Find(path)
          from entry in entries
          where entry.Catalog == catalog
          select entry.Paper;
        return papers.Reverse().ToArray();
      }
    }

    private class Entry
    {
      public string Catalog { get; set; }

      public IPaper Paper { get; set; }
    }
  }
}