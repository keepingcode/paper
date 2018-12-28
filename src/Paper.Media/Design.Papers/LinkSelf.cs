using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;
using Paper.Media.Rendering;

namespace Paper.Media.Design.Papers
{
  /// <summary>
  /// Cria o link da própria entidade.
  /// </summary>
  /// <seealso cref="Paper.Media.Link" />
  /// <seealso cref="Media.Design.Extensions.Papers.ILink" />
  public class LinkSelf : LinkTo
  {
    public LinkSelf(string href = null, string title = null)
      : base(href, title, RelNames.Self)
    {
    }
  }
}