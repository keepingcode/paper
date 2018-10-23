using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Toolset.Reflection;

namespace Toolset.Types
{
  public class Var
  {
    private readonly Type typeRestriction;

    private object _rawValue;

    public Var()
    {
      this.typeRestriction = typeof(object);
    }

    public Var(object rawValue)
    {
      this.typeRestriction = typeof(object);
      this.RawValue = rawValue;
    }

    protected Var(Type typeRestriction)
    {
      this.typeRestriction = typeRestriction;
    }

    public VarKind Kind { get; private set; }

    public object RawValue
    {
      get => _rawValue;
      set
      {
        if (value == null)
        {
          Kind = VarKind.Null;
          _rawValue = null;
        }
        else if (value is string)
        {
          Kind = VarKind.String;
          _rawValue = value;
        }
        else if (value is Range)
        {
          Kind = VarKind.Range;
          _rawValue = Range = (Range)value;
        }
        else if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(Range<>))
        {
          Kind = VarKind.Range;
          var min = value._Get("min");
          var max = value._Get("max");
          _rawValue = Range = new Range(min, max);
        }
        else if (value.GetType().IsValueType)
        {
          Kind = VarKind.Primitive;
          _rawValue = value;
        }
        else if (value is IList list)
        {
          Kind = VarKind.List;
          _rawValue = List = list;
        }
        else
        {
          Kind = VarKind.Graph;
          _rawValue = value;
        }
      }
    }

    public bool IsNull => (RawValue == null);

    public object Value
    {
      get => !(RawValue is IList || RawValue is Range) ? RawValue : null;
      set => RawValue = value;
    }

    public IList List
    {
      get => RawValue is IList list ? list : null;
      set => RawValue = value;
    }

    public Range Range
    {
      get => RawValue is Range range ? range : default(Range);
      set => RawValue = value;
    }

    #region Métodos utilitários
    
    /// <summary>
    /// Constrói uma expressão regular que representa o padrão de texto.
    /// O padrão de texto pode conter dois dos curingas comumente usados no comando
    /// LIKE do SQL: O curinga "%" que representa qualquer quantidade de texto na posição e o
    /// curinga "*" que representa um único caracter qualquer na posição.
    /// </summary>
    /// <param name="text">
    /// O padrão de texto analisado.
    /// O padrão de texto pode conter dois dos curingas comumente usados no comando
    /// LIKE do SQL: O curinga "%" que representa qualquer quantidade de texto na posição e o
    /// curinga "*" que representa um único caracter qualquer na posição.
    /// </param>
    /// <returns>
    /// A expressão regular que representa o padrão de texto.
    /// </returns>
    public static string CreateTextPattern(string text)
    {
      return (text != null)
        ? $"^{Regex.Escape(text).Replace("%", ".*").Replace("_", ".")}$"
        : "";
    }

    /// <summary>
    /// Determina se o padrão de texto indicado contém caracteres curinga.
    /// O padrão de texto pode conter dois dos curingas comumente usados no comando
    /// LIKE do SQL: O curinga "%" que representa qualquer quantidade de texto na posição e o
    /// curinga "*" que representa um único caracter qualquer na posição.
    /// </summary>
    /// <param name="text">
    /// O padrão de texto analisado.
    /// O padrão de texto pode conter dois dos curingas comumente usados no comando
    /// LIKE do SQL: O curinga "%" que representa qualquer quantidade de texto na posição e o
    /// curinga "*" que representa um único caracter qualquer na posição.
    /// </param>
    /// <returns>Verdadeiro se o texto contém os curingas; Falso caso contrário.</returns>
    public static bool HasWildcards(string text)
    {
      return text?.Contains("%") == true || text?.Contains("_") == true;
    }

    #endregion
  }
}
