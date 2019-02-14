using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Media.Utilities;

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
      if (item is IEnumerable list && !(item is string))
      {
        if (item is IDictionary map)
        {
          var isSerializable =
               map.Keys.Cast<object>().All(IsStringCompatible)
            && map.Values.Cast<object>().All(IsSerializable);
          return isSerializable;
        }
        else
        {
          return list.Cast<object>().All(IsSerializable);
        }
      }
      else
      {
        return item == null
            || item.GetType().IsValueType
            || IsStringCompatible(item);
      }
    }

    /// <summary>
    /// Determina se o tipo deve ser considerado compatível com string.
    /// </summary>
    /// <param name="value">O valor a ser verificado.</param>
    /// <returns>Verdadeiro se o tipo pode ser considerado string; Falso caso contrário.</returns>
    public static bool IsStringCompatible(object value)
    {
      if (value == null)
        return false;

      return value is string
          || value is Href
          || value is Uri
          || value is CaseVariantString
          || value is Guid
          || value is Version
          || value.GetType().IsEnum
          || value.GetType().FullName == "Microsoft.AspNetCore.Http.PathString";
    }
  }
}
