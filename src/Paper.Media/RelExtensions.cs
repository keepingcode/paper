using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;

namespace Paper.Media
{
  public static class RelExtensions
  {
    /// <summary>
    /// Obtém o nome padronizado do relacionamento.
    /// O nome obtido equivale àquele declarado nas constantes de
    /// RelNames.
    /// </summary>
    /// <param name="rel">O nome da relação.</param>
    /// <returns>O nome padronizado do relacionamento</returns>
    public static string GetName(this Rel rel)
    {
      return rel.ToString().ChangeCase(TextCase.CamelCase);
    }

    public static bool Has(this NameCollection names, Rel rel)
    {
      var term = rel.ToString();
      return names?.Any(x => x.EqualsIgnoreCase(term.ToString())) == true;
    }

    public static bool HasAnyOf(this NameCollection names, params Rel[] rels)
    {
      var terms = rels.Select(x => x.ToString());
      return names?.Any(x => x.EqualsAnyIgnoreCase(terms)) == true;
    }

    public static bool HasAllOf(this NameCollection names, params Rel[] rels)
    {
      var terms = rels.Select(x => x.ToString());
      return terms.All(x => names?.Any(y => x.EqualsIgnoreCase(y)) == true);
    }
  }
}