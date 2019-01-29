using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Rendering
{
  public class RenderingContext
  {
    public Bookshelf Bookshelf { get; set; }

    public IFactory Factory { get; set; }

    public Request Request { get; set; }

    public Response Response { get; set; }
  }
}
