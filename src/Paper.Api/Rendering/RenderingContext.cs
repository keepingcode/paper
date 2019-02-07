using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  public class RenderingContext
  {
    public IPaperCatalog PaperCatalog { get; set; }

    public IFactory Factory { get; set; }

    public Request Request { get; set; }

    public Response Response { get; set; }
  }
}
