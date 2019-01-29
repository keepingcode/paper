using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Media.Rendering
{
  public interface IQueryArgs
  {
    ICollection<string> Keys { get; }

    Var this[string key] { get; set; }
  }
}
