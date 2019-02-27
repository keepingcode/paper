﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Media
{
  /// <summary>
  /// Tipos de componentes de campos conhecidos.
  /// São aceitos apenas os tipos convencionados para o HTML5.
  /// </summary>
  public static class FieldTypeNames
  {
    #region Tipos internos do HTML

    public const string Hidden = "hidden";
    public const string Text = "text";
    public const string Search = "search";
    public const string Tel = "tel";
    public const string Url = "url";
    public const string Email = "email";
    public const string Password = "password";
    public const string Datetime = "datetime";
    public const string Date = "date";
    public const string Month = "month";
    public const string Week = "week";
    public const string Time = "time";
    public const string DatetimeLocal = "datetimeLocal";
    public const string Number = "number";
    public const string Range = "range";
    public const string Color = "color";
    public const string Checkbox = "checkbox";
    public const string Radio = "radio";
    public const string File = "file";
    public const string Submit = "submit";

    #endregion

    #region Tipos especiais do Paper

    public const string Binding = "binding";

    #endregion

    /// <summary>
    /// Obtém um tipo apropriado para o componente de edição do campo, conforme
    /// definido pela classe FieldTypeNames.
    /// O tipo obtido é um daqueles convencionados para o HTML5.
    /// </summary>
    /// <param name="dataTypeName">Nome do tipo do dado.</param>
    /// <returns>Nome do tipo do componente de edição relacionado.</returns>
    public static string GetFieldTypeFromDataType(string dataTypeName)
    {
      switch (dataTypeName)
      {
        case "boolean":
        case "bit":
        case "integer":
        case "int":
        case "long":
        case "number":
        case "double":
        case "float":
        case "decimal":
          return Number;

        case "date":
          return Date;

        case "time":
          return Time;

        case "datetime":
          return Datetime;

        default:
          return Text;
      }
    }
  }
}
