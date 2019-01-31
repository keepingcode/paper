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
      var req = ctx.Request;
      var res = ctx.Response;
      try
      {
        var bookshelf = serviceProvider.GetService<Bookshelf>();
        var papers = bookshelf?.FindPaper(req.Path);
        if (papers == null)
        {
          var status = HttpStatusCode.ServiceUnavailable;
          var message =
            "Paper não configurado corretamente. " +
            "A instância de Bookshelf não foi configurada na instância de IServiceProvider.";
          await WriteFaultAsync(ctx, status, message, null);
          return;
        }

        if (!papers.Any())
        {
          var status = HttpStatusCode.NotFound;
          var message = $"Não existe uma rota para: {req.PathBase}{req.Path}";
          await WriteFaultAsync(ctx, status, message, null);
          return;
        }

        var context = new RenderingContext();
        context.Bookshelf = bookshelf;
        context.Factory = new Factory(serviceProvider);
        context.Request = new Request(new HttpRequest(ctx));
        context.Response = new Response(context.Request, new HttpResponse(ctx));
        
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
        var status = ex is HttpException http ? http.Status : HttpStatusCode.InternalServerError;
        await WriteFaultAsync(ctx, status, ex.Message, ex);
      }
    }

    private async Task WriteFaultAsync(HttpContext ctx, HttpStatusCode status, string message, Exception ex)
    {
      var req = ctx.Request;
      var res = ctx.Response;

      try
      {
        var headers = new Headers(new HttpHeaders(req.Headers));
        var queryString = new QueryArgs(req.QueryString.Value);

        var validMimeTypes = new[] {
          MediaSerializer.JsonSiren,
          MediaSerializer.Json,
          MediaSerializer.XmlSiren,
          MediaSerializer.Xml
        };

        var accept = new AcceptHeader(headers, queryString);
        var mimeType = accept.SelectBestMatch(accept.MimeTypes, validMimeTypes) ?? MediaSerializer.JsonSiren;
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
          var entity = HttpEntity.Create(req.GetDisplayUrl(), status, message);
          serializer.Serialize(entity, memory, encoding);

          memory.Position = 0;

          res.StatusCode = (int)status;
          res.ContentType = $"{contentType}; charset={encoding.WebName}";

          await memory.CopyToAsync(res.Body);
        }
      }
      catch (Exception exx)
      {
        res.StatusCode = (int)status;
        res.ContentType = "text/plain; charset=UTF-8";

        var ln = Environment.NewLine;
        await res.WriteAsync(
          $"{(int)status} - {status.ToString().ChangeCase(TextCase.ProperCase)}{ln}{message}{ln}Fault:{ln}{exx.GetStackTrace()}{ln}Caused by:{ln}{ex.GetStackTrace()}"
        );
      }
    }
  }
}