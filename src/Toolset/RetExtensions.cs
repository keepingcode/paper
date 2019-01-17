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
    public static bool IsNotFound(this Ret ret)
      => ret.Status == (int)HttpStatusCode.NotFound;

    public static bool IsNotFound<T>(this Ret<T> ret)
      => ret.Status == (int)HttpStatusCode.NotFound;

    public static bool IsNotFoundOrImplemented(this Ret ret)
      => ret.Status == (int)HttpStatusCode.NotFound || ret.Status == (int)HttpStatusCode.NotImplemented;

    public static bool IsNotFoundOrImplemented<T>(this Ret<T> ret)
      => ret.Status == (int)HttpStatusCode.NotFound || ret.Status == (int)HttpStatusCode.NotImplemented;
  }
}