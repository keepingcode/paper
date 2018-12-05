using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public VarKinds Kind { get; private set; }

    public object RawValue
    {
      get => _rawValue;
      set
      {
        if (value == null)
        {
          Kind = VarKinds.Null;
          _rawValue = null;
        }
        else if (value is string)
        {
          Kind = VarKinds.String;
          _rawValue = value;
        }
        else if (value is Range range)
        {
          Kind = VarKinds.Range;
          var min = Change.To(range.Min, typeRestriction);
          var max = Change.To(range.Max, typeRestriction);
          _rawValue = new Range(min, max);
        }
        else if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(Range<>))
        {
          Kind = VarKinds.Range;
          var min = Change.To(value._Get("min"), typeRestriction);
          var max = Change.To(value._Get("max"), typeRestriction);
          _rawValue = new Range(min, max);
        }
        else if (value.GetType().IsValueType)
        {
          Kind = VarKinds.Primitive;
          _rawValue = Change.To(value, typeRestriction);
        }
        else if (value is IList list)
        {
          Type type = list.GetType();
          Type elementType = type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault() ?? typeof(object);
          if (elementType != typeRestriction)
          {
            var sourceArray = list.Cast<object>().Select(x => Change.To(x, typeRestriction)).ToArray();
            var targetArray = Array.CreateInstance(typeRestriction, sourceArray.Length);
            sourceArray.CopyTo(targetArray, 0);
            list = targetArray;
          }
          Kind = VarKinds.List;
          _rawValue = list;
        }
        else
        {
          Kind = VarKinds.Graph;
          _rawValue = value;
        }
      }
    }

    public bool IsNull => (RawValue == null);

    public object Value
    {
      get => Kind.HasFlag(VarKinds.Value) ? RawValue : null;
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
