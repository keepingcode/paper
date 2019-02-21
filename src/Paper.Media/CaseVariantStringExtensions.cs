using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Toolset;

namespace Paper.Media
{
  public static class CaseVariantStringExtensions
  {
    private static string delimiters = "_.:-";
    private static char[] delimitersChars = delimiters.ToArray();

    #region Operations

    public static string[] Split(this CaseVariantString text, params char[] separators)
    {
      return text.Value.Split(separators);
    }

    public static string[] Split(this CaseVariantString text, string token)
    {
      return text.Value.Split(token);
    }

    public static string[] Split(this CaseVariantString text, params string[] tokens)
    {
      return text.Value.Split(tokens);
    }

    #endregion

    #region Comparisons

    /// <summary>
    /// Implementação de comparação similar ao comando LIKE do SQL.
    /// Os caracteres tem significado especial no padrão comparado:
    /// - "%", qualquer quantidade de caracteres na posição.
    /// - "_", um único caractere qualquer na posição.
    /// </summary>
    /// <param name="text">O texto comparado.</param>
    /// <param name="pattern">O padrão pesquisado.</param>
    /// <returns>Verdadeiro se o padrão corresponde ao texto, falso caso contrário.</returns>
    public static bool Like(this CaseVariantString text, string pattern)
    {
      return text.Value.Like(pattern);
    }

    public static bool LikeIgnoreCase(this CaseVariantString text, string pattern)
    {
      return text.Value.LikeIgnoreCase(pattern);
    }

    public static bool EqualsAny(this CaseVariantString text, params string[] terms)
    {
      return text.Value.EqualsAny(terms);
    }

    public static bool EqualsAny(this CaseVariantString text, IEnumerable<string> terms)
    {
      return text.Value.EqualsAny(terms);
    }

    public static bool EqualsAnyIgnoreCase(this CaseVariantString text, params string[] terms)
    {
      return text.Value.EqualsAnyIgnoreCase(terms);
    }

    public static bool EqualsAnyIgnoreCase(this CaseVariantString text, IEnumerable<string> terms)
    {
      return text.Value.EqualsAnyIgnoreCase(terms);
    }

    public static bool EqualsIgnoreCase(this CaseVariantString text, string other)
    {
      return text.Value.EqualsIgnoreCase(other);
    }

    public static bool ContainsIgnoreCase(this CaseVariantString text, string pattern)
    {
      return text.Value.ContainsIgnoreCase(pattern);
    }

    public static int IndexOfIgnoreCase(this CaseVariantString text, string pattern)
    {
      return text.Value.IndexOfIgnoreCase(pattern);
    }

    public static bool StartsWithIgnoreCase(this CaseVariantString text, string other)
    {
      return text.Value.StartsWithIgnoreCase(other);
    }

    public static bool EndsWithIgnoreCase(this CaseVariantString text, string other)
    {
      return text.Value.EndsWithIgnoreCase(other);
    }

    public static bool IsNullOrEmpty(this CaseVariantString text)
    {
      return text.Value.IsNullOrEmpty();
    }

    public static bool IsNullOrWhiteSpace(this CaseVariantString text)
    {
      return text.Value.IsNullOrWhiteSpace();
    }

    #endregion

  }
}
