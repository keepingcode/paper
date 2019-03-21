using System;
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
  public enum FieldType
  {
    #region Tipos internos do HTML

    Hidden,
    Text,
    Search,
    Tel,
    Url,
    Email,
    Password,

    // "Datetime" suporta "timezone" mas o Paper ainda não suporta.
    // Use "DatetimeLocal".
    //Datetime,

    Date,
    Month,
    Week,
    Time,

    /// <summary>
    /// Data e hora local sem informação de timezone.
    /// </summary>
    DatetimeLocal,

    Number,
    Range,
    Color,
    Checkbox,
    Radio,
    File,
    Submit,

    #endregion

    #region Tipos especiais do Paper

    /// <summary>
    /// Caixa de mensagem.
    /// </summary>
    Label,

    /// <summary>
    /// Caixa de seleção.
    /// A propriedade Value contém as opções da caixa do tipo FieldValueCollection.
    /// </summary>
    Select2,

    /// <summary>
    /// Caixa de seleção de registros.
    /// A propriedade Provider contém as regras de consulta dos itens selecionáveis.
    /// </summary>
    SelectRecord,

    #endregion
  }
}