using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Rendering
{
  public class Request
  {
    public Request(IHttpRequest request)
    {
      this.ContentHeader = new ContentHeader(request.Headers);
    }

    public ContentHeader ContentHeader { get; }
  }
}
