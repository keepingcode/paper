using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Toolset.Data
{
  /// <summary>
  /// Funções auxiliares para instâncias de IVar.
  /// </summary>
  public static class Var
  {
    /// <summary>
    /// Obtém o tipo parametrizado de um tipo genérico.
    /// </summary>
    /// <param name="type">O tipo genérico investigado.</param>
    /// <returns>O tipo parametrizado do tipo genérico; Nulo se o tipo não for genérico.</returns>
    public static Type GetUnderlyingType(Type type)
    {
      if (!typeof(IVar).IsAssignableFrom(type))
        return null;

      var classType = type.GetGenericArguments().FirstOrDefault();
      return classType ?? typeof(object);
    }

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
  }
}
