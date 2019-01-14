using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Paper.Media.Design.Papers;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Reflection;

namespace Sandbox.Bot.Api
{
  static class EntityParser
  {
    public static Entity ParseEntity(Stream inputStream)
    {
      var serializer = new MediaSerializer();
      var entity = serializer.Deserialize(inputStream);
      return entity;
    }

    public static T ParseEntity<T>(Entity entity)
      where T : class, new()
    {
      var graph = new T();
      ParseProperties(entity.Properties, graph);
      return graph;
    }

    private static void ParseProperties(PropertyCollection source, object target)
    {
      foreach (var key in target._GetPropertyNames())
      {
        var type = target._GetPropertyType(key);
        var isGraph = IsGraph(type);
        if (isGraph)
        {
          var writable = target._CanWrite(key);
          if (writable
           && source.ContainsKey(key)
           && source[key].Value is PropertyCollection newSource)
          {
            var newTarget = target._SetNew(key);
            ParseProperties(newSource, newTarget);
          }
        }
        else
        {
          if (source.ContainsKey(key))
          {
            var value = source[key].Value;
            target._Set(key, value);
          }
        }
      }
    }

    private static bool IsGraph(Type type)
    {
      return !type.Namespace.StartsWith("System");
    }
  }
}
