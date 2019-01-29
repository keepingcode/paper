using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Rendering
{
  public interface IHeaders
  {
    ICollection<string> Keys { get; }

    string this[string key] { get; set; }
  }
}
