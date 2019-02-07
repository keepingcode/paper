using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Paper.Api.Commons;

namespace Paper.Api.Commons
{
  [DataContract(Namespace = "")]
  public class CatalogCollection<T> : ICatalogCollection<T>
    where T : class
  {
    private List<T> _items = new List<T>();

    public CatalogCollection()
    {
    }

    public CatalogCollection(string name)
    {
      this.Name = name;
    }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public ICollection<T> Items
    {
      get => _items;
      set => _items = value as List<T> ?? new List<T>(value);
    }
  }
}