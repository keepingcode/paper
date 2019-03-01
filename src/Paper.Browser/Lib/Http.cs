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

    public async Task<Ret<Content>> RequestAsync(
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
        request.Method = method;
        request.Accept = downType;
        request.Headers[HeaderNames.AcceptCharset] = downCharset;

        if (upData != null)
        {
          request.ContentType = $"{upType}; charset={upCharset}";
          using (var stream = request.GetRequestStream())
          {
            await SerializeAsync(upData, upType, upCharset, stream);
          }
        }

        HttpWebResponse response;

        try
        {
          response = (HttpWebResponse)request.GetResponse();
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
          var downData = new Content { Href = uri };

          using (var stream = response.GetResponseStream())
          {
            downData.Data = await DeserializeAsync(stream, downType, downCharset);
          }

          var headers = new HashMap<string>(response.Headers.Count);
          foreach (string key in response.Headers.Keys)
          {
            headers[key] = response.Headers[key];
          }

          return new Ret<Content>
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
        ex.Trace();
        return ex;
      }
    }

    private async Task SerializeAsync(object source, string type, string charset, Stream target)
    {
      if (source is Entity entity)
      {
        var serializer = new MediaSerializer(type);
        serializer.Serialize(entity, target, charset);
        return;
      }

      if (source is byte[] bytes)
      {
        await target.WriteAsync(bytes, 0, bytes.Length);
        return;
      }

      if (source is string body)
      {
        using (var writer = new StreamWriter(target))
        {
          await writer.WriteAsync(body);
          await writer.FlushAsync();
        }
        return;
      }

      throw new MediaException("Formato de dados não suportado: " + source.GetType().FullName);
    }

    private async Task<object> DeserializeAsync(Stream source, string type, string charset)
    {
      using (var memory = new MemoryStream())
      {
        await source.CopyToAsync(memory);
        memory.Position = 0;

        if (memory.Length == 0)
          return null;

        if (type.Contains("siren"))
        {
          var serializer = new MediaSerializer(type);
          var entity = serializer.Deserialize(memory, charset);
          return entity;
        }

        return memory.ToArray();
      }
    }
  }
}