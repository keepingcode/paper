using System;
using System.Collections.Generic;
using System.Text;
using Toolset;
using Toolset.Collections;
using Toolset.Net;

namespace Paper.Api.Rendering
{
  public class ContentHeader
  {
    private Headers headers;

    public ContentHeader(Headers headers)
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

    public Encoding Encoding
    {
      get
      {
        var value = Change.ToOrDefault<string>(headers[HeaderNames.ContentEncoding]);
        try
        {
          return (value != null) ? Encoding.GetEncoding(value) : Encoding.UTF8;
        }
        catch
        {
          return Encoding.UTF8;
        }
      }
      set
      {
        headers[HeaderNames.ContentEncoding] = value?.BodyName;
      }
    }

    public string Disposition
    {
      get => Change.ToOrDefault<string>(headers[HeaderNames.ContentDisposition]);
      set => headers[HeaderNames.ContentDisposition] = value;
    }
  }
}