using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Toolset;
using Toolset.Collections;
using Paper.Media.Utilities;
using Paper.Media.Serialization;
using Toolset.Reflection;

namespace Paper.Media
{
  [CollectionDataContract(
      Namespace = Namespaces.Default
    , Name = "Properties"
    , ItemName = "Property"
    , KeyName = "Name"
    , ValueName = "Value"
  )]
  [KnownType(typeof(PropertyMap))]
  [KnownType(typeof(NameCollection))]
  [KnownType(typeof(DictionaryEntry))]
  [KnownType(typeof(PropertyValueCollection))]
  public class PropertyMap : HashMap<object>, IPropertyMap
  {
    PropertyMap IPropertyMap.Properties
    {
      get => this;
      set => throw new MediaException("A propriedade é somente leitura: Properties");
    }

    protected override void OnAdd(Dictionary<string, object> store, string key, object value)
    {
      base.OnAdd(store, key, UnwrapTerm(value, null, null));
    }

    public static object CreateCompatibleValue(object value, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      var selectKeyPaths = select?.Select(x => x.Split('.'));
      var exceptKeyPaths = except?.Select(x => x.Split('.'));
      return UnwrapTerm(value, selectKeyPaths, exceptKeyPaths);
    }

    private static object UnwrapTerm(object value, IEnumerable<IEnumerable<string>> select, IEnumerable<IEnumerable<string>> except)
    {
      if (!SerializationUtilities.IsSerializable(value) || value is PropertyMap)
      {
        if (value is IDictionary dictionary)
        {
          value = UnwrapDictionary(dictionary, select, except);
        }
        else if (value is IEnumerable list)
        {
          value = UnwrapArray(list, select, except);
        }
        else
        {
          value = UnwrapGraph(value, select, except);
        }
      }
      return value;
    }

    private static PropertyMap UnwrapGraph(object graph, IEnumerable<IEnumerable<string>> select, IEnumerable<IEnumerable<string>> except)
    {
      var accept = select?.Select(x => x.FirstOrDefault()).NonNull();
      var ignore = except?.Where(x => x.Count() == 1).Select(x => x.First()).NonNull();

      var map = new PropertyMap();

      var keys =
        from key in graph._GetPropertyNames()
        where accept?.Any() != true || key.EqualsAnyIgnoreCase(accept)
        where ignore?.Any() != true || !key.EqualsAnyIgnoreCase(ignore)
        select key;

      foreach (var key in keys)
      {
        var value = graph._Get(key);
        if (value != null)
        {
          var deepSelect = 
            select?.Where(x => x.FirstOrDefault()?.EqualsIgnoreCase(key) == true).Select(x => x.Skip(1));
          var deepExcept = 
            except?.Where(x => x.FirstOrDefault()?.EqualsIgnoreCase(key) == true).Select(x => x.Skip(1));
          map[key] = UnwrapTerm(value, deepSelect, deepExcept);
        }
      }
      return map;
    }

    private static PropertyMap UnwrapDictionary(IDictionary dictionary, IEnumerable<IEnumerable<string>> select, IEnumerable<IEnumerable<string>> except)
    {
      var map = new PropertyMap();
      var accept = select?.Select(x => x.FirstOrDefault()).NonNull();
      var ignore = except?.Where(x => x.Count() == 1).Select(x => x.First()).NonNull();

      var keys =
        from key in dictionary.Keys.Cast<string>()
        where accept?.Any() != true || key.EqualsAnyIgnoreCase(accept)
        where ignore?.Any() != true || !key.EqualsAnyIgnoreCase(ignore)
        select key;

      foreach (string key in keys)
      {
        var value = dictionary[key];

        var deepSelect =
          select?.Where(x => x.FirstOrDefault()?.EqualsIgnoreCase(key) == true).Select(x => x.Skip(1));
        var deepExcept =
          except?.Where(x => x.FirstOrDefault()?.EqualsIgnoreCase(key) == true).Select(x => x.Skip(1));

        value = UnwrapTerm(value, deepSelect, deepExcept);

        map.Add(key, value);
      }
      return map;
    }

    private static PropertyValueCollection UnwrapArray(IEnumerable array, IEnumerable<IEnumerable<string>> select, IEnumerable<IEnumerable<string>> except)
    {
      var items = array.Cast<object>();
      var list = new PropertyValueCollection(items.Count());
      foreach (var item in items)
      {
        var value = UnwrapTerm(item, select, except);
        list.Add(value);
      }
      return list;
    }
  }
}