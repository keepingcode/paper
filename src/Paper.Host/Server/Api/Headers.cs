using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Paper.Media.Rendering;
using Toolset;

namespace Paper.Host.Server.Api
{
  public class Headers : IHeaders
  {
    private IHeaderDictionary headers;

    public Headers(IHeaderDictionary headers)
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
