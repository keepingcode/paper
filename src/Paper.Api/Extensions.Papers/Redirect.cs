using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Paper.Media;
using Toolset;
using Toolset.Collections;
using Toolset.Net;

namespace Paper.Api.Extensions.Papers
{
  public static class Redirect
  {
    public static Ret To(Href location)
    {
      return new Ret
      {
        Status = new Ret.RetStatus
        {
          Code = HttpStatusCode.RedirectKeepVerb,
          Data = new HashMap<string> { { HeaderNames.Location, location } }
        }
      };
    }

    public static Ret To<TPaper>()
    {
      throw new NotImplementedException();
    }

    public static Ret To(Type paperType)
    {
      throw new NotImplementedException();
    }
  }
}
