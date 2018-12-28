using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;
using Paper.Media.Rendering;

namespace Paper.Media.Design.Papers
{
  /// <summary>
  /// Cria um link para uma URI.
  /// </summary>
  /// <seealso cref="Paper.Media.Link" />
  /// <seealso cref="Media.Design.Extensions.Papers.ILink" />
  public class LinkTo : Link, ILink
  {
    public LinkTo(string href = null, string title = null, NameCollection rel = null)
    {
      this.Href = href;
      this.Title = title;
      this.Rel = rel ?? RelNames.Link;
    }

    public Link RenderLink(PaperContext ctx)
    {
      return this;
    }
  }
}