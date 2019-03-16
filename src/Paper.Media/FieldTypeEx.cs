using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media
{
  public class FieldTypeEx
  {
    public FieldTypeEx(string fieldType, string dataType)
    {
      FieldType = fieldType;
      DataType = dataType;
    }

    public string FieldType { get; set; }
    public string DataType { get; set; }

    //
    // Nota:
    // - O tipo Radio ainda não é suportado.
    //

    public static readonly FieldTypeEx HiddenString = new FieldTypeEx(FieldTypeNames.Hidden, DataTypeNames.String);
    public static readonly FieldTypeEx HiddenInteger = new FieldTypeEx(FieldTypeNames.Hidden, DataTypeNames.Integer);
    public static readonly FieldTypeEx HiddenDecimal = new FieldTypeEx(FieldTypeNames.Hidden, DataTypeNames.Decimal);
    public static readonly FieldTypeEx HiddenDateTime = new FieldTypeEx(FieldTypeNames.Hidden, DataTypeNames.DateTime);
    public static readonly FieldTypeEx HiddenDate = new FieldTypeEx(FieldTypeNames.Hidden, DataTypeNames.Date);
    public static readonly FieldTypeEx HiddenTime = new FieldTypeEx(FieldTypeNames.Hidden, DataTypeNames.Time);
    public static readonly FieldTypeEx HiddenBoolean = new FieldTypeEx(FieldTypeNames.Hidden, DataTypeNames.Boolean);

    public static readonly FieldTypeEx Submit = new FieldTypeEx(FieldTypeNames.Submit, null);
    public static readonly FieldTypeEx SubmitString = new FieldTypeEx(FieldTypeNames.Submit, DataTypeNames.String);
    public static readonly FieldTypeEx SubmitInteger = new FieldTypeEx(FieldTypeNames.Submit, DataTypeNames.Integer);
    public static readonly FieldTypeEx SubmitDecimal = new FieldTypeEx(FieldTypeNames.Submit, DataTypeNames.Decimal);
    public static readonly FieldTypeEx SubmitDate = new FieldTypeEx(FieldTypeNames.Submit, DataTypeNames.Date);
    public static readonly FieldTypeEx SubmitTime = new FieldTypeEx(FieldTypeNames.Submit, DataTypeNames.Time);
    public static readonly FieldTypeEx SubmitDatetime = new FieldTypeEx(FieldTypeNames.Submit, DataTypeNames.Datetime);
    public static readonly FieldTypeEx SubmitBoolean = new FieldTypeEx(FieldTypeNames.Submit, DataTypeNames.Boolean);

    // Tipos Select* requerem opcoes adicionais definidas no campo Value do Field
    public static readonly FieldTypeEx SelectString = new FieldTypeEx(null, DataTypeNames.String);
    public static readonly FieldTypeEx SelectInteger = new FieldTypeEx(null, DataTypeNames.Integer);
    public static readonly FieldTypeEx SelectDecimal = new FieldTypeEx(null, DataTypeNames.Decimal);
    public static readonly FieldTypeEx SelectDate = new FieldTypeEx(null, DataTypeNames.Date);
    public static readonly FieldTypeEx SelectTime = new FieldTypeEx(null, DataTypeNames.Time);
    public static readonly FieldTypeEx SelectDatetime = new FieldTypeEx(null, DataTypeNames.Datetime);
    public static readonly FieldTypeEx SelectBoolean = new FieldTypeEx(null, DataTypeNames.Boolean);

    public static readonly FieldTypeEx String = new FieldTypeEx(FieldTypeNames.Text, DataTypeNames.String);
    public static readonly FieldTypeEx Tel = new FieldTypeEx(FieldTypeNames.Tel, DataTypeNames.String);
    public static readonly FieldTypeEx SearchString = new FieldTypeEx(FieldTypeNames.Search, DataTypeNames.String);
    public static readonly FieldTypeEx Url = new FieldTypeEx(FieldTypeNames.Url, DataTypeNames.String);
    public static readonly FieldTypeEx Email = new FieldTypeEx(FieldTypeNames.Email, DataTypeNames.String);
    public static readonly FieldTypeEx Password = new FieldTypeEx(FieldTypeNames.Password, DataTypeNames.String);
    public static readonly FieldTypeEx ColorString = new FieldTypeEx(FieldTypeNames.Color, DataTypeNames.String);

    public static readonly FieldTypeEx Integer = new FieldTypeEx(FieldTypeNames.Number, DataTypeNames.Integer);
    public static readonly FieldTypeEx Decimal = new FieldTypeEx(FieldTypeNames.Number, DataTypeNames.Decimal);

    public static readonly FieldTypeEx IntegerRange = new FieldTypeEx(FieldTypeNames.Range, DataTypeNames.Integer);

    public static readonly FieldTypeEx Date = new FieldTypeEx(FieldTypeNames.Date, DataTypeNames.Date);
    public static readonly FieldTypeEx Time = new FieldTypeEx(FieldTypeNames.Time, DataTypeNames.Time);

    // Contem informacao de timezone
    public static readonly FieldTypeEx Datetime = new FieldTypeEx(FieldTypeNames.Datetime, DataTypeNames.Datetime);
    // Nao contem informacao de timezone
    public static readonly FieldTypeEx DatetimeLocal = new FieldTypeEx(FieldTypeNames.DatetimeLocal, DataTypeNames.Datetime);

    public static readonly FieldTypeEx Month = new FieldTypeEx(FieldTypeNames.Month, DataTypeNames.Date);
    public static readonly FieldTypeEx Week = new FieldTypeEx(FieldTypeNames.Week, DataTypeNames.Date);

    public static readonly FieldTypeEx Boolean = new FieldTypeEx(FieldTypeNames.Checkbox, DataTypeNames.Boolean);

    public static readonly FieldTypeEx File = new FieldTypeEx(FieldTypeNames.File, DataTypeNames.Binary);

    // Record requer a definicao do Provider no Field
    public static readonly FieldTypeEx Record = new FieldTypeEx(null, DataTypeNames.Record);
  }
}