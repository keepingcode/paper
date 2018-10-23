using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Toolset.Types
{
  /// <summary>
  /// Representação de hora.
  /// Quando convertido para DateTime a porção data corresponde ao valor
  /// <see cref="System.Data.SqlTypes.SqlDateTime.MinValue"/>.
  /// </summary>
  /// <example>
  /// DateTime dataHora = new DateTime(2018, 12, 31, 23, 59, 59);
  /// Console.WriteLine(dataHora);            // produz: 2018/12/31 23:59:59
  /// 
  /// Time hora = dataHora
  /// Console.WriteLine(hora);                // produz: 23:59:59
  /// 
  /// DateTime dataHoraRecuperada = hora;
  /// Console.WriteLine(dataHoraRecuperada);  // produz: 1753/01/01 23:59:59
  /// </example>
  public struct Time
  {
    public DateTime Value { get; }

    private Time(DateTime time)
    {
      this.Value = ((DateTime)SqlDateTime.MinValue) + time.TimeOfDay;
    }

    private Time(TimeSpan time)
    {
      this.Value = ((DateTime)SqlDateTime.MinValue) + time;
    }

    public Time(int hours, int minutes, int seconds)
    {
      this.Value = ((DateTime)SqlDateTime.MinValue) + new TimeSpan(hours, minutes, seconds);
    }

    public Time(int hours, int minutes, int seconds, int milliseconds)
    {
      this.Value = ((DateTime)SqlDateTime.MinValue) + new TimeSpan(0, hours, minutes, seconds, milliseconds);
    }

    /// <summary>
    /// Determina se a instância de DateTime representa hora apenas.
    /// A instância é considerada hora apenas quando dia, mês e ano representam a data´mínima do DotNet
    /// ou do SQL.
    /// </summary>
    /// <param name="dateTime">A instância de DateTime investigada.</param>
    /// <returns>
    /// Verdadeiro se a instância de DateTime representa apenas hora apenas;
    /// Falso caso a instância de DateTime contiver informação de data.
    /// </returns>
    public static bool IsTimeOnly(DateTime dateTime)
    {
      return dateTime.Date.CompareTo((DateTime)SqlDateTime.MinValue) <= 0;
    }

    public static implicit operator Time(TimeSpan time)
    {
      return new Time(time);
    }

    public static implicit operator Time(DateTime time)
    {
      return new Time(time);
    }

    public static implicit operator TimeSpan(Time time)
    {
      return time.Value.TimeOfDay;
    }

    public static implicit operator DateTime(Time time)
    {
      return time.Value;
    }

    public override string ToString()
    {
      var format = (Value.Millisecond == 0) ? "HH:mm:ss" : "HH:mm:ss.fff";
      return Value.ToString(format);
    }

    public string ToString(string format)
    {
      return Value.ToString(format);
    }

    public string ToString(IFormatProvider formatProvider)
    {
      return Value.ToString(formatProvider);
    }
  }
}