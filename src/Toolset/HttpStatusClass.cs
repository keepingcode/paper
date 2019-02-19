using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Toolset
{
  public enum HttpStatusClass
  {
    Informational = 1,
    Success = 2,
    Redirection = 3,
    ClientError = 4,
    ServerError = 5
  }
}