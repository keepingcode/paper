using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Toolset
{
  public class ChangeTest
  {
    [Fact]
    public void To_DeComum_ParaTipoNullable()
    {
      Assert.Null(Change.To<int?>(null));
      Assert.Equal(100, Change.To<int?>("100"));
      Assert.Equal(100, Change.To<int?>(100));
      Assert.Equal(100, Change.To<int?>(100.001d));
      Assert.Equal(100, Change.To<int?>(100.001m));
      Assert.Equal(100, Change.To<int?>((int?)100));
      Assert.Equal(100, Change.To<int?>((double?)100.001d));
      Assert.Equal(100, Change.To<int?>((decimal?)100.001m));
    }

    [Fact]
    public void To_DeTipoNullable_ParaTipoComum()
    {
      Assert.Equal(0, Change.To<int>(null));
      Assert.Equal(100, Change.To<int>("100"));
      Assert.Equal(100, Change.To<int>((int?)100));
      Assert.Equal(100, Change.To<int>((double?)100.001d));
      Assert.Equal(100, Change.To<int>((decimal?)100.001m));
    }
  }
}