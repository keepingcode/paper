using System;
using System.Linq;
using Paper.Media.Routing;
using Paper.Media.Utilities.Types;
using Toolset;
using Toolset.Collections;
using Toolset.Reflection;

namespace Paper.Media.Design.Papers
{
  /// <summary>
  /// Fábrica para criação de link para outro Paper.
  /// </summary>
  /// <typeparam name="T">O tipo do Paper.</typeparam>
  /// <seealso cref="Media.Design.Extensions.Papers.ILinkFactory{Media.Design.Extensions.Papers.LinkToPaper{T}.Context}" />
  public class LinkTo<T> : ILink
    where T : IPaper
  {
    private readonly Action<T> setup;
    private readonly Action<Link> builder;

    public LinkTo(Action<T> setup = null, Action<Link> builder = null)
    {
      this.setup = setup;
      this.builder = builder;
    }

    public Link RenderLink(IContext ctx)
    {
      var paper = ctx.Factory.CreateInstance<T>();

      setup?.Invoke(paper);

      var link = new Link
      {
        Href = CreateHref(ctx, paper)
      };

      if (paper._Has<string>("GetTitle"))
      {
        link.Title = paper._Call<string>("GetTitle");
      }

      builder?.Invoke(link);

      if (link.Rel?.Any() != true)
        link.Rel = RelNames.Link;

      return link;
    }

    private string CreateHref(IContext ctx, T paper)
    {
      var paperBlueprint = ctx.Catalog.GetPaperBlueprint<T>();
      var paperTemplate = new UriTemplate(paperBlueprint.UriTemplate);

      paperTemplate.SetArgsFromGraph(paper);

      var uri = paperTemplate.CreateUri();
      var targetUri = new Route(uri);

      targetUri = targetUri.SetArg(
        "f", ctx.RequestUri.Query["f"],
        "in", ctx.RequestUri.Query["in"],
        "out", ctx.RequestUri.Query["out"]
      );

      var filter = paper._Get<IFilter>("Filter");
      if (filter != null)
      {
        var map = new FieldMap(filter);
        var args = (
          from field in map
          where field.Value != null
          select new[] {
            field.Key.ChangeCase(TextCase.CamelCase),
            field.Value
          }
        ).SelectMany();
        targetUri = targetUri.SetArg(args);
      }

      return targetUri.ToString();
    }
  }
}