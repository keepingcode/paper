using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Api.Extensions.Site
{
  [Expose]
  public class SandboxSiteMap : SiteMap
  {
    public SandboxSiteMap()
    {
      this.Href = "/Sandbox";
      this.Items.Add(new Route
      {
        Href = $"{this.Href}/My/First/Page",
        Title = "Rota Talz",
        Properties = new Media.PropertyMap
        {
          { "icon", "peneira" }
        }
      });
    }
  }
}
