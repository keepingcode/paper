using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Rendering;
using Toolset.Collections;

namespace Paper.Api.Extensions.Papers
{
  internal class PaperContext : IPaperContext
  {
    public PaperDescriptor Paper { get; set; }

    public string Path { get; set; }

    public HashMap Args { get; set; }

    public Request Request { get; set; }

    public Response Response { get; set; }
  }
}