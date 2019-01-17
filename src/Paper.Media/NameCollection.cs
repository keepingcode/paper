using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Toolset;
using Toolset.Collections;

namespace Paper.Media
{
  /// <summary>
  /// Coleção de nomes do Paper.Media.
  /// A coleção suporta a conversão implícita de uma lista
  /// definida em string com elementos separados por vírgula,
  /// ponto-e-vírgula ou barra vertical (|).
  /// </summary>
  [CollectionDataContract(Namespace = Namespaces.Default, Name = "Names", ItemName = "Name")]
  public class NameCollection : Collection<string>
  {
    public NameCollection()
    {
    }

    public NameCollection(IEnumerable<string> items)
      : base(items)
    {
    }

    public NameCollection(params string[] items)
      : base(items)
    {
    }

    public bool Has(string term)
    {
      return this.Any(x => x.EqualsIgnoreCase(term));
    }

    public bool HasAnyOf(params string[] terms)
    {
      return this.Any(x => x.EqualsAnyIgnoreCase(terms));
    }

    public bool HasAllOf(params string[] terms)
    {
      return terms.All(x => this.Any(y => x.EqualsIgnoreCase(y)));
    }

    protected override void OnCommitAdd(ItemStore store, IEnumerable<string> items, int index = -1)
    {
      items = ParseNames2(items).ToArray();
      if (index > -1)
      {
        base.OnCommitAdd(store, items, index);
      }
      else
      {
        var camelItems = items.Where(item => char.IsLower(item.FirstOrDefault()));
        var otherItems = items.Except(camelItems);

        camelItems = camelItems.Except(store);
        otherItems = otherItems.Except(store);

        base.OnCommitAdd(store, camelItems, 0);
        base.OnCommitAdd(store, otherItems, -1);
      }
    }

    public override string ToString()
    {
      return string.Join(",", this);
    }

    public static implicit operator NameCollection(string items)
    {
      return new NameCollection(items);
    }

    public static implicit operator NameCollection(string[] items)
    {
      return new NameCollection(items);
    }

    private static IEnumerable<string> ParseNames2(IEnumerable<string> tokens)
    {
      var names =
        from token in tokens.NonNull()
        from name in token.Split(',', ';', '|').NonNullOrWhitespace()
        select name;
      foreach (var name in names)
      {
        yield return name.Trim();
      }
    }
  }
}