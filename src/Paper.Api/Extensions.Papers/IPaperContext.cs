using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Rendering;
using Toolset.Collections;

namespace Paper.Api.Extensions.Papers
{
  public interface IPaperContext
  {
    PaperDescriptor Paper { get; }

    string Path { get; }

    HashMap Args { get; }

    Request Request { get; }

    Response Response { get; }
  }
}