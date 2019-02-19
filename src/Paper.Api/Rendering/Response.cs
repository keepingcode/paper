using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Paper.Media;
using Toolset.Collections;
using Toolset;
using Toolset.Net;

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
        var link = new Link();
        link.AddRel(RelNames.Self);
        link.SetHref(req.RequestUri);
        entity.WithLinks().AddAt(0, link);
      }

      entity.ExpandUri(req.RequestUri, req.PathBase);

      var accept = new AcceptHeader(req.Headers, req.QueryArgs);
      var mimeType = accept.BestMimeType;
      var encoding = accept.BestEncoding;

      var contentType = mimeType;

      // FIXME:
      // Por enquanto a resposta desce em forma simples:
      // -  application/json ou application/xml em vez de application/vnd.siren+json ou application/vnd.siren+xml
      // No futuro seria bom retornar o tipo especifico, se os clientes em geral suportarem.
      if (contentType == MediaSerializer.JsonSiren) contentType = MediaSerializer.Json;
      if (contentType == MediaSerializer.XmlSiren) contentType = MediaSerializer.Xml;

      var serializer = new MediaSerializer(mimeType);
      using (var memory = new MemoryStream())
      {
        serializer.Serialize(entity, memory, encoding);
        memory.Position = 0;

        Status = HttpStatusCode.OK;

        Headers[HeaderNames.ContentType] = $"{contentType}; charset={encoding.WebName}";
        SetContentDisposition(mimeType, entity);

        await memory.CopyToAsync(res.Body);
      }
    }

    private void SetContentDisposition(string mimeType, Entity entity)
    {
      if (Headers[HeaderNames.ContentDisposition] != null)
        return;

      if (mimeType != MediaSerializer.Excel && mimeType != MediaSerializer.Csv)
        return;

      var name = entity.Title
            ?? req.Path.Split('/').NonNullOrEmpty().LastOrDefault()
            ?? entity.Class?.FirstOrDefault(x => char.IsUpper(x.First()))
            ?? "Download";

      name = name.ChangeCase(TextCase.Underscore);

      if (mimeType == MediaSerializer.Excel)
      {
        if (!name.EndsWith(".xlsx"))
        {
          name += ".xlsx";
        }
        Headers[HeaderNames.ContentDisposition] = $"attachment; filename={name}";
      }
      else if (mimeType == MediaSerializer.Csv)
      {
        if (!name.EndsWith(".csv"))
        {
          name += ".csv";
        }
        Headers[HeaderNames.ContentDisposition] = $"attachment; filename={name}";
      }
    }
  }
}
