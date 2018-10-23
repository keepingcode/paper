using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;
using Toolset.Types;
using Xunit;

namespace Paper.Media.Utilities.Types
{
  public class UriArgsTest
  {
    #region ToUriComponent

    [Fact]
    public void ToUriComponent_ParaTipoSimples_SerializarNoPadrao()
    {
      // Given
      var uriArgs = new UriArgs();
      uriArgs["number"] = 10;
      uriArgs["string"] = "Ten";
      uriArgs["boolean"] = true;
      // When
      var uriComponent = uriArgs.ToUriComponent();
      // Then
      var expected = "?number=10&string=Ten&boolean=1";
      var obtained = uriComponent;
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ToUriComponent_ParaTipoData_SerializarNoPadrao()
    {
      // Given
      var dateTime = new DateTime(2020, 12, 31, 23, 59, 59, DateTimeKind.Utc);

      var uriArgs = new UriArgs();
      uriArgs["dateTime"] = dateTime;
      uriArgs["date"] = (Date)dateTime;
      uriArgs["time"] = (Time)dateTime;
      // When
      var uriComponent = uriArgs.ToUriComponent();
      // Then
      var expected = "?dateTime=2020-12-31T23:59:59&date=2020-12-31&time=23:59:59";
      var obtained = uriComponent;
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ToUriComponent_ParaTipoRange_SerializarNoPadrao()
    {
      // Given
      var uriArgs = new UriArgs();
      uriArgs["less"] = new Range(null, 20);
      uriArgs["more"] = new Range(10, null);
      uriArgs["in"] = new Range(10, 20);
      // When
      var uriComponent = uriArgs.ToUriComponent();
      // Then
      var expected = "?less.max=20&more.min=10&in.min=10&in.max=20";
      var obtained = uriComponent;
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void ToUriComponent_ParaTipoIList_SerializarNoPadrao()
    {
      // Given
      var uriArgs = new UriArgs();
      uriArgs["items"] = new List<string> { "one", "two" };
      // When
      var uriComponent = uriArgs.ToUriComponent();
      // Then
      var expected = "?items[]=one&items[]=two";
      var obtained = uriComponent;
      Assert.Equal(expected, obtained);
    }

    #endregion

    #region ToUriComponent

    [Fact]
    public void MakeString_ParaTiposSuportados_SerializarNoPadrao()
    {
      // Given
      var dateTime = new DateTime(2020, 12, 31, 23, 59, 59, DateTimeKind.Utc);
      var values = new object[]
      {
        null,
        true,
        false,
        10,
        "Ten",
        dateTime,
        (Date)dateTime,
        (Time)dateTime
      };
      // When
      var strings = values.Select(UriArgs.MakeString).ToArray();
      // Then
      var expected = string.Join(", ",
        "",
        "1",
        "0",
        "10",
        "Ten",
        "2020-12-31T23:59:59",
        "2020-12-31",
        "23:59:59"
      );
      var obtained = string.Join(", ", strings);
      Assert.Equal(expected, obtained);
    }

    #endregion
  }
}