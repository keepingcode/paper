using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Paper.Api.Rendering;
using Paper.Media;
using Toolset.Collections;

namespace Paper.Core
{
  public class HttpResponse : IHttpResponse
  {
    private readonly Microsoft.AspNetCore.Http.HttpResponse res;

    public HttpResponse(HttpContext context)
    {
      this.res = context.Response;
      this.Headers = new Headers(new HttpHeaders(context.Response.Headers));
    }

    public Headers Headers { get; }

    public Stream Body
    {
      get => res.Body;
    }

    public HttpStatusCode Status
    {
      get => (HttpStatusCode)res.StatusCode;
      set => res.StatusCode = (int)value;
    }
  }
}