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
    public static PaperLink Self<TPaper>()
      where TPaper : IPaper
    {
      return Self(typeof(TPaper), (object[])null);
    }

    public static PaperLink Self<TPaper>(object arg)
      where TPaper : IPaper
    {
      return Self(typeof(TPaper), new[] { arg });
    }

    public static PaperLink Self<TPaper>(object[] args)
      where TPaper : IPaper
    {
      return Self(typeof(TPaper), args);
    }

    public static PaperLink Self(Type paperType, object arg)
    {
      return Self(paperType, new[] { arg });
    }

    public static PaperLink Self(Type paperType, object[] args)
    {
      return new PaperLink((factory, entity) =>
      {
        // TODO: deveriamos consultar o catalogo
        // var paperCatalog = factory.GetInstance<IPaperCatalog>();
        // paperCatalog.FindByType(typeof(TPaper));

        var paper = (IPaper)Activator.CreateInstance(paperType);
        var paperDescriptor = new PaperDescriptor(paper);

        var href = paperDescriptor.PathTemplate.Substring(1);

        if (args != null)
        {
          foreach (var arg in args)
          {
            var value = Change.To<string>(arg);
            href = Regex.Replace(href, @"\{[^{}]+\}", value);
          }
        }

        entity.SetSelfLink(href);
      });
    }

    public static PaperLink Self(Href href)
    {
      return new PaperLink((factory, entity) => entity.SetSelfLink(href));
    }

    public static PaperLink Link<TPaper>(Action<Link> opt)
    {
      return Link(typeof(TPaper), (object[])null, opt);
    }

    public static PaperLink Link<TPaper>(object arg, Action<Link> opt)
    {
      return Link(typeof(TPaper), new[] { arg }, opt);
    }

    public static PaperLink Link<TPaper>(object[] args, Action<Link> opt)
    {
      return Link(typeof(TPaper), args, opt);
    }

    public static PaperLink Link(Type paperType, object arg, Action<Link> opt)
    {
      return Link(paperType, new[] { arg }, opt);
    }

    public static PaperLink Link(Type paperType, object[] args, Action<Link> opt)
    {
      return new PaperLink((factory, entity) =>
      {
        // TODO: deveriamos consultar o catalogo
        // var paperCatalog = factory.GetInstance<IPaperCatalog>();
        // paperCatalog.FindByType(typeof(TPaper));

        var paper = (IPaper)Activator.CreateInstance(paperType);
        var paperDescriptor = new PaperDescriptor(paper);

        var href = paperDescriptor.PathTemplate.Substring(1);

        if (args != null)
        {
          foreach (var arg in args)
          {
            var value = Change.To<string>(arg);
            href = Regex.Replace(href, @"\{[^{}]+\}", value);
          }
        }

        AddLinkToEntity(entity, href, opt);
      });
    }

    public static PaperLink Link(Href href, Action<Link> opt)
    {
      return new PaperLink((factory, entity) => AddLinkToEntity(entity, href, opt));
    }

    private static void AddLinkToEntity(Entity entity, Href href, Action<Link> opt)
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