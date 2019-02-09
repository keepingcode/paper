using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Extensions.Site
{
  [Expose]
  public class SiteMapPipeline : IPipeline
  {
    private readonly ISiteMapCatalog siteMapCatalog;

    public SiteMapPipeline(ISiteMapCatalog siteMapCatalog)
    {
      this.siteMapCatalog = siteMapCatalog;
    }

    public string Route { get; } = "/Paper/SiteMap";

    public async Task RenderAsync(Request request, Response response, NextAsync next)
    {
      var path = request.Path.Substring(Route.Length);
      var route = new UriString(this.Route);

      var catalog = siteMapCatalog.FindExact(path).FirstOrDefault();
      if (catalog == null)
      {
        await next.Invoke();
        return;
      }

      var parent = siteMapCatalog.FindExact(path + "/..").FirstOrDefault();
      var children = siteMapCatalog.FindExact(path + "/*").ToArray();

      var entity = new Entity();
      var rowHeaders = new List<string>();

      entity.AddClass(ClassNames.Record);
      entity.AddClass(Conventions.MakeName(typeof(ISiteMap)));
      entity.SetTitle(catalog.Title);
      entity.AddLinkSelf(route.Append(catalog.Href).ToHref());
      foreach (var property in catalog.Properties.NonNull())
      {
        if (property.Value != null)
        {
          entity.AddDataHeader(property.Name, dataType: Conventions.MakeDataType(property.Value.GetType()));
          entity.AddProperty(property.Name, property.Value);
        }
      }

      if (parent != null)
      {
        entity.AddLink(route.Append(parent.Href).ToHref(), parent.Title, Rel.Up);
      }

      var subItems = children.Concat(catalog.Items.NonNull());
      foreach (var item in subItems)
      {
        var row = new Entity();
        row.AddClass(ClassNames.Data);
        row.AddClass(Conventions.MakeName((item is ISiteMap) ? typeof(ISiteMap) : typeof(IRoute)));
        row.AddRel(RelNames.Rows);
        row.SetTitle(item.Title);
        row.AddLinkSelf((item is ISiteMap) ? route.Append(item.Href).ToHref() : item.Href);
        foreach (var property in item.Properties.NonNull())
        {
          if (property.Value == null)
            continue;

          if (!property.Name.EqualsAnyIgnoreCase(rowHeaders))
          {
            rowHeaders.Add(property.Name);
            entity.AddRowHeader(property.Name, dataType: Conventions.MakeDataType(property.Value.GetType()));
          }
          row.AddDataHeader(property.Name, dataType: Conventions.MakeDataType(property.Value.GetType()));
          row.AddProperty(property.Name, property.Value);
        }
        entity.AddRow(row);
      }

      await response.WriteEntityAsync(entity);
    }
  }
}
