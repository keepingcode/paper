using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Paper.Media.Serialization;
using Sandbox.Lib;
using Toolset.Reflection;

namespace Sandbox.Bot.Net
{
  public class PaperClient
  {
    private readonly WebClient web;

    public PaperClient(string endpoint = null)
    {
      web = new WebClient();
      web.Encoding = Encoding.UTF8;
      web.BaseAddress = endpoint ?? Settings.Endpoint;
    }

    public async Task<Entity> ReadAsync(string route, object args = null)
    {
      if (args != null)
      {
        route = new UriString(route).SetArgs(args);
      }

      var uri = new Uri(route, UriKind.RelativeOrAbsolute);
      var body = await web.DownloadStringTaskAsync(uri);

      var serializer = new MediaSerializer();
      var entity = serializer.Deserialize(body);

      return entity;
    }

    public async Task<byte[]> ReadBytesAsync(string route, object args = null)
    {
      if (args != null)
      {
        route = new UriString(route).SetArgs(args);
      }

      var uri = new Uri(route, UriKind.RelativeOrAbsolute);
      var bytes = await web.DownloadDataTaskAsync(uri);

      return bytes;
    }
  }
}
