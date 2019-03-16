using Microsoft.CSharp;
using Paper.Media.Serialization;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;

namespace Paper.Media
{
  /// <summary>
  /// Tipos de dados conhencidos.
  /// </summary>
  public static class DataTypeNames
  {
    /// <summary>
    /// Tipo para campo texto.
    /// Nomes alternativos:
    /// - string
    /// </summary>
    public const string String = "string";

    /// <summary>
    /// Tipo para campo booliano.
    /// Nomes alternativos:
    /// - boolean
    /// </summary>
    public const string Boolean = "boolean";

    /// <summary>
    /// Tipo para campo númerico inteiro, sem dígito.
    /// Nomes alternativos:
    /// - integer
    /// - int
    /// - long
    /// </summary>
    public const string Integer = "integer";

    /// <summary>
    /// Tipo para campo numérico fracionário, com dígito.
    /// Nomes alternativos:
    /// - double
    /// - float
    /// </summary>
    public const string Decimal = "decimal";

    /// <summary>
    /// Tipo para campo data somente, sem hora.
    /// </summary>
    public const string Date = "date";

    /// <summary>
    /// Tipo para campo hora somente, sem data.
    /// </summary>
    public const string Time = "time";

    /// <summary>
    /// Tipo para campo data/hora.
    /// </summary>
    public const string Datetime = "datetime";

    /// <summary>
    /// Tipo para um vetor de bytes.
    /// </summary>
    public const string Binary = "binary";

    /// <summary>
    /// Tipo especial para seleção de registro ou coleção de registros.
    /// </summary>
    public const string Record = "record";

    /// <summary>
    /// Determina o DataType apropriado para representar o tipo ou instância indicado.
    /// </summary>
    /// <param name="typeOrInstance">O tipo ou a instância testada.</param>
    /// <returns>O DataType mais apropriado.</returns>
    public static string FromType(object typeOrInstance)
    {
      if (typeOrInstance == null)
        return null;

      var type = (typeOrInstance is Type) ? (Type)typeOrInstance : typeOrInstance.GetType();

      type = TypeOf.Var(type) ?? type;
      type = Nullable.GetUnderlyingType(type) ?? type;

      var isList = false;

      if (type.IsArray)
      {
        isList = true;
        type = type.GetElementType();
      }
      else if (typeof(IList<>).IsAssignableFrom(type))
      {
        isList = true;
        type = type.GetGenericArguments().Single();
      }

      var typeName = Canonicalize(type);

      if (isList)
        typeName += "[]";

      if (typeName.Contains("AnonymousType"))
        typeName = "AnonymousType";

      return typeName;
    }

    private static string Canonicalize(Type type)
    {
      if (type == typeof(bool))
        return Boolean;

      if (type == typeof(int))
        return Integer;

      if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
        return Decimal;

      if (type == typeof(DateTime))
        return Datetime;

      if (SerializationUtilities.IsStringCompatible(type))
        return String;

      return type.FullName.Split(',').First();
    }

    public static bool IsList(object typeOrInstance)
    {
      if (typeOrInstance == null)
        return false;

      var type = (typeOrInstance is Type) ? (Type)typeOrInstance : typeOrInstance.GetType();
      return type.IsArray || typeof(IList<>).IsAssignableFrom(type);
    }
  }
}