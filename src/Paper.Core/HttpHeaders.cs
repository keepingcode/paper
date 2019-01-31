using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Paper.Api.Rendering;
using Toolset;

namespace Paper.Core
{
  public class HttpHeaders : IHeaders
  {
    private IHeaderDictionary headers;

    public HttpHeaders(IHeaderDictionary headers)
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
