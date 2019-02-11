using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media.Design
{
  /// <summary>
  /// Extensões de desenho de links de objetos Entity.
  /// </summary>
  public static class LinkExtensions
  {
    public static Link SetHref(this Link link, Href href)
    {
      link.Href = href;
      return link;
    }

    /// <summary>
    /// Define o tipo (MimeType) do documento retornado pelo link.
    /// Como "text/xml", "application/pdf", etc.
    /// </summary>
    /// <param name="link">O link a ser modificado.</param>
    /// <param name="mediaType">O mime type do link.</param>
    /// <returns>A própria instância do link modificado.</returns>
    public static Link SetType(this Link link, string mediaType)
    {
      link.Type = mediaType;
      return link;
    }
  }
}