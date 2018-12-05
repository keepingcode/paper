using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Xunit;

namespace Toolset.Types
{
  public class VarTest
  {
    class Graph
    {
      public int Id { get; set; }
    }

    #region Testes para Var não tipado.

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarNull()
    {
      // Given
      var nullVar = new Var();
      var nullableVar = new Var();
      // When
      nullVar.RawValue = null;
      nullableVar.RawValue = new int?();
      // Then
      Assert.Equal(expected: VarKinds.Null, actual: nullVar.Kind);
      Assert.Equal(expected: VarKinds.Null, actual: nullableVar.Kind);
      Assert.Null(nullVar.Value);
      Assert.Null(nullableVar.Value);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoPrimitivo()
    {
      // Given
      var code = new Var();
      var date = new Var();
      var time = new Var();
      var full = new Var();
      // Given
      code.RawValue = 1234;
      date.RawValue = new Date(2020, 12, 31);
      time.RawValue = new Time(23, 59, 59, 999);
      full.RawValue = new DateTime(2020, 12, 31, 23, 59, 59, 999);
      // Then
      Assert.Equal(expected: VarKinds.Primitive, actual: code.Kind);
      Assert.Equal(expected: VarKinds.Primitive, actual: date.Kind);
      Assert.Equal(expected: VarKinds.Primitive, actual: time.Kind);
      Assert.Equal(expected: VarKinds.Primitive, actual: full.Kind);
      Assert.Equal(expected: 1234, actual: code.Value);
      Assert.Equal(expected: 2020, actual: date.Value is Date actualDate ? actualDate.Value.Year : 0);
      Assert.Equal(expected: 0023, actual: time.Value is Time actualTime ? actualTime.Value.Hour : 0);
      Assert.Equal(expected: 2020, actual: full.Value is DateTime actualDateTime ? actualDateTime.Year : 0);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoString()
    {
      // Given
      var text = new Var();
      // Given
      text.RawValue = "XY";
      // Then
      Assert.Equal(expected: VarKinds.String, actual: text.Kind);
      Assert.Equal(expected: "XY", actual: text.Value);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoGraph()
    {
      // Given
      var graph = new Graph { Id = 10 };
      var var = new Var();
      // When
      var.RawValue = graph;
      // Then
      Assert.Equal(expected: VarKinds.Graph, actual: var.Kind);
      Assert.Equal(expected: graph, actual: var.Value);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoRange()
    {
      // Given
      var range = new Range<int?>(10, 20);
      var var = new Var();
      // When
      var.RawValue = range;
      // Then
      Assert.Equal(expected: VarKinds.Range, actual: var.Kind);
      Assert.Equal(expected: 10, actual: var.Range.Min);
      Assert.Equal(expected: 20, actual: var.Range.Max);
      Assert.Null(var.Value);
    }

    [Fact]
    public void RawValue_QuandoNaoTipado_AceitarTipoIList()
    {
      // Given
      var list = new int[] { 10, 20 };
      var var = new Var();
      // When
      var.RawValue = list;
      // Then
      Assert.Equal(expected: VarKinds.List, actual: var.Kind);
      Assert.Equal(expected: new[] { 10, 20 }, actual: var.List);
      Assert.Null(var.Value);
    }

    #endregion

    #region Testes para Var<T> tipado

    [Fact]
    public void RawValue_QuandoTipado_AceitarNull()
    {
      // Given
      var nullVar = new Var<string>();
      var nullableVar = new Var<int?>();
      // When
      nullVar.RawValue = null;
      nullableVar.RawValue = new int?();
      // Then
      Assert.Equal(expected: VarKinds.Null, actual: nullVar.Kind);
      Assert.Equal(expected: VarKinds.Null, actual: nullableVar.Kind);
      Assert.Null(nullVar.Value);
      Assert.Null(nullableVar.Value);
    }

    [Fact]
    public void RawValue_QuandoTipado_AceitarTipoPrimitivo()
    {
      // Given
      var code = new Var<int>();
      var date = new Var<Date>();
      var time = new Var<Time>();
      var full = new Var<DateTime>();
      // Given
      code.RawValue = 1234;
      date.RawValue = new Date(2020, 12, 31);
      time.RawValue = new Time(23, 59, 59, 999);
      full.RawValue = new DateTime(2020, 12, 31, 23, 59, 59, 999);
      // Then
      Assert.Equal(expected: VarKinds.Primitive, actual: code.Kind);
      Assert.Equal(expected: VarKinds.Primitive, actual: date.Kind);
      Assert.Equal(expected: VarKinds.Primitive, actual: time.Kind);
      Assert.Equal(expected: VarKinds.Primitive, actual: full.Kind);
      Assert.Equal(expected: 1234, actual: code.Value);
      Assert.Equal(expected: 2020, actual: date.Value is Date actualDate ? actualDate.Value.Year : 0);
      Assert.Equal(expected: 0023, actual: time.Value is Time actualTime ? actualTime.Value.Hour : 0);
      Assert.Equal(expected: 2020, actual: full.Value is DateTime actualDateTime ? actualDateTime.Year : 0);
    }

    [Fact]
    public void RawValue_QuandoTipado_AceitarTipoString()
    {
      // Given
      var text = new Var<string>();
      // Given
      text.RawValue = "XY";
      // Then
      Assert.Equal(expected: VarKinds.String, actual: text.Kind);
      Assert.Equal(expected: "XY", actual: text.Value);
    }

    [Fact]
    public void RawValue_QuandoTipado_AceitarTipoGraph()
    {
      // Given
      var graph = new Graph { Id = 10 };
      var var = new Var<Graph>();
      // When
      var.RawValue = graph;
      // Then
      Assert.Equal(expected: VarKinds.Graph, actual: var.Kind);
      Assert.Equal(expected: graph, actual: var.Value);
    }

    [Fact]
    public void RawValue_QuandoTipado_AceitarTipoRange()
    {
      // Given
      var range = new Range(10, "20");
      var var = new Var<int?>();
      // When
      var.RawValue = range;
      // Then
      Assert.Equal(expected: VarKinds.Range, actual: var.Kind);
      Assert.Equal(expected: 10, actual: var.Range.Min);
      Assert.Equal(expected: 20, actual: var.Range.Max);
      Assert.Null(var.Value);
    }

    [Fact]
    public void RawValue_QuandoTipado_AceitarTipoIList()
    {
      // Given
      var list = new object[] { null, 10, "20" };
      var var = new Var<int?>();
      // When
      var.RawValue = list;
      // Then
      Assert.Equal(expected: VarKinds.List, actual: var.Kind);
      Assert.Equal(expected: new int?[] { null, 10, 20 }, actual: var.List);
      Assert.Null(var.Value);
    }

    #endregion
  }
}
