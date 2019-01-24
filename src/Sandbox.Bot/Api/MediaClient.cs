using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Paper.Media.Serialization;
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

    public async Task<Ret> CheckConnectivityAsync()
    {
      try
      {
        var uri = new UriString(Settings.Endpoint).Combine(ApiInfo.Prefix, "/Status");
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
            return Ret.Ok(HttpStatusCode.UnsupportedMediaType);
          }
        }
      }
      catch (Exception ex)
      {
        return ex;
      }
    }

    public async Task<Ret<Result>> TransferAsync(
      UriString route,
      string method = MethodNames.Get,
      Entity requestData = null)
    {
      try
      {
        Ret ret = null;
        await DoRequestAsync(route, method, requestData, webResponse =>
        {
          var isRedirect = ((int)webResponse.StatusCode / 100) == 3;
          if (isRedirect)
          {
            var location = webResponse.Headers[HttpResponseHeader.Location];
            var uri = new Uri(location, UriKind.RelativeOrAbsolute);
            ret = Ret.Ok(uri, webResponse.StatusCode);
          }
          else
          {
            using (var stream = webResponse.GetResponseStream())
            {
              var entity = EntityParser.ParseEntity(stream);
              ret = Ret.Ok(entity, webResponse.StatusCode);
            }
          }
        });
        return ret;
      }
      catch (Exception ex)
      {
        ex.Trace();
        return ex;
      }
    }

    public async Task<Ret<byte[]>> TransferBytesAsync(
      UriString route,
      string method = MethodNames.Get,
      byte[] requestData = null
      )
    {
      try
      {
        Ret ret = null;
        await DoRequestAsync(route, method, requestData, async webResponse =>
        {
          if (webResponse.StatusCode == HttpStatusCode.OK)
          {
            using (var stream = webResponse.GetResponseStream())
            using (var memory = new MemoryStream())
            {
              await stream.CopyToAsync(memory);
              ret = Ret.Ok(memory.ToArray(), webResponse.StatusCode);
            }
          }
          else
          {
            ret = Ret.Fail(webResponse.StatusCode);
          }
        });
        return ret;
      }
      catch (Exception ex)
      {
        ex.Trace();
        return ex;
      }
    }

    private async Task DoRequestAsync(
      UriString route,
      string method,
      object requestData,
      Action<HttpWebResponse> onSuccess)
    {
      if (string.IsNullOrEmpty(route.Host))
      {
        route = new UriString(Settings.Endpoint).Combine(ApiInfo.Prefix).Combine(route);
      }

      var hasData = (requestData != null);
      var hasBody = method.EqualsAnyIgnoreCase(
        MethodNames.Post, MethodNames.Put, MethodNames.Patch);

      if (hasData && !hasBody)
      {
        if (requestData is Entity entity && entity.Properties != null)
        {
          foreach (var property in entity.Properties)
          {
            if (property.Value != null)
            {
              var argName = property.Name.ChangeCase(TextCase.CamelCase);
              route = route.SetArg(argName, property.Value);
            }
          }
        }
        else
        {
          throw new NotSupportedException("Conteúdo não suportado: " + requestData.GetType().FullName);
        }
      }

      var client = WebRequest.CreateHttp((string)route);
      client.Method = method;

      if (hasData && hasBody)
      {
        using (var targetStream = await client.GetRequestStreamAsync())
        {
          if (requestData is byte[] buffer)
          {
            await targetStream.WriteAsync(buffer, 0, buffer.Length);
          }
          else if (requestData is Entity entity)
          {
            new MediaSerializer().SerializeToJson(entity, targetStream);
          }
          else
          {
            throw new NotSupportedException("Conteúdo não suportado: " + requestData.GetType().FullName);
          }
          await targetStream.FlushAsync();
        }
      }

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
      {
        onSuccess.Invoke(webResponse);
      }
    }
  }
}
