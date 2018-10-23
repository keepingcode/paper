using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Toolset.Types
{
  public class RangeTest
  {
    [Fact]
    public void Construtor_ParaTipoClass_DeveInstanciar()
    {
      // Given
      string min = "1960";
      string max = "2020";
      // When
      var between = new Range<string>(min, max);
      var after = new Range<string>(min, null);
      var before = new Range<string>(null, max);
      // Then
      var expected = new string[]
      {
        "1960",
        "2020",
        "1960",
        null,
        null,
        "2020"
      };
      var obtained = new string[]
      {
        between.Min,
        between.Max,
        after.Min,
        after.Max,
        before.Min,
        before.Max
      };
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void Construtor_ParaTipoStruct_DeveInstanciar()
    {
      // Given
      int min = 10;
      int max = 20;
      // When
      var between = new Range<int?>(min, max);
      var after = new Range<int?>(min, null);
      var before = new Range<int?>(null, max);
      // Then
      var expected = new int?[]
      {
        10,
        20,
        10,
        null,
        null,
        20
      };
      var obtained = new int?[]
      {
        between.Min,
        between.Max,
        after.Min,
        after.Max,
        before.Min,
        before.Max
      };
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ConversaoImplicita_DeTipoClassParaNaoTipado_DeveConverter()
    {
      // Given
      Range<string> after = new Range<string>("1960", null);
      // When
      Range range = after;
      // Then
      var expected = new object[] { "1960", null };
      var obtained = new object[] { range.Min, range.Max };
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ConversaoImplicita_DeTipoStructParaNaoTipado_DeveConverter()
    {
      // Given
      Range<int?> after = new Range<int?>(10, null);
      // When
      Range range = after;
      // Then
      var expected = new object[] { 10, null };
      var obtained = new object[] { range.Min, range.Max };
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ConversaoImplicita_DeNaoTipadoParaTipoClass_DeveConverter()
    {
      // Given
      Range after = new Range(1960, null);
      // When
      Range<string> range = after;
      // Then
      var expected = new string[] { "1960", null };
      var obtained = new string[] { range.Min, range.Max };
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ConversaoImplicita_DeNaoTipadoParaTipoStruct_DeveConverter()
    {
      // Given
      Range after = new Range("1960", null);
      // When
      Range<int?> range = after;
      // Then
      var expected = new int?[] { 1960, null };
      var obtained = new int?[] { range.Min, range.Max };
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ConversaoImplicita_DeStringInvalidaParaStruct_DeveEmitirFormatException()
    {
      // Given
      Range after = new Range("ONE", "TWO");
      // Then
      Assert.Throws<FormatException>(() =>
      {
        Range<int?> range = after;
      });
    }

    [Fact]
    public void ConversaoImplicita_EntreTiposIncompativeis_DeveEmitirInvalidCastException()
    {
      // Given
      Range after = new Range(DateTime.Today, DateTime.Now);
      // Then
      Assert.Throws<InvalidCastException>(() =>
      {
        Range<int?> range = after;
      });
    }
  }
}