using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Design;
using Toolset;

namespace Paper.Api.Extensions.Papers
{
  public static class Links
  {
    public static PaperLink Self<TPaper>(params object[] args)
      where TPaper : IPaper
    {
      return Self(typeof(TPaper), args);
    }

    public static PaperLink Self(Type paperType, params object[] args)
    {
      return new PaperLink((factory, entity) =>
      {
        // TODO: deveriamos consultar o catalogo
        // var paperCatalog = factory.GetInstance<IPaperCatalog>();
        // paperCatalog.FindByType(typeof(TPaper));

        var paper = (IPaper)Activator.CreateInstance(paperType);
        var paperDescriptor = new PaperDescriptor(paper);

        var href = paperDescriptor.PathTemplate;
        foreach (var arg in args)
        {
          var value = Change.To<string>(arg);
          href = Regex.Replace(href, @"\{[^{}]+\}", value);
        }

        entity.SetSelfLink(href);
      });
    }

    public static PaperLink Self(Href href)
    {
      return new PaperLink((factory, entity) => entity.SetSelfLink(href));
    }

    public static PaperLink Link<TPaper>(Action<Link> opt, params object[] args)
    {
      return Link(typeof(TPaper), opt, args);
    }

    public static PaperLink Link(Type paperType, Action<Link> opt, params object[] args)
    {
      return new PaperLink((factory, entity) =>
      {
        // TODO: deveriamos consultar o catalogo
        // var paperCatalog = factory.GetInstance<IPaperCatalog>();
        // paperCatalog.FindByType(typeof(TPaper));

        var paper = (IPaper)Activator.CreateInstance(paperType);
        var paperDescriptor = new PaperDescriptor(paper);

        var href = paperDescriptor.PathTemplate;
        foreach (var arg in args)
        {
          var value = Change.To<string>(arg);
          href = Regex.Replace(href, @"\{[^{}]+\}", value);
        }

        DoAddLink(entity, href, opt);
      });
    }

    public static PaperLink Link(Href href, Action<Link> opt)
    {
      return new PaperLink((factory, entity) => DoAddLink(entity, href, opt));
    }

    private static void DoAddLink(Entity entity, Href href, Action<Link> opt)
    {
      var link = new Link();
      link.SetHref(href);
      opt?.Invoke(link);
      if (link.Rel?.Any() != true)
      {
        link.AddRel(RelNames.Link);
      }
      entity.AddLink(link);
    }
  }
}
