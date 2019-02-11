using System;
using System.Collections.Generic;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media
{
  public static class HrefExtensions
  {
    public static Href ToHref(this UriString uri)
    {
      return (Href)uri.ToString();
    }

    public static void ExpandUri(this Href href, UriString requestUri, UriString pathBase)
    {
      DoExpandUri(href, requestUri, pathBase);
    }

    public static void ExpandUri(this IHyperLink href, UriString requestUri, UriString pathBase)
    {
      DoExpandUri(href.Href, requestUri, pathBase);
    }

    public static void ExpandUri(this Entity entity, UriString requestUri, UriString pathBase)
    {
      foreach (var action in entity.Actions.NonNull())
      {
        ExpandUri(action, requestUri, pathBase);
        foreach (var field in action.Fields.NonNull())
        {
          if (field.Provider != null)
          {
            DoExpandUri(field.Provider.Href, requestUri, pathBase);
          }
        }
      }
      foreach (var link in entity.Links.NonNull())
      {
        DoExpandUri(link.Href, requestUri, pathBase);
      }
      foreach (var child in entity.Entities.NonNull())
      {
        ExpandUri(child, requestUri, pathBase);
      }
    }

    private static void DoExpandUri(this Href href, UriString requestUri, UriString pathBase)
    {
      if (href != null)
      {
        href.Value = requestUri.Combine(pathBase, href.Value);
      }
    }
  }
}