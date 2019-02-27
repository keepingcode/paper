using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Toolset;

namespace Paper.Media.Design
{
  public static class HeaderInfoExtensions
  {
    public static object GetValue(this IHeaderInfo header, Entity entity)
    {
      var value = entity?.Properties[header.Name];
      switch (header.DataType)
      {
        case DataTypeNames.Bit:
          return Change.To<bool>(value);

        case DataTypeNames.Number:
          return Change.To<int>(value);

        case DataTypeNames.Decimal:
          return Change.To<decimal>(value);

        case DataTypeNames.Date:
        case DataTypeNames.Time:
        case DataTypeNames.Datetime:
          return Change.To<DateTime>(value);

        case DataTypeNames.Label:
        case DataTypeNames.Text:
        default:
          return Change.To<string>(value);
      }
    }
  }
}