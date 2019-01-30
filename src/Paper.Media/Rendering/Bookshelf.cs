using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;

namespace Paper.Media.Rendering
{
  public class Bookshelf
  {
    private readonly object synclock = new object();

    private PathIndex<List<Entry>> index = new PathIndex<List<Entry>>();
    private Map<string, List<string>> catalogs = new Map<string, List<string>>();

    public void AddCatalog(Catalog catalog)
    {
      AddCatalog(catalog.Id.ToString(), catalog);
    }

    public void AddCatalog(string catalogId, params IPaper[] papers)
    {
      AddCatalog(catalogId, (IEnumerable<IPaper>)papers);
    }

    public void AddCatalog(string catalogId, IEnumerable<IPaper> papers)
    {
      lock (synclock)
      {
        var paths = catalogs[catalogId];
        if (paths == null)
        {
          catalogs[catalogId] = paths = new List<string>();
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
            CatalogId = catalogId,
            Paper = paper
          });

          paths.Add(paper.Route);
        }
      }
    }

    public void RemoveCatalog(Catalog catalog)
    {
      RemoveCatalog(catalog.Id.ToString());
    }

    public void RemoveCatalog(string catalogId)
    {
      lock (synclock)
      {
        var paths = catalogs[catalogId];
        if (paths == null)
          return;

        catalogs[catalogId] = null;

        foreach (var path in paths)
        {
          var entries = index.Get(path);
          if (entries == null)
            continue;

          entries.RemoveAll(x => x.CatalogId == catalogId);
        }
      }
    }

    public bool HasCatalog(Catalog catalog)
    {
      lock (synclock)
      {
        return HasCatalog(catalog.Id.ToString());
      }
    }

    public bool HasCatalog(string catalogId)
    {
      lock (synclock)
      {
        return catalogs.ContainsKey(catalogId);
      }
    }

    public ICollection<string> GetCatalogIds()
    {
      lock (synclock)
      {
        return catalogs.Keys;
      }
    }

    public IEnumerable<IPaper> GetCatalogPapers(string catalogId)
    {
      lock (synclock)
      {
        var paths = catalogs[catalogId];
        if (paths == null)
          yield break;

        foreach (var path in paths)
        {
          var entries = index.Get(path);
          foreach (var entry in entries.Where(x => x.CatalogId == catalogId))
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

    private class Entry
    {
      public string CatalogId { get; set; }

      public IPaper Paper { get; set; }
    }
  }
}