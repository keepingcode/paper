using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  public interface IHeaders
  {
    ICollection<string> Keys { get; }

    string this[string key] { get; set; }
  }
}
