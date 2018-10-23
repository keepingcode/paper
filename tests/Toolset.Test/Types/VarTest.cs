using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Toolset.Types
{
  public class VarTest
  {
    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarNull()
    {
      // Given
      Var nullVar = new Var();
      var nullableVar = new Var();
      // When
      nullVar.RawValue = null;
      nullableVar.RawValue = new int?();
      // Then
      Assert.Equal(expected: VarKind.Null, actual: nullVar.Kind);
      Assert.Equal(expected: VarKind.Null, actual: nullableVar.Kind);
      Assert.Null(nullVar.Value);
      Assert.Null(nullableVar.Value);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoPrimitivo()
    {
      // Given
      Var code = new Var();
      var date = new Var();
      var time = new Var();
      var full = new Var();
      // Given
      code.RawValue = 1234;
      date.RawValue = new Date(2020, 12, 31);
      time.RawValue = new Time(23, 59, 59, 999);
      full.RawValue = new DateTime(2020, 12, 31, 23, 59, 59, 999);
      // Then
      Assert.Equal(expected: VarKind.Primitive, actual: code.Kind);
      Assert.Equal(expected: VarKind.Primitive, actual: date.Kind);
      Assert.Equal(expected: VarKind.Primitive, actual: time.Kind);
      Assert.Equal(expected: VarKind.Primitive, actual: full.Kind);
      Assert.Equal(expected: 1234, actual: code.Value);
      Assert.Equal(expected: 2020, actual: date.Value is Date actualDate ? actualDate.Value.Year : 0);
      Assert.Equal(expected: 0023, actual: time.Value is Time actualTime ? actualTime.Value.Hour : 0);
      Assert.Equal(expected: 2020, actual: full.Value is DateTime actualDateTime ? actualDateTime.Year : 0);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoString()
    {
      // Given
      Var text = new Var();
      // Given
      text.RawValue = "XY";
      // Then
      Assert.Equal(expected: VarKind.Primitive, actual: text.Kind);
      Assert.Equal(expected: "XY", actual: text.Value);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoGraph()
    {
      // Given
      var graph = new { Id = 10 };
      Var var = new Var();
      // When
      var.RawValue = graph;
      // Then
      Assert.Equal(expected: VarKind.Graph, actual: var.Kind);
      Assert.Equal(expected: graph, actual: var.Value);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoRange()
    {
      // Given
      var range = new Range<int?>(10, 20);
      Var var = new Var();
      // When
      var.RawValue = range;
      // Then
      Assert.Equal(expected: VarKind.Range, actual: var.Kind);
      Assert.Equal(expected: range, actual: var.Value);
      Assert.Equal(expected: 10, actual: var.Range.Min);
      Assert.Equal(expected: 20, actual: var.Range.Max);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoIList()
    {
      // Given
      var list = new[] { 10, 20 };
      Var var = new Var();
      // When
      var.RawValue = list;
      // Then
      Assert.Equal(expected: VarKind.List, actual: var.Kind);
      Assert.Equal(expected: new[] { 10, 20 }, actual: var.List);
      Assert.Null(var.Value);
    }

    //[Fact]
    //public void Construtor_TipoRange_NaoTipado()
    //{
    //}
  }
}
