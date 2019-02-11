using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Toolset.Collections
{
  public class HashMap : Map<string, object>
  {
    public HashMap()
      : base(StringComparer.OrdinalIgnoreCase)
    {
    }

    public HashMap(int capacity)
      : base(StringComparer.OrdinalIgnoreCase, capacity)
    {
    }

    public HashMap(IEnumerable<KeyValuePair<string, object>> entries)
      : base(StringComparer.OrdinalIgnoreCase, entries)
    {
    }

    public HashMap(IEqualityComparer<string> keyComparer)
      : base(keyComparer)
    {
    }

    public HashMap(IEqualityComparer<string> keyComparer, int capacity)
      : base(keyComparer, capacity)
    {
    }

    public HashMap(IEqualityComparer<string> keyComparer, IEnumerable<KeyValuePair<string, object>> entries)
      : base(keyComparer, entries)
    {
    }
  }
}