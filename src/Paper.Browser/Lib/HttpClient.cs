using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Toolset;
using Toolset.Net;

namespace Paper.Browser.Lib
{
  public class HttpClient
  {
    private Http http;

    public HttpClient()
    {
      this.http = new Http();
    }

    public HttpClient(Http http)
    {
      this.http = http;
    }

    public async Task<Ret<Content>> RequestAsync(
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

    public async Task<Ret<Content>> RequestAsync(
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

    private async Task<Ret<Content>> SendAsync(
        string uri
      , string method
      , object upData
      , string upType
      , string upCharset
      , string downType
      , string downCharset
      , int maxRedirect = 5
      )
    {
      try
      {
        var ret = await http.RequestAsync(uri, method, upData, upType, upCharset, downType, downCharset);

        var isRedirect = (ret.Status.CodeClass == HttpStatusClass.Redirection) && (maxRedirect > 0);
        if (isRedirect)
        {
          var redirectUri = ret.Status.Headers[HeaderNames.Location] ?? uri;
          switch (ret.Status.Code)
          {
            case HttpStatusCode.Redirect:
              {
                return await SendAsync(
                    redirectUri
                  , MethodNames.Get
                  , upData: null
                  , upType: null
                  , upCharset: null
                  , downType: downType
                  , downCharset: downCharset
                  , maxRedirect: maxRedirect - 1
                );
              }
            case HttpStatusCode.RedirectKeepVerb:
              {
                return await SendAsync(
                    redirectUri
                  , method
                  , upData
                  , upType
                  , upCharset
                  , downType
                  , downCharset
                  , maxRedirect - 1
                );
              }
          }
        }

        if (ret.Value?.Data == null)
        {
          var faultEntity = HttpEntity.CreateFromRet(uri, ret);
          ret.Value = new Content
          {
            Href = uri,
            Data = faultEntity.Value
          };
        }

        return ret;
      }
      catch (Exception ex)
      {
        var faultEntity = HttpEntity.Create(uri, ex);
        return new Ret<Content>
        {
          Status = faultEntity.Status,
          Fault = faultEntity.Fault,
          Value = new Content
          {
            Href = uri,
            Data = faultEntity.Value
          }
        };
      }
    }
  }
}