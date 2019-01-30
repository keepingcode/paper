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
using Paper.Media.Rendering;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Xml;
using Microsoft.Extensions.DependencyInjection;

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
      var req = ctx.Request;
      var res = ctx.Response;
      try
      {
        var bookshelf = serviceProvider.GetService<Bookshelf>();
        var papers = bookshelf?.FindPaper(req.Path);
        if (papers == null)
        {
          var status = HttpStatusCode.ServiceUnavailable;
          var entity = HttpEntity.Create(req.GetDisplayUrl(), status,
            "Paper não configurado corretamente. " +
            "A instância de Bookshelf não foi configurada na instância de IServiceProvider."
          );
          await WriteEntityAsync(ctx, entity, status);
          return;
        }

        if (!papers.Any())
        {
          var status = HttpStatusCode.NotFound;
          var entity = HttpEntity.Create(req.GetDisplayUrl(), status,
            $"Não existe uma rota para: {req.PathBase}{req.Path}"
          );
          await WriteEntityAsync(ctx, entity, status);
          return;
        }

        var context = new RenderingContext
        {
          Bookshelf = bookshelf,
          Factory = new Factory(serviceProvider),
          Request = new Request(new HttpRequest(ctx)),
          Response = new Response(new HttpRequest(ctx), new HttpResponse(ctx))
        };

        var enumerator = papers.GetEnumerator();
        NextAsync next = null;
        next = new NextAsync(async () =>
        {
          if (enumerator.MoveNext())
          {
            var paper = enumerator.Current;
            await paper.RenderAsync(context, next);
          }
        });
        await next.Invoke();
      }
      catch (Exception ex)
      {
        try
        {
          var status = HttpStatusCode.InternalServerError;
          var entity = HttpEntity.Create(req.GetDisplayUrl(), status, ex);
          await WriteEntityAsync(ctx, entity, status);
        }
        catch (Exception exx)
        {
          res.StatusCode = (int)HttpStatusCode.InternalServerError;
          res.ContentType = "text/plain; charset=UTF-8";

          var ln = Environment.NewLine;
          await res.WriteAsync($"Fault:{ln}{exx.GetStackTrace()}{ln}Caused by:{ln}{ex.GetStackTrace()}");
        }
      }
    }

    private async Task WriteEntityAsync(HttpContext ctx, Entity entity, HttpStatusCode status)
    {
      var req = ctx.Request;
      var res = ctx.Response;

      var accept = new AcceptHeader(new Headers(req.Headers), new QueryArgs(req.QueryString.Value));
      var mimeType = accept.BestMimeType;
      var encoding = accept.BestEncoding;

      res.StatusCode = (int)status;
      res.ContentType = $"{mimeType}; charset={encoding.WebName}";
      
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