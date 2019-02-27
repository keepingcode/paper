using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolset;

namespace Paper.Browser.Gui
{
  public static class Formatter
  {
    public static string Format(object value)
    {
      if (value == null || value is string || value is int)
      {
        return value?.ToString() ?? "";
      }

      if (value is bool bit)
      {
        return bit ? "Ativado" : "Desativado";
      }

      if (value is double || value is decimal || value is float)
      {
        return $"{value:#,##0.00}";
      }

      if (value is DateTime date)
      {
        var hasTime = date.Hour != 0
                   || date.Minute != 0
                   || date.Second != 0
                   || date.Millisecond != 0;
        return date.ToString(hasTime ? "dd/MM/yyyy HH:mm:ss" : "dd/MM/yyyy");
      }

      return Change.To<string>(value);
    }
  }
}
