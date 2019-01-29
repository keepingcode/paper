using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media
{
  /// <summary>
  /// Interface para parte da entidade que contém um link de referência.
  /// </summary>
  public interface IHyperLink
  {
    Href Href { get; set; }
  }
}
