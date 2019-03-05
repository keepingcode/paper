using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media.Data;

namespace Paper.Api.Extensions.Papers
{
  public class RenderContext
  {
    public Sort Sort { get; set; }

    public Page Page { get; set; }

    public bool HasMorePages { get; set; }

    public IFilter Filter { get; set; }
  }
}
