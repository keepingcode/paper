using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Toolset;

namespace Paper.Media
{
  public class MediaException : HttpException
  {
    public MediaException()
    {
    }

    public MediaException(string message)
      : base(message)
    {
    }

    public MediaException(Exception cause)
      : base(cause)
    {
    }

    public MediaException(HttpStatusCode status)
      : base(status)
    {
    }

    public MediaException(HttpStatusCode status, string message)
      : base(status, message)
    {
    }

    public MediaException(HttpStatusCode status, Exception cause)
      : base(status, cause)
    {
    }

    public MediaException(HttpStatusCode status, string message, Exception cause)
      : base(status, message, cause)
    {
    }
  }
}
