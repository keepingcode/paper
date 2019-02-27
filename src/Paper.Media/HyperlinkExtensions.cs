using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media
{
  public static class HyperlinkExtensions
  {
    public static THyperLink SetHref<THyperLink>(this THyperLink hyperLink, string href)
      where THyperLink : IHyperLink
    {
      hyperLink.Href = href;
      return hyperLink;
    }
  }
}