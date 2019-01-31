﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Paper.Media;

namespace Paper.Api.Rendering
{
  public class Response : IHttpResponse
  {
    private Request req;
    private IHttpResponse res;

    public Response(Request request, IHttpResponse response)
    {
      this.req = request;
      this.res = response;
    }

    public Headers Headers => res.Headers;

    public Stream Body => res.Body;

    public HttpStatusCode Status
    {
      get => res.Status;
      set => res.Status = value;
    }

    public async Task WriteEntityAsync(Entity entity)
    {
      var hasSelfLink = entity.Links?.Any(x => x.Rel?.Contains(RelNames.Self) == true) == true;
      if (!hasSelfLink)
      {
        entity.AddLinkSelf(req.RequestUri);
      }

      var accept = new AcceptHeader(req.Headers, req.QueryArgs);
      var mimeType = accept.BestMimeType;
      var encoding = accept.BestEncoding;

      Headers[HeaderNames.ContentType] = $"{mimeType}; charset={encoding.WebName}";

      var serializer = new MediaSerializer();
      using (var memory = new MemoryStream())
      {
        serializer.Serialize(entity, mimeType, memory, encoding);
        memory.Position = 0;
        await memory.CopyToAsync(res.Body);
      }
    }
  }
}