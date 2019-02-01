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

    protected override void OnCommitAdd(ItemStore store, IEnumerable<string> items, int index = -1)
    {
      var groups =
        from item in ParseNames2(items)
        let isCamelCase = char.IsLower(item.FirstOrDefault())
        group item by isCamelCase into g
        select new { isCamelCase = g.Key, items = g };

      var divider =
         store.TakeWhile(item => !char.IsUpper(item.FirstOrDefault())).Select((item, i) => i + 1).LastOrDefault();

      foreach (var group in groups)
      {
        if (group.isCamelCase)
        {
          var position = (index == -1 || index > divider) ? divider : index;
          base.OnCommitAdd(store, group.items, position);
        }
        else
        {
          var position = (index == -1) ? -1 : index + divider;
          base.OnCommitAdd(store, group.items, position);
        }
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