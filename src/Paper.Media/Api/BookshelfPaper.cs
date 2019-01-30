using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Paper.Media.Design;
using Paper.Media.Rendering;
using Toolset;

namespace Paper.Media.Api
{
  [Expose]
  public class BookshelfPaper : IPaper
  {
    private Bookshelf bookshelf;

    public BookshelfPaper(Bookshelf bookshelf)
    {
      this.bookshelf = bookshelf;
    }

    public string Route { get; } = "/Bookshelf";

    public async Task RenderAsync(RenderingContext ctx, NextAsync next)
    {
      var match = Regex.Match(ctx.Request.Path, $"^/Bookshelf/([^/]+)(.*)");
      var catalogId = match.Success ? match.Groups[1].Value : null;
      var paperPath = match.Success ? match.Groups[2].Value : null;

      if (!string.IsNullOrEmpty(paperPath))
      {
        await RenderPaperAsync(ctx, catalogId, paperPath);
      }
      else if (!string.IsNullOrEmpty(catalogId))
      {
        await RenderCatalogAsync(ctx, catalogId);
      }
      else
      {
        await RenderBookhshelfAsync(ctx);
      }
    }

    private async Task RenderPaperAsync(RenderingContext ctx, string catalogId, string paperPath)
    {
      var req = ctx.Request;
      var res = ctx.Response;
      var uri = new UriString(req.RequestUri);

      var papers = bookshelf.GetCatalogPapers(catalogId);
      var paper = papers?.FirstOrDefault(x => x.Route.EqualsIgnoreCase(paperPath));
      if (paper == null)
      {
        var fault = HttpEntity.Create(req.RequestUri, HttpStatusCode.NotFound);
        res.Status = HttpStatusCode.NotFound;
        await res.WriteEntityAsync(fault);
        return;
      }

      var entity = new Entity();
      entity.AddClass(ClassNames.Data);
      entity.AddTitle("Paper");
      entity.AddData(paper);
      entity.AddLink(uri.Combine(".."), "Catalog");
      entity.AddLink(uri.Combine("../.."), "Bookshelf");

      await res.WriteEntityAsync(entity);
    }

    private async Task RenderCatalogAsync(RenderingContext ctx, string catalogId)
    {
      var req = ctx.Request;
      var res = ctx.Response;
      var uri = new UriString(req.RequestUri);

      var papers = bookshelf.GetCatalogPapers(catalogId);
      if (papers == null)
      {
        var fault = HttpEntity.Create(req.RequestUri, HttpStatusCode.NotFound);
        res.Status = HttpStatusCode.NotFound;
        await res.WriteEntityAsync(fault);
        return;
      }
      
      var entity = new Entity();
      entity.AddClass(ClassNames.Rows);
      entity.AddTitle("Catalog");
      entity.AddData(new { catalogId });
      entity.AddRowHeadersFrom<IPaper>();
      entity.AddLink(uri.Combine(".."), "Bookshelf");

      foreach (var paper in papers)
      {
        var item = new Entity();
        item.AddClass(ClassNames.Row);
        item.AddTitle("Paper");
        item.AddProperties(paper);
        item.AddLinkSelf(uri.Append(paper.Route));
        entity.AddEntity(item);
      }

      await res.WriteEntityAsync(entity);
    }

    private async Task RenderBookhshelfAsync(RenderingContext ctx)
    {
      var req = ctx.Request;
      var res = ctx.Response;
      var uri = new UriString(req.RequestUri);

      var entity = new Entity();
      entity.AddClass(ClassNames.Rows);
      entity.AddTitle("Bookshelf");
      entity.AddRowHeader("catalogId", "CatalogId");

      foreach (var catalogId in bookshelf.GetCatalogIds())
      {
        var item = new Entity();
        item.AddClass(ClassNames.Row);
        item.AddTitle("Catalog");
        item.AddProperties(new { catalogId });
        item.AddLinkSelf(uri.Append(catalogId));
        entity.AddEntity(item);
      }

      await res.WriteEntityAsync(entity);
    }
  }
}