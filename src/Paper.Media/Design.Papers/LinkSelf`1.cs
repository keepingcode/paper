using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Paper.Media;
using Paper.Media.Rendering;
using Toolset.Collections;

namespace Paper.Media.Design.Papers
{
  /// <summary>
  /// Cria o link da própria entidade.
  /// </summary>
  /// <seealso cref="Paper.Media.Link" />
  /// <seealso cref="Media.Design.Extensions.Papers.ILink" />
  public class LinkSelf<T> : LinkTo<T>
      where T : IPaper
  {
    public LinkSelf(string title = null)
      : base(title, RelNames.Self)
    {
    }

    public LinkSelf(Action<T> setup, string title = null)
      : base(setup, title, RelNames.Self)
    {
    }

    public LinkSelf(Action<T> setup, Action<Link> builder)
      : base(setup, link =>
      {
        builder?.Invoke(link);
        link.AddRel(RelNames.Self);
      })
    {
    }
  }
}