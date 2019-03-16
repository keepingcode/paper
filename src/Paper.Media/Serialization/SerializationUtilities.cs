using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Media.Utilities;
using Toolset;

namespace Paper.Media.Serialization
{
  public static class SerializationUtilities
  {
    /// <summary>
    /// Determina se o tipo é suportado pelo algoritmo de serialização.
    /// </summary>
    /// <param name="item">O item analisado.</param>
    /// <returns>Verdadeiro se o item é serializável; Falso caso contrário.</returns>
    public static bool IsSerializable(object item)
    {
      if (item == null || item is PropertyMap)
        return true;

      if (item is IEnumerable list && !(item is string))
      {
        if (item is IDictionary map)
        {
          var isSerializable =
               map.Keys.Cast<object>().All(IsStringCompatible)
            && map.Values.Cast<object>().All(IsSerializable);
          return isSerializable;
        }

        var elementType = TypeOf.CollectionElement(item);
        if (elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
          return false;

        return list.Cast<object>().All(IsSerializable);
      }

      if (item.GetType().IsGenericType && item.GetType().GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
        return false;

      return item.GetType().IsValueType || IsStringCompatible(item);
    }

    /// <summary>
    /// Determina se o tipo deve ser considerado compatível com string.
    /// </summary>
    /// <param name="value">O valor a ser verificado.</param>
    /// <returns>Verdadeiro se o tipo pode ser considerado string; Falso caso contrário.</returns>
    public static bool IsStringCompatible(object value)
    {
      var type = (value is Type) ? (Type)value : value?.GetType();
      if (type == null)
        return false;

      return type == typeof(string)
          || type == typeof(CaseVariantString)
          || type == typeof(Href)
          || type == typeof(Uri)
          || type == typeof(UriString)
          || type == typeof(Guid)
          || type == typeof(Version)
          || type.IsEnum
          || type.FullName == "Microsoft.AspNetCore.Http.PathString";
    }
  }
}
