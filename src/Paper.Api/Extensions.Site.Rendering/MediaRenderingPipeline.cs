using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Toolset;

namespace Paper.Api.Extensions.Site.Rendering
{
  [Expose]
  public class MediaRenderingPipeline : IPipeline
  {
    private readonly IObjectFactory objectFactory;

    public MediaRenderingPipeline(IObjectFactory objectFactory)
    {
      this.objectFactory = objectFactory;
    }

    public string Route { get; } = "/";

    public async Task RenderAsync(Request request, Response response, NextAsync next)
    {
      var path = request.Path;

      SiteDescriptor siteDescriptor = new SiteDescriptor();

      IEnumerable<MediaRenderer> renderers = siteDescriptor.FindMediaRenderers(path);
      if (renderers?.Any() != true)
      {
        await next.Invoke();
        return;
      }

      var entity = new Entity();

      renderers = renderers.OrderBy(x => x.Priority);

      foreach (var renderer in renderers)
      {
        
      }

      await response.WriteEntityAsync(entity);
    }
  }

  class MediaRenderer
  {
    public int Priority { get; set; }
  }

  class SiteDescriptor
  {

  }
}
