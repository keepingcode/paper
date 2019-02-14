using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Toolset;

namespace Paper.Api.Rendering
{
  public class Headers : IHeaders
  {
    private IHeaders headers;

    public Headers(IHeaders headers)
    {
      this.headers = headers;
    }

    public ICollection<string> Keys => this.headers.Keys;

    public string this[string key]
    {
      get => headers[key];
      set => headers[key] = value;
    }
  }
}
