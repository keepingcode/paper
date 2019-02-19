using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Toolset
{
  public enum HttpStatusClass
  {
    Informational,
    Success,
    Redirection,
    ClientError,
    ServerError
  }
}