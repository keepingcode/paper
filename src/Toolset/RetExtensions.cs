using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Toolset
{
  public static class RetExtensions
  {
    public static bool IsNotFound(this Ret.RetStatus status)
      => status.Code == HttpStatusCode.NotFound;

    public static bool IsNotFoundOrImplemented(this Ret.RetStatus status)
      => status.Code == HttpStatusCode.NotFound || status.Code == HttpStatusCode.NotImplemented;

    public static bool IsRedirect(this Ret.RetStatus status)
      => ((int)status.Code / 100) == 3;
  }
}