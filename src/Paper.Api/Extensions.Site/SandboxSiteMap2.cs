using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Api.Extensions.Site
{
  [Expose]
  public class SandboxSiteMap2 : SiteMap
  {
    public SandboxSiteMap2()
    {
      this.Href = "/Sandbox/2";
      this.Items.Add(new Route
      {
        Href = $"{this.Href}/My/First/Page/2",
        Title = "Rota Talz 2",
        Properties = new Media.PropertyMap
        {
          { "icon", "teoria" }
        }
      });
    }
  }
}
