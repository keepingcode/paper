using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  public interface IBookshelf
  {
    ICollection<string> CollectionNames { get; }

    void AddCollection(PaperCollection collection);

    void AddCollection(string collectionName, params IPaper[] papers);

    void AddCollection(string collectionName, IEnumerable<IPaper> papers);

    void RemoveCollection(PaperCollection collection);

    void RemoveCollection(string collectionName);

    bool HasCollection(PaperCollection collection);

    bool HasCollection(string collectionName);

    IEnumerable<IPaper> FindPapers(string path);

    IEnumerable<IPaper> FindPapersInCollection(string collectionName);

    IEnumerable<IPaper> FindPapersInCollection(string collectionName, string path);
  }
}
