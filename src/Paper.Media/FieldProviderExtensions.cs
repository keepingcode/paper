using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paper.Media
{
  public static class FieldProviderExtensions
  {
    public static FieldProvider SetKeys(this FieldProvider provider, params string[] keys)
    {
      provider.Keys = keys;
      return provider;
    }

    public static FieldProvider SetKeys(this FieldProvider provider, IEnumerable<string> keys)
    {
      provider.Keys = keys.ToArray();
      return provider;
    }
  }
}