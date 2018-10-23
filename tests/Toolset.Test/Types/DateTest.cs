using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Toolset.Types
{
  public class DateTest
  {
    [Fact]
    public void Construtor_ZerarAHora()
    {
      // Given
      DateTime dateTime = new DateTime(2020, 12, 31, 23, 59, 59, 999);
      // When
      Date date = dateTime;
      // Then
      var expected = new int[] { 0, 0, 0, 0 };
      var obtained = new int[] {
        date.Value.Hour,
        date.Value.Minute,
        date.Value.Second,
        date.Value.Millisecond
      };
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ToString_SerializarNumaFormaPadrao()
    {
      // Given
      DateTime dateTime = new DateTime(2020, 12, 31, 23, 59, 59, DateTimeKind.Utc);
      Date date = dateTime;
      // When
      var text = date.ToString();
      // Then
      var expected = "2020-12-31";
      var obtained = text;
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void IsDateOnly_QuandoAHoraForZerada_DeveIndicarDataApenas()
    {
      // Given
      DateTime dateTime = new DateTime(2020, 12, 31, 0, 0, 0, 0);
      // When
      var isDateOnly = Date.IsDateOnly(dateTime);
      // Then
      Assert.True(isDateOnly);
    }

    [Fact]
    public void IsDateOnly_QuandoAHoraNaoForZerada_NaoDeveIndicarDataApenas()
    {
      // Given
      DateTime dateTime = new DateTime(2020, 12, 31, 0, 0, 0, 001);
      // When
      var isDateOnly = Date.IsDateOnly(dateTime);
      // Then
      Assert.False(isDateOnly);
    }
  }
}