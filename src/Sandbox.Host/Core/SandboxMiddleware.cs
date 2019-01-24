using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Net.Http.Headers;
using Paper.Core;
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Paper.Media.Serialization;
using Sandbox.Lib;
using Toolset;
using Toolset.Xml;

namespace Sandbox.Host.Core
{
  public class SandboxMiddleware
  {
    private readonly RequestDelegate next;

    public SandboxMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider)
    {
      Entity entity = null;
      int status;

      try
      {
        var target = RenderTarget(httpContext);
        if (target?.Value is Uri uri)
        {
          // Redirecionando para outra URI
          httpContext.Response.StatusCode = (int)HttpStatusCode.Found;
          httpContext.Response.Headers[HeaderNames.Location] = uri.ToString();
          return;
        }

        entity = target?.Value as Entity;
        if (entity == null)
        {
          var requestUri = httpContext.Request.GetDisplayUrl();
          entity = HttpEntity.Create(requestUri, HttpStatusCode.NotFound);
        }

        status = (int)HttpStatusCode.OK;
      }
      catch (Exception ex)
      {
        ex.Trace();
        entity = HttpEntity.Create(httpContext.Request.GetDisplayUrl(), ex);
        status = (int)HttpStatusCode.InternalServerError;
      }

      try
      {
        var mediaType = httpContext.Request.ContentType?.Contains("xml") == true
          ? "application/xml"
          : "application/json";

        var serializer = new MediaSerializer();
        var content = serializer.Serialize(entity, mediaType);

        httpContext.Response.StatusCode = (int)status;
        httpContext.Response.ContentType = $"{mediaType}; charset=UTF-8";

        await httpContext.Response.WriteAsync(content);
      }
      catch (Exception ex)
      {
        ex.Trace();

        status = (int)HttpStatusCode.InternalServerError;

        var statusDecription = 
          HttpStatusCode.InternalServerError
            .ToString()
            .ChangeCase(TextCase.ProperCase);

        httpContext.Response.StatusCode = status;
        httpContext.Response.ContentType = $"text/plain; charset=UTF-8";

        var nl = Environment.NewLine;
        var cause = string.Join(nl, ex.GetCauseMessages().Select(x => $"  • {x}"));
        var message = $"Fault {nl}  {status} - {statusDecription} {nl}Cause {nl}{cause}";

        await httpContext.Response.WriteAsync(message);
      }
    }

    private Target RenderTarget(HttpContext httpContext)
    {
      var req = httpContext.Request;
      var method = req.Method;
      var prefix = req.PathBase.Value;
      var path = req.Path.Value;
      var requestUri = new UriString(req.GetDisplayUrl());

      if (method.EqualsAnyIgnoreCase(MethodNames.Post, MethodNames.Put, MethodNames.Patch))
      {
        var serializer = new MediaSerializer(req.ContentType);
        var entity = serializer.Deserialize(req.Body);
        Debug.WriteLine(Json.Beautify(serializer.SerializeToJson(entity)));
      }

      switch (path.ToLower())
      {
        case "":
          return requestUri.Combine(prefix).Combine("Index").ToUri();

        case "/status":
          return SandboxEntities.GetStatus(requestUri, prefix);

        case "/index":
          return SandboxEntities.GetIndex(requestUri, prefix);

        case "/blueprint":
          return SandboxEntities.GetBlueprint(requestUri, prefix);

        default:
          return FindEmbeddedResource(path);
      }
    }

    private Target FindEmbeddedResource(string path)
    {
      var name = path.Replace("/", "-") + ".json";
      var text = GetType().GetResourceText(name);
      if (text == null)
        return null;

      var media = new MediaSerializer("json");
      var entity = media.Deserialize(text);
      return entity;
    }
  }
}
