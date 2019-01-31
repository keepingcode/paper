using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Api.Rendering
{
  public interface IArgs
  {
    ICollection<string> Keys { get; }

    Var this[string key] { get; }
  }
}
