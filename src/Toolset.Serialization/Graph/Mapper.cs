using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;
using Toolset.Reflection;

namespace Toolset.Serialization.Graph
{
  public class Mapper
  {
    internal Mapper()
    {
    }

    public T Create<T>(HashMap properties)
      where T : new()
    {
      return CopyProperties(properties, new T());
    }

    public T CopyProperties<T>(HashMap properties, T graph)
    {
      foreach (var entry in properties)
      {
        var key = entry.Key;
        var value = entry.Value;

        if (Is.Dictionary(graph))
        {
          var map = (IDictionary)graph;
          var type = TypeOf.DictionaryValue(graph);
          value = Change.To(value, type);
          map[key] = value;
        }
        else
        {
          var info = graph._GetPropertyInfo(key);
          var type = info?.PropertyType;
          if (type == null)
          {
            var name = graph.GetType().FullName.Split(',').First();
            throw new SerializationException(
              $"A propriedade não existe no objeto destino: {name}.{key}"
            );
          }

          if (value is HashMap map && !type.IsAssignableFrom(value.GetType()))
          {
            value = Activator.CreateInstance(type);
            CopyProperties(map, value);
          }

          graph._Set(key, value);
        }
      }
      return graph;
    }
  }
}