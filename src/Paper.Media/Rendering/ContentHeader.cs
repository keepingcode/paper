using System;
using System.Collections.Generic;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media.Rendering
{
  public class ContentHeader
  {
    private IHeaders headers;

    public ContentHeader(IHeaders headers)
    {
      this.headers = headers;
    }

    public string Type
    {
      get => Change.ToOrDefault<string>(headers[HeaderNames.ContentType]);
      set => headers[HeaderNames.ContentType] = value;
    }

    public int Length
    {
      get => Change.ToOrDefault<int>(headers[HeaderNames.ContentLength]);
      set => headers[HeaderNames.ContentLength] = value.ToString();
    }

    public string Encoding
    {
      get => Change.ToOrDefault<string>(headers[HeaderNames.ContentEncoding]);
      set => headers[HeaderNames.ContentEncoding] = value;
    }

    public string Disposition
    {
      get => Change.ToOrDefault<string>(headers[HeaderNames.ContentDisposition]);
      set => headers[HeaderNames.ContentDisposition] = value;
    }
  }
}