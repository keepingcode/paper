using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Paper.Media;
using Paper.Api.Rendering;
using Toolset;
using Toolset.Collections;

namespace Paper.Core
{
  public class HttpRequest : IHttpRequest
  {
    public HttpRequest(HttpContext context)
    {
      this.RequestUri = context.Request.GetDisplayUrl();
      this.PathBase = context.Request.PathBase;
      this.Path = context.Request.Path;
      this.Headers = new Headers(new HttpHeaders(context.Request.Headers));
      this.Body = context.Request.Body;
    }

    public string RequestUri { get; }

    public string PathBase { get; }

    public string Path { get; }

    public Headers Headers { get; }

    public Stream Body { get; }
  }
}
