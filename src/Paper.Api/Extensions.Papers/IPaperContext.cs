using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Rendering;
using Paper.Media.Data;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Extensions.Papers
{
  public interface IPaperContext
  {
    PaperDescriptor Paper { get; }

    string Path { get; }

    Args Args { get; }

    Request Request { get; }

    Response Response { get; }

    RenderContext RenderContext { get; }
  }
}