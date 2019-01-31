using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Design;
using Toolset;

namespace Paper.Api.Papers
{
  [Expose]
  public class SandboxPaper : IPaper
  {
    public string Route { get; } = "/Users/{UserId}/Profile";

    public async Task RenderAsync(RenderingContext context, NextAsync next)
    {
      var req = context.Request;
      var res = context.Response;

      var args = new Args(new PathArgs(req.Path, Route), req.QueryArgs);

      Debug.WriteLine($"- - - - - -");
      foreach (var key in args.Keys)
      {
        Debug.WriteLine($"{key}={args[key]}");
      }
      Debug.WriteLine($"- - - - - -");

      var uri = new UriString("http://host.com/?id=10&ten=20")
        .Combine(req.PathBase)
        .Append(new PathArgs(req.Path, Route).ToString());

      var entity = HttpEntity.Create(req.RequestUri, HttpStatusCode.OK);
      entity.Value.AddTitle(uri);

      await res.WriteEntityAsync(entity);
    }
  }
}
