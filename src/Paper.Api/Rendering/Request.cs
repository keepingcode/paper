using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Paper.Media.Serialization;

namespace Paper.Api.Rendering
{
  public class Request : IHttpRequest
  {
    private IHttpRequest request;

    public Request(IHttpRequest request)
    {
      this.request = request;
      this.QueryArgs = new QueryArgs(request.RequestUri);
    }

    public string RequestUri => request.RequestUri;

    public string PathBase => request.PathBase;

    public string Path => request.Path;

    public string Method => request.Method;

    public Headers Headers => request.Headers;

    public Stream Body => request.Body;

    public QueryArgs QueryArgs { get; }

    public async Task<Entity> ReadEntityAsync()
    {
      var contentHeader = new ContentHeader(Headers);
      var mimeType = contentHeader.Type;
      var encoding = contentHeader.Encoding;

      var serializer = new MediaSerializer(mimeType);
      var entity = serializer.Deserialize(Body, encoding);
      return await Task.FromResult(entity);
    }
  }
}