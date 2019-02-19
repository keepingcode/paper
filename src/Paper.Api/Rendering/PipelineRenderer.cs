using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Net;

namespace Paper.Api.Rendering
{
  public class PipelineRenderer
  {
    private IPipelineCatalog pipelineCatalog;
    private IObjectFactory objectFactory;

    public PipelineRenderer(IPipelineCatalog pipelineCatalog, IObjectFactory objectFactory)
    {
      this.pipelineCatalog = pipelineCatalog;
      this.objectFactory = objectFactory;
    }

    public async Task RenderAsync(Request req, Response res)
    {
      try
      {
        var papers = pipelineCatalog?.Find(req.Path);
        if (papers == null)
        {
          var status = HttpStatusCode.ServiceUnavailable;
          var message =
            "Paper não configurado corretamente. " +
            "A instância de IPaperCatalog não foi configurada na instância de IServiceProvider.";
          await WriteAsync(req, res, status, message, null);
          return;
        }

        if (!papers.Any())
        {
          var status = HttpStatusCode.NotFound;
          var message = $"Não existe uma rota para: {req.PathBase}{req.Path}";
          await WriteAsync(req, res, status, message, null);
          return;
        }

        var enumerator = papers.GetEnumerator();

        NextAsync next = null;

        next = new NextAsync(async () =>
        {
          if (enumerator.MoveNext())
          {
            var paper = enumerator.Current;
            await paper.RenderAsync(req, res, next);
          }
          else
          {
            await WriteAsync(req, res, HttpStatusCode.NotFound, null, null);
          }
        });

        await next.Invoke();
      }
      catch (Exception ex)
      {
        var status = ex is HttpException http ? http.Status : HttpStatusCode.InternalServerError;
        await WriteAsync(req, res, status, ex.Message, ex);
      }
    }

    private async Task WriteAsync(Request req, Response res, HttpStatusCode status, string message, Exception ex)
    {
      try
      {
        if (message == null)
        {
          message = ex?.Message ?? status.ToString().ChangeCase(TextCase.ProperCase);
        }

        var validMimeTypes = new[] {
          MediaSerializer.JsonSiren,
          MediaSerializer.Json,
          MediaSerializer.XmlSiren,
          MediaSerializer.Xml
        };

        var accept = new AcceptHeader(req.Headers, req.QueryArgs);
        var encoding = accept.BestEncoding;
        var mimeType = accept.SelectBestMatch(accept.MimeTypes, validMimeTypes) ?? MediaSerializer.Json;
        var renderType = mimeType.Contains("json") ? MediaSerializer.JsonSiren : MediaSerializer.XmlSiren;

        var serializer = new MediaSerializer(renderType);
        using (var memory = new MemoryStream())
        {
          var entity = (ex != null)
            ? HttpEntity.Create(req.RequestUri, status, message, ex)
            : HttpEntity.Create(req.RequestUri, status, message);

          serializer.Serialize(entity, memory, encoding);

          memory.Position = 0;

          res.Status = status;
          res.Headers[HeaderNames.ContentType] = $"{mimeType}; charset={encoding.WebName}";

          await memory.CopyToAsync(res.Body);
        }
      }
      catch (Exception exx)
      {
        res.Status = status;
        res.Headers[HeaderNames.ContentType] = $"text/plain; charset=UTF-8";

        var ln = Environment.NewLine;
        var description = status.ToString().ChangeCase(TextCase.ProperCase);

        var writer = new StreamWriter(res.Body, Encoding.UTF8, bufferSize: 8 * 1024, leaveOpen: true);
        await writer.WriteAsync(
          $"{(int)status} - {description}{ln}{message}{ln}Fault:{ln}{exx.GetStackTrace()}{ln}Caused by:{ln}{ex.GetStackTrace()}"
        );
      }
    }
  }
}