using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Api.Rendering
{
  public interface IArgs : IEnumerable<KeyValuePair<string, Var>>
  {
    ICollection<string> Keys { get; }

    ICollection<Var> Values { get; }

    Var this[string key] { get; }
  }
}
