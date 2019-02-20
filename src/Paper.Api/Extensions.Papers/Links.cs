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
    internal class CustomLink : Link, IFormatter
    {
      public event Format OnFormat;

      public void Format(IObjectFactory factory, Entity entity)
      {
        OnFormat?.Invoke(factory, entity);
        if (Rel?.Any() != true)
        {
          this.AddRel(RelNames.Link);
        }
      }
    }

    internal class PaperLink : CustomLink, IFormatter
    {
      private readonly Type paperType;
      private readonly object[] paperArgs;

      public PaperLink(Type paperType, object[] paperArgs)
      {
        this.paperType = paperType;
        this.paperArgs = paperArgs;
      }

      public void Format(IObjectFactory factory, Entity entity)
      {
        base.Format(factory, entity);

        // TODO: deveriamos consultar o catalogo
        // var paperCatalog = factory.GetInstance<IPaperCatalog>();
        // paperCatalog.FindByType(typeof(TPaper));
        var paper = (IPaper)Activator.CreateInstance(paperType);
        var paperDescriptor = new PaperDescriptor(paper);

        var href = paperDescriptor.PathTemplate.Substring(1);

        if (paperArgs != null)
        {
          foreach (var arg in paperArgs)
          {
            var value = Change.To<string>(arg);
            href = Regex.Replace(href, @"\{[^{}]+\}", value);
          }
        }

        this.Href = href;
      }
    }

    #region SelfLink

    public static Link Self<TPaper>(params object[] args)
      where TPaper : IPaper
    {
      return new PaperLink(typeof(TPaper), args).AddRel(RelNames.Self);
    }

    public static Link Self(Type paperType, params object[] args)
    {
      return new PaperLink(paperType, args).AddRel(RelNames.Self);
    }

    public static Link Self(Href href)
    {
      return new Link().SetHref(href).AddRel(RelNames.Self);
    }

    #endregion

    #region Link

    public static Link Link<TPaper>(params object[] args)
    {
      return new PaperLink(typeof(TPaper), args);
    }

    public static Link Link(Type paperType, params object[] args)
    {
      return new PaperLink(paperType, args);
    }

    public static Link Link(Href href)
    {
      return new CustomLink().SetHref(href);
    }

    #endregion
  }
}