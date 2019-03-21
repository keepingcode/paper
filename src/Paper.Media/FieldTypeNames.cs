using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolset;

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

    // "Datetime" suporta "timezone" mas o Paper ainda não suporta.
    // Use "DatetimeLocal".
    //public const string Datetime = "datetime";

    public const string Date = "date";
    public const string Month = "month";
    public const string Week = "week";
    public const string Time = "time";

    /// <summary>
    /// Data e hora local sem informação de timezone.
    /// </summary>
    public const string DatetimeLocal = "datetime-local";

    public const string Number = "number";
    public const string Range = "range";
    public const string Color = "color";
    public const string Checkbox = "checkbox";
    public const string Radio = "radio";
    public const string File = "file";
    public const string Submit = "submit";

    #endregion

    #region Tipos especiais do Paper

    /// <summary>
    /// Caixa de mensagem.
    /// </summary>
    public const string Label = "__label";

    /// <summary>
    /// Caixa de seleção.
    /// A propriedade Value contém as opções da caixa do tipo FieldValueCollection.
    /// </summary>
    public const string Select = "__select";

    /// <summary>
    /// Caixa de seleção de registros.
    /// A propriedade Provider contém as regras de consulta dos itens selecionáveis.
    /// </summary>
    public const string SelectRecord = "__select-record";

    #endregion

    /// <summary>
    /// Obtém um tipo apropriado para o componente de edição do campo, conforme
    /// definido pela classe FieldTypeNames.
    /// O tipo obtido é um daqueles convencionados para o HTML5.
    /// </summary>
    /// <param name="dataTypeName">Nome do tipo do dado.</param>
    /// <returns>Nome do tipo do componente de edição relacionado.</returns>
    public static string FromDataType(DataType dataType)
    {
      return FromDataType(dataType.GetName());
    }

    /// <summary>
    /// Obtém um tipo apropriado para o componente de edição do campo, conforme
    /// definido pela classe FieldTypeNames.
    /// O tipo obtido é um daqueles convencionados para o HTML5.
    /// </summary>
    /// <param name="dataType">Nome do tipo do dado.</param>
    /// <returns>Nome do tipo do componente de edição relacionado.</returns>
    public static string FromDataType(string dataType)
    {
      switch (dataType)
      {
        case DataTypeNames.Boolean:
          return Checkbox;

        case DataTypeNames.Integer:
        case DataTypeNames.Decimal:
          return Number;

        case DataTypeNames.Date:
          return Date;

        case DataTypeNames.Time:
          return Time;

        case DataTypeNames.Datetime:
          return DatetimeLocal;

        case DataTypeNames.Binary:
          return File;

        case DataTypeNames.Record:
          return SelectRecord;

        case DataTypeNames.String:
        default:
          return Text;
      }
    }
  }
}