using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Toolset
{
  public class HttpException : Exception
  {
    public HttpException()
      : this(HttpStatusCode.InternalServerError, null, null)
    {
    }

    public HttpException(string message)
      : this(HttpStatusCode.InternalServerError, message, null)
    {
    }

    public HttpException(Exception cause)
      : this(HttpStatusCode.InternalServerError, null, cause)
    {
    }

    public HttpException(HttpStatusCode status)
      : this(status, null, null)
    {
    }

    public HttpException(HttpStatusCode status, string message)
      : this(status, message, null)
    {
    }

    public HttpException(HttpStatusCode status, Exception cause)
      : this(status, null, cause)
    {
    }

    public HttpException(HttpStatusCode status, string message, Exception cause)
      : base(message ?? cause?.Message ?? status.ToString().ChangeCase(TextCase.ProperCase), cause)
    {
      this.Data["StatusCode"] = (int)(this.Status = status);
    }

    public HttpStatusCode Status { get; }
  }
}