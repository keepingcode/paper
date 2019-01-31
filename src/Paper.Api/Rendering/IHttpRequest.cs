using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public interface IHttpRequest
  {
    string RequestUri { get; }

    string PathBase { get; }

    string Path { get; }

    Headers Headers { get; }

    Stream Body { get; }
  }
}
