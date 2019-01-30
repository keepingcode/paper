using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Rendering
{
  public class Request
  {
    private IHttpRequest request;

    public Request(IHttpRequest request)
    {
      this.request = request;
      this.ContentHeader = new ContentHeader(request.Headers);
    }

    public string RequestUri => request.RequestUri;

    public string PathBase => request.PathBase;

    public string Path => request.Path;

    public ContentHeader ContentHeader { get; }
  }
}
