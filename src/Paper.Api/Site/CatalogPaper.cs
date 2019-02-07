using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Toolset;

namespace Paper.Api.Site
{
  [Expose]
  public class CatalogPaper : IPaper
  {
    private readonly ICatalog catalog;

    public string Route { get; } = "/Sandbox";

    //public CatalogPaper(ICatalog catalog)
    public CatalogPaper()
    {
      //this.catalog = catalog;
    }

    public async Task RenderAsync(RenderingContext context, NextAsync next)
    {
      var req = context.Request;
      var res = context.Response;



      var entity = await req.ReadEntityAsync();

      await res.WriteEntityAsync(entity);
    }
  }
}