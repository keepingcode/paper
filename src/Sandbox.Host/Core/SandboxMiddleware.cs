using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Net.Http.Headers;
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Paper.Media.Serialization;
using Sandbox.Lib;
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
      var requestUri = new UriString(httpContext.Request.GetDisplayUrl());
      var target = RenderTarget(requestUri);

      if (target?.Value is Entity entity)
      {
        var mediaType = httpContext.Request.ContentType?.Contains("xml") == true
          ? "application/xml"
          : "application/json";

        var serializer = new MediaSerializer();
        var content = serializer.Serialize(entity, mediaType);

        httpContext.Response.ContentType = $"{mediaType}; charset=UTF-8";

        await httpContext.Response.WriteAsync(content);
      }
      else if (target?.Value is Uri uri)
      {
        httpContext.Response.StatusCode = (int)HttpStatusCode.Found;
        httpContext.Response.Headers[HeaderNames.Location] = uri.ToString();
      }
      else
      {
        await next.Invoke(httpContext);
      }
    }

    private Target RenderTarget(UriString uri)
    {
      switch (uri.Path.ToLower())
      {
        case "":
          return uri.Clone().Combine("/index").ToUri();

        case "/index":
          return GetIndex(uri);

        case "/blueprint":
          return GetBlueprint(uri);

        default:
          return null;
      }
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
      entity.AddLink(uri.Clone().Combine("/blueprint"), "Blueprint", Rel.Blueprint);
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
      entity.AddLink(uri.Clone().Combine("/index"), "Início", Rel.Index);
      return entity;
    }
  }
}
