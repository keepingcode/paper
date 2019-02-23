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
using Toolset.Collections;
using Toolset.Net;

namespace Paper.Browser.Lib
{
  public class Http
  {
    public Http()
    {
      Prefix = Settings.Prefix;
      Endpoint = Settings.Endpoint;
    }

    public string Prefix { get; }
    public string Endpoint { get; }

    public async Task<Ret<Result>> RequestAsync(
        string uri
      , string method
      , Entity upData
      , string upType
      , string upCharset
      , string downType
      , string downCharset
      )
    {
      return await SendAsync(uri, method, upData, upType, upCharset, downType, downCharset);
    }

    public async Task<Ret<Result>> RequestAsync(
        string uri
      , string method
      , byte[] upData
      , string upType
      , string upCharset
      , string downType
      , string downCharset
      )
    {
      return await SendAsync(uri, method, upData, upType, upCharset, downType, downCharset);
    }

    private async Task<Ret<Result>> SendAsync(
        string uri
      , string method
      , object upData
      , string upType
      , string upCharset
      , string downType
      , string downCharset
      )
    {
      try
      {
        if (!uri.Contains("://"))
        {
          uri = new UriString(Endpoint).Combine(Prefix).Combine(uri);
        }

        var request = HttpWebRequest.CreateHttp(uri);

        if (upData is Entity entity)
        {
          request.Headers[HeaderNames.ContentType] = $"{upType}; charset={upCharset}";
          using (var stream = await request.GetRequestStreamAsync())
          {
            var serializer = new MediaSerializer(upType);
            serializer.Serialize(entity, stream, upCharset);
          }
        }
        else if (upData is byte[] bytes)
        {
          request.Headers[HeaderNames.ContentType] = $"{upType}; charset={upCharset}";
          using (var stream = await request.GetRequestStreamAsync())
          {
            await stream.WriteAsync(bytes, 0, bytes.Length);
          }
        }

        request.Headers[HeaderNames.Accept] = downType;
        request.Headers[HeaderNames.AcceptCharset] = downCharset;

        HttpWebResponse response;

        try
        {
          response = (HttpWebResponse)await request.GetResponseAsync();
        }
        catch (WebException ex)
        {
          response = ex.Response as HttpWebResponse;
          if (response == null)
          {
            throw;
          }
        }

        using (response)
        {
          byte[] bytes;

          using (var stream = response.GetResponseStream())
          using (var memory = new MemoryStream())
          {
            await stream.CopyToAsync(memory);
            bytes = memory.Length > 0 ? memory.ToArray() : null;
          }

          Result downData = null;
          if (bytes.Length > 0)
          {
            if (downType.Contains("siren"))
            {
              using (var memory = new MemoryStream())
              {
                await memory.WriteAsync(bytes, 0, bytes.Length);
                memory.Position = 0;
                var serializer = new MediaSerializer(downType);
                downData = serializer.Deserialize(memory, downCharset);
              }
            }
            else
            {
              downData = bytes;
            }
          }

          var headers = new HashMap<string>(response.Headers.Count);
          foreach (string key in response.Headers.Keys)
          {
            headers[key] = response.Headers[key];
          }

          return new Ret<Result>
          {
            Status = new Ret.RetStatus
            {
              Code = response.StatusCode,
              Headers = headers
            },
            Value = downData
          };
        }
      }
      catch (Exception ex)
      {
        return ex;
      }
    }
  }
}