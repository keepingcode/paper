using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Commons
{
  public interface ICatalog<T>
    where T : class
  {
    ICollection<string> CollectionNames { get; }

    void AddCollection(ICatalogCollection<T> collection);

    void AddCollection(string collectionName, params T[] items);

    void AddCollection(string collectionName, IEnumerable<T> items);

    void RemoveCollection(ICatalogCollection<T> collection);

    void RemoveCollection(string collectionName);

    bool HasCollection(ICatalogCollection<T> collection);

    bool HasCollection(string collectionName);

    IEnumerable<T> Find(string path);

    IEnumerable<T> FindInCollection(ICatalogCollection<T> collection);

    IEnumerable<T> FindInCollection(string collectionName);

    IEnumerable<T> FindInCollection(ICatalogCollection<T> collection, string path);

    IEnumerable<T> FindInCollection(string collectionName, string path);
  }
}
