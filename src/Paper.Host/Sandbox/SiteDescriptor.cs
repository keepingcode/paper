using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Toolset.Collections;

namespace Paper.Host.Sandbox
{
  public delegate IEnumerable<EntityAction> ActionRenderer(Request request, Response response);

  public class SiteDescriptor
  {
    private PathIndex<HashMap<Entry>> rendererIndex = new PathIndex<HashMap<Entry>>();

    public Renderer GetRenderer(string route, string method)
    {
      var map = rendererIndex.Get(route);
      return map?[method]?.Renderer;
    }

    public ICollection<ActionRenderer> GetActionRenderers(string route)
    {
      var map = rendererIndex.Get(route);
      return map?[MethodNames.Get]?.ActionRenderers;
    }

    private Entry EnsureEntry(string route, string method)
    {
      var map = rendererIndex.Get(route);
      if (map == null)
      {
        rendererIndex.Add(route, map = new HashMap<Entry>());
      }
      return map[method] ?? (map[method] = new Entry());
    }

    public SiteDescriptor MapRenderer(string route, Method method, Renderer renderer)
    {
      var entry = EnsureEntry(route, method.GetName());
      entry.Renderer = renderer;
      return this;
    }

    public SiteDescriptor MapRenderer(string route, string method, Renderer renderer)
    {
      var entry = EnsureEntry(route, method);
      entry.Renderer = renderer;
      return this;
    }

    public SiteDescriptor MapActionRenderer(string route, ActionRenderer renderer)
    {
      var entry = EnsureEntry(route, MethodNames.Get);
      entry.ActionRenderers.Add(renderer);
      return this;
    }

    public class Entry
    {
      public Renderer Renderer { get; set; }
      public List<ActionRenderer> ActionRenderers { get; } = new List<ActionRenderer>();
    }
  }
}
