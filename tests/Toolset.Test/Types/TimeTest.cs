using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text;
using Xunit;

namespace Toolset.Types
{
  public class TimeTest
  {
    [Fact]
    public void Construtor_ZerarAData()
    {
      // Given
      DateTime dateTime = new DateTime(2020, 12, 31, 23, 59, 59, 999);
      // When
      Time time = dateTime;
      // Then
      var expected = new int[] {
        ((DateTime)SqlDateTime.MinValue).Day,
        ((DateTime)SqlDateTime.MinValue).Month,
        ((DateTime)SqlDateTime.MinValue).Year
      };
      var obtained = new int[] {
        time.Value.Day,
        time.Value.Month,
        time.Value.Year
      };
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ToString_SerializarNumaFormaPadrao()
    {
      // Given
      DateTime dateTime = new DateTime(2020, 12, 31, 23, 59, 59, 999);
      Time time = dateTime;
      // When
      var text = time.ToString();
      // Then
      var expected = "23:59:59.999";
      var obtained = text;
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void IsTimeOnly_QuandoADataForAMinimaDoDotNet_DeveIndicarHoraApenas()
    {
      // Given
      DateTime min = DateTime.MinValue;
      DateTime dateTime = new DateTime(min.Year, min.Month, min.Day, 23, 59, 59, 59);
      //When
      var isTimeOnly = Time.IsTimeOnly(dateTime);
      // Then
      Assert.True(isTimeOnly);
    }

    [Fact]
    public void IsTimeOnly_QuandoADataForMinimaDoSql_DeveIndicarHoraApenas()
    {
      // Given
      DateTime min = (DateTime)SqlDateTime.MinValue;
      DateTime dateTime = new DateTime(min.Year, min.Month, min.Day, 23, 59, 59, 59);
      //When
      var isTimeOnly = Time.IsTimeOnly(dateTime);
      // Then
      Assert.True(isTimeOnly);
    }

    [Fact]
    public void IsTimeOnly_QuandoADataForMinima_NaoDeveIndicarHoraApenas()
    {
      // Given
      DateTime dateTime = new DateTime(2020, 12, 31, 23, 59, 59, 59);
      //When
      var isTimeOnly = Time.IsTimeOnly(dateTime);
      // Then
      Assert.False(isTimeOnly);
    }
  }
}