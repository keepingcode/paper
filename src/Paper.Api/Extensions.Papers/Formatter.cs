using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Rendering;
using Paper.Media;

namespace Paper.Api.Extensions.Papers
{
  public class Formatter : IFormatter
  {
    public event Format OnFormat;

    public void Format(IPaperContext context, IObjectFactory factory, Entity entity)
    {
      OnFormat?.Invoke(context, factory, entity);
    }

    public static IFormatter Format(Format format)
    {
      var formatter = new Formatter();
      formatter.OnFormat += format;
      return formatter;
    }
  }
}
