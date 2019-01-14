using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
      }
      catch (Exception ex)
      {
        ex.Trace();
        entity = HttpEntity.Create(httpContext.Request.GetDisplayUrl(), ex);
      }

      try
      {
        var mediaType = httpContext.Request.ContentType?.Contains("xml") == true
          ? "application/xml"
          : "application/json";

        var serializer = new MediaSerializer();
        var content = serializer.Serialize(entity, mediaType);

        httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        httpContext.Response.ContentType = $"{mediaType}; charset=UTF-8";

        await httpContext.Response.WriteAsync(content);
      }
      catch (Exception ex)
      {
        ex.Trace();

        var status = (int)HttpStatusCode.InternalServerError;
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
      var path = httpContext.Request.Path.Value;
      var uri = new UriString(httpContext.Request.GetDisplayUrl(), httpContext.Request.PathBase);

      switch (path.ToLower())
      {
        case "":
          return uri.Combine("/Index").ToUri();

        case "/status":
          return GetStatus(uri);

        case "/index":
          return GetIndex(uri);

        case "/blueprint":
          return GetBlueprint(uri);

        default:
          return null;
      }
    }

    private Entity GetStatus(UriString uri)
    {
      return HttpEntity.Create(uri, HttpStatusCode.OK);
    }

    private Entity GetIndex(UriString uri)
    {
      var entity = new Entity();
      entity.AddTitle("Início");
      entity.AddClass(Class.Single);
      entity.AddProperties(new
      {
        Text = "Olá, mundo!"
      });
      entity.AddLinkSelf(uri);
      entity.AddLink(uri.Clone().Combine("/Blueprint"), "Blueprint", Rel.Blueprint);
      return entity;
    }

    private Entity GetBlueprint(UriString uri)
    {
      var entity = new Entity();
      entity.AddTitle("Blueprint");
      entity.AddClass(Class.Blueprint);
      entity.AddProperties(new Blueprint
      {
        HasNavBox = true,
        Theme = "blue",
        Info = new Blueprint.Details
        {
          Name = "Tickets",
          Title = "Tickets",
          Description = "Sistema de atendimento a clientes.",
          Manufacturer = "KeepCoding",
          Copyright = "Copyleft (ɔ) All rights reversed",
          Guid = Guid.Parse("D2C9BA1E-0F97-4C93-9FA9-AB1D5EDA2000"),
          Version = Version.Parse("1.0.0")
        }
      });
      entity.AddLinkSelf(uri);
      entity.AddLink(uri.Clone().Combine("/Index"), "Início", Rel.Index);
      return entity;
    }
  }
}
