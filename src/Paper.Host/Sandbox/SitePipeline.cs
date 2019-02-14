using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Design;
using Toolset;

namespace Paper.Host.Sandbox
{
  [Expose]
  public class SitePipeline : IPipeline
  {
    public SitePipeline()
    {
      SiteDescriptor = new SiteDescriptor();
      SiteDescriptor.MapRenderer("/Users", Method.Get, RenderUsersAsync);
      SiteDescriptor.MapActionRenderer("/Users", MakeAction1);
      SiteDescriptor.MapActionRenderer("/Users", MakeAction2);
    }

    public string Route { get; } = "/Sandbox";

    public SiteDescriptor SiteDescriptor { get; }

    public async Task RenderAsync(Request request, Response response, NextAsync next)
    {
      var path = request.Path.Substring(Route.Length);

      var renderer = SiteDescriptor.GetRenderer(path, request.Method);
      if (renderer != null)
      {
        await renderer.Invoke(request, response, next);
      }
      else
      {
        await next.Invoke();
      }
    }

    private async Task RenderUsersAsync(Request request, Response response, NextAsync next)
    {
      var entity = new Entity();
      entity.AddClass(ClassNames.Record, ClassNames.List, ClassNames.Cards, "User");
      entity.AddProperties(new
      {
        Id = 10,
        Name = "Fu Lano"
      });
      entity.AddEntities(
        new Entity()
          .AddClass(ClassNames.Record, ClassNames.Card, "User")
          .AddRel(Rel.Item, Rel.Card)
          .AddProperties(new
          {
            Id = 11,
            Name = "Bel Trano"
          }),
        new Entity()
          .AddClass(ClassNames.Record, ClassNames.Card, "User")
          .AddRel(Rel.Item, Rel.Card)
          .AddProperties(new
          {
            Id = 12,
            Name = "Cic Rano"
          })
      );

      var path = request.Path.Substring(Route.Length);
      var actionRenderers = SiteDescriptor.GetActionRenderers(path);
      foreach (var actionRenderer in actionRenderers)
      {
        var actions = actionRenderer.Invoke(request, response);
        entity.AddActions(actions);
      }

      await response.WriteEntityAsync(entity);
    }

    private IEnumerable<EntityAction> MakeAction1(Request request, Response response)
    {
      var href = new UriString(Route).Append(request.Path);
      yield return new EntityAction()
        .SetHref(href.ToHref())
        .SetName("action1")
        .SetMethod(Method.Post)
        ;
    }

    private IEnumerable<EntityAction> MakeAction2(Request request, Response response)
    {
      var href = new UriString(Route).Append(request.Path);
      yield return new EntityAction()
        .SetHref(href.ToHref())
        .SetName("action2")
        .SetMethod(Method.Post)
        ;
    }
  }
}
