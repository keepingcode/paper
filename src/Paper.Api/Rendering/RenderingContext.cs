using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  public class RenderingContext
  {
    public IBookshelf Bookshelf { get; set; }

    public IFactory Factory { get; set; }

    public Request Request { get; set; }

    public Response Response { get; set; }
  }
}
