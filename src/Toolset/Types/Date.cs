using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Toolset.Types
{
  /// <summary>
  /// Representação da porção de data de uma instância de DateTime.
  /// A instância original de DateTime é preservada e pode ser resgatada por
  /// conversão implícita ou explícita.
  /// </summary>
  /// <example>
  /// DateTime dataHora = new DateTime(2018, 12, 31, 23, 59, 59);
  /// Console.WriteLine(dataHora);            // produz: 2018/12/31 23:59:59
  /// 
  /// Date data = dataHora;
  /// Console.WriteLine(data);                // produz: 2018/12/31
  /// 
  /// DateTime dataHoraRecuperada = data;
  /// Console.WriteLine(dataHoraRecuperada);  // produz: 2018/12/31 23:59:59
  /// </example>
  public struct Date
  {
    /// <summary>
    /// Data original encapsulada.
    /// </summary>
    public DateTime Value { get; }

    private Date(DateTime date)
    {
      this.Value = date.Date;
    }
    
    public Date(int year, int month, int day)
    {
      this.Value = new DateTime(year, month, day);
    }

    /// <summary>
    /// Determina se a instância de DateTime representa data apenas.
    /// A instância é considerada data apenas quando hora, minuto, segundo e milisegundo são zerados.
    /// </summary>
    /// <param name="dateTime">A instância de DateTime investigada.</param>
    /// <returns>
    /// Verdadeiro se a instância de DateTime representa apenas uma data;
    /// Falso caso a instância de DateTime contiver informação de hora.
    /// </returns>
    public static bool IsDateOnly(DateTime dateTime)
    {
      return dateTime.Millisecond == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0 && dateTime.Hour == 0;
    }

    public static implicit operator DateTime(Date date)
    {
      return date.Value;
    }

    public static implicit operator Date(DateTime date)
    {
      return new Date(date);
    }

    public override string ToString()
    {
      return Value.ToString("yyyy-MM-dd");
    }

    public string ToString(string format)
    {
      return Value.ToString(format);
    }

    public string ToString(IFormatProvider provider)
    {
      return Value.ToString(provider);
    }
  }
}