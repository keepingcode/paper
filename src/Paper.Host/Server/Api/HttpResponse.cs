using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Paper.Media;
using Paper.Media.Rendering;
using Toolset.Collections;

namespace Paper.Host.Server.Api
{
  public class HttpResponse : IHttpResponse
  {
    public HttpResponse(HttpContext context)
    {
      this.Headers = new Headers(context.Response.Headers);
      this.Body = context.Response.Body;
    }

    public IHeaders Headers { get; }

    public Stream Body { get; }
  }
}