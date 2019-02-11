using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Toolset.Collections;
using Toolset;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;

namespace Paper.Media
{
  [CollectionDataContract(Namespace = Namespaces.Default, Name = "Properties")]
  public class PropertyCollection : Collection<Property>
  {
    public PropertyCollection()
    {
    }

    public PropertyCollection(IEnumerable<Property> items)
    : base(items)
    {
    }

    public PropertyCollection(params Property[] items)
    : base(items)
    {
    }

    public void Add(object graph)
    {
      if (graph == null)
        return;

      if (graph is Property)
      {
        base.Add((Property)graph);
        return;
      }

      var properties =
        from property in graph.GetType().GetProperties()
        where !property.GetIndexParameters().Any()
        select property;

      foreach (var property in properties)
      {
        var value = property.GetValue(graph);
        Add(property.Name, value);
      }
    }

    #region Sobreposicao de Add e Remove para garantir a unicidade de cada argumento

    protected override void OnCommitAdd(ItemStore store, IEnumerable<Property> items, int index = -1)
    {
      // removendo itens duplicados
      items = (
        from x in items
        group x by x.Name into g
        select g.Last()
      ).ToArray();

      var names = items.Select(x => x.Name);
      store.RemoveWhen(item => names.Any(name => name.EqualsIgnoreCase(item.Name)));

      if (index > -1)
      {
        base.OnCommitAdd(store, items, index);
      }
      else
      {
        var metas = items.Where(x => x.Name.StartsWith("__"));
        var nonMetas = items.Except(metas);

        var firstMeta =
          store
            .Select((item, at) => new { item, at })
            .FirstOrDefault(x => x.item.Name.StartsWith("__"));

        var firstMetaIndex = (firstMeta?.at ?? store.Count);

        base.OnCommitAdd(store, nonMetas, firstMetaIndex);
        base.OnCommitAdd(store, metas, -1);
      }
    }

    #endregion

    public static PropertyCollection Create(object graph)
    {
      var collection = new PropertyCollection();
      collection.Add(graph);
      return collection;
    }

    public string[] PropertyNames
      => this.Select(x => x.Name).ToArray();

    public Property this[string propertyName]
    {
      get => this.FirstOrDefault(x => x.Name.EqualsIgnoreCase(propertyName));
      set
      {
        value.Name = propertyName;
        this.Add(value);
      }
    }

    public Property Add(string name, object value)
    {
      var property = this.FirstOrDefault(x => x.Name.EqualsIgnoreCase(name));
      if (property == null)
      {
        Add(property = new Property { Name = name });
      }
      property.Value = value;
      return property;
    }

    public void Remove(string name)
    {
      this.RemoveWhen(x => x.Name.EqualsIgnoreCase(name));
    }

    public bool ContainsKey(string PropertyName)
    {
      return this.Any(x => x.Name.EqualsIgnoreCase(PropertyName));
    }

    public override string ToString()
    {
      return string.Join(",", this);
    }

    //#region Implementação de IKeyValueCollection

    //int IKeyValueCollection.Count { get; }

    //ICollection<string> IKeyValueCollection<string, object>.Keys => this.PropertyNames;

    //ICollection IKeyValueCollection.Keys => this.PropertyNames;

    //object IKeyValueCollection<string, object>.this[string key]
    //{
    //  get => this[key]?.Value;
    //  set => this[key].Value = value;
    //}

    //object IKeyValueCollection.this[object key]
    //{
    //  get => this[(string)key]?.Value;
    //  set => this[(string)key].Value = value;
    //}

    //void IKeyValueCollection<string, object>.Add(string key, object value) => this.Add(key, value);

    //void IKeyValueCollection.Add(object key, object value) => this.Add((string)key, value);

    //void IKeyValueCollection<string, object>.Remove(string key) => this.Remove(key);

    //void IKeyValueCollection.Remove(object key) => this.Remove((string)key);

    //#endregion
  }
}