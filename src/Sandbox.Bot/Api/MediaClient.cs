using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Sandbox.Lib;
using Toolset;

namespace Sandbox.Bot.Api
{
  class MediaClient
  {
    public static readonly MediaClient Current = new MediaClient();

    private MediaClient()
    {
    }

    public UriString GetEndpoint() => new UriString(Settings.Endpoint);
    public UriString GetApiEndpoint() => new UriString(Settings.Endpoint, "/Api/1");

    public async Task<Ret> CheckConnectivityAsync()
    {
      try
      {
        var uri = GetApiEndpoint().Combine("/Status");
        var client = WebRequest.CreateHttp(uri.ToString());
        using (var response = await client.GetResponseAsync())
        using (var stream = response.GetResponseStream())
        {
          try
          {
            // Checando se a resposta é uma entidade (Entity) válida.
            EntityParser.ParseEntity(stream);

            // TODO: Futuramente a entidade pode ser investigada para
            // determinar se é uma entidade compatível com a esperada
            // para a consulta de status.

            return Ret.Ok();
          }
          catch (Exception ex)
          {
            ex.Trace();
            return Ret.As(HttpStatusCode.UnsupportedMediaType);
          }
        }
      }
      catch (Exception ex)
      {
        return ex;
      }
    }

    public async Task<Ret<Entity>> FindEntityAsync(string route, object args = null)
    {
      try
      {
        var uri = GetApiEndpoint().Combine(route).SetArgs(args);
        var client = WebRequest.CreateHttp(uri.ToString());

        HttpWebResponse webResponse;
        try
        {
          webResponse = (HttpWebResponse)await client.GetResponseAsync();
        }
        catch (Exception ex)
        {
          webResponse = (ex as WebException)?.Response as HttpWebResponse;

          if (webResponse == null)
            throw;
        }

        using (webResponse)
        using (var stream = webResponse.GetResponseStream())
        {
          var entity = EntityParser.ParseEntity(stream);
          return Ret.As(webResponse.StatusCode, entity);
        }
      }
      catch (Exception ex)
      {
        return ex;
      }
    }

    public async Task<Ret<byte[]>> DownloadAsync(string route, object args = null)
    {
      try
      {
        var uri = GetEndpoint().Combine(route).SetArgs(args);
        var client = WebRequest.CreateHttp(uri.ToString());
        using (var response = (HttpWebResponse)await client.GetResponseAsync())
        {
          if (response.StatusCode != HttpStatusCode.OK)
          {
            return Ret.Fail(response.StatusCode);
          }

          using (var stream = response.GetResponseStream())
          using (var memory = new MemoryStream())
          {
            stream.CopyTo(memory);
            var bytes = memory.ToArray();
            return bytes;
          }
        }
      }
      catch (Exception ex)
      {
        return ex;
      }
    }
  }
}
