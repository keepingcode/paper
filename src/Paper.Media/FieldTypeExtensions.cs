using System;
using System.Collections.Generic;
using System.Text;
using Toolset;
using Toolset.Reflection;

namespace Paper.Media
{
  public static class FieldTypeExtensions
  {
    /// <summary>
    /// Obtém o nome padronizado do tipo de dado.
    /// O nome obtido equivale àquele declarado nas constantes de
    /// DataTypeNames.
    /// </summary>
    /// <param name="fieldType">O nome do tipo de dado.</param>
    /// <returns>O nome padronizado do tipo de dado</returns>
    public static string GetName(this FieldType fieldType)
    {
      var name = fieldType.ToString();
      var value = typeof(FieldTypeNames)._Get<string>(name);
      return value;
    }
  }
}