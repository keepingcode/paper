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
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Xml;
using Microsoft.Extensions.DependencyInjection;
using Paper.Api.Rendering;

namespace Paper.Core
{
  public class Middleware
  {
    private readonly RequestDelegate next;

    public Middleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext ctx, IServiceProvider serviceProvider)
    {
      try
      {
        var req = new Request(new HttpRequest(ctx));
        var res = new Response(req, new HttpResponse(ctx));

        var factory = serviceProvider.GetService<IObjectFactory>();
        if (factory == null)
          throw new NullReferenceException("A instância de IObjectFactory não foi definida no IServiceProvider.");

        var renderer = factory.CreateObject<PipelineRenderer>();

        await renderer.RenderAsync(req, res);
      }
      catch (Exception ex)
      {
        ex.Trace();

        var req = ctx.Request;
        var res = ctx.Response;
        var status = HttpStatusCode.InternalServerError;

        res.StatusCode = (int)status;
        res.ContentType = "text/plain; charset=UTF-8";

        var ln = Environment.NewLine;
        await res.WriteAsync(
          $"{(int)status} - {status.ToString().ChangeCase(TextCase.ProperCase)}{ln}{ex.Message}{ln}Caused by:{ln}{ex.GetStackTrace()}"
        );
      }
    }
  }
}