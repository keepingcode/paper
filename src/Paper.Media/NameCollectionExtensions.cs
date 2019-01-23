using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;

namespace Paper.Media
{
  public static class NameCollectionExtensions
  {
    public static bool Has(this NameCollection names, string term)
    {
      return names?.Any(x => x.EqualsIgnoreCase(term)) == true;
    }

    public static bool HasAnyOf(this NameCollection names, params string[] terms)
    {
      return names?.Any(x => x.EqualsAnyIgnoreCase(terms)) == true;
    }

    public static bool HasAllOf(this NameCollection names, params string[] terms)
    {
      return terms.All(x => names?.Any(y => x.EqualsIgnoreCase(y)) == true);
    }
  }
}
