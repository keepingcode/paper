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
    /// <summary>
    /// Define o tipo (MimeType) do documento retornado pelo link.
    /// Como "text/xml", "application/pdf", etc.
    /// </summary>
    /// <param name="link">O link a ser modificado.</param>
    /// <param name="mediaType">O mime type do link.</param>
    /// <returns>A própria instância do link modificado.</returns>
    public static Link AddMimeType(this Link link, string mediaType)
    {
      link.Type = mediaType;
      return link;
    }

    #region Extensões de Entity

    /// <summary>
    /// Adiciona um link à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="link">A instância do link.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddLink(this Entity entity, Link link)
    {
      if (entity.Links == null)
      {
        entity.Links = new LinkCollection();
      }
      entity.Links.Add(link);
      return entity;
    }

    /// <summary>
    /// Adiciona um link e invoca um método de construção.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="builder">A função de construção do link.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddLink(this Entity entity, string href, Action<Link> builder)
    {
      if (entity.Links == null)
      {
        entity.Links = new LinkCollection();
      }

      var link = new Link();
      link.Href = href;
      builder.Invoke(link);

      entity.Links.Add(link);
      return entity;
    }

    /// <summary>
    /// Adiciona um link à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="href">A URL de referência do link.</param>
    /// <param name="title">O título do link.</param>
    /// <param name="rel">A relação entre o link e a entidade.</param>
    /// <param name="otherRels">Outras relações entre o link e a entidade.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddLink(this Entity entity, string href, string title, params string[] rels)
    {
      if (entity.Links == null)
      {
        entity.Links = new LinkCollection();
      }

      if (rels.Length == 0)
      {
        rels = new[] { RelNames.Link };
      }

      var link = new Link();
      link.Href = href;
      link.Rel = new NameCollection();
      link.Rel.AddMany(rels);
      link.Title = title;

      entity.Links.Add(link);
      return entity;
    }

    /// <summary>
    /// Adiciona um link à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="href">A URL de referência do link.</param>
    /// <param name="title">O título do link.</param>
    /// <param name="rel">A relação entre o link e a entidade.</param>
    /// <param name="otherRels">Outras relações entre o link e a entidade.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddLink(this Entity entity, string href, string title, Rel rel, params Rel[] otherRels)
    {
      if (entity.Links == null)
      {
        entity.Links = new LinkCollection();
      }

      var link = new Link();
      link.Href = href;
      link.Rel = new NameCollection();
      link.Rel.Add(rel.GetName());
      link.Rel.AddMany(otherRels.Select(RelExtensions.GetName));
      link.Title = title;

      entity.Links.Add(link);
      return entity;
    }

    /// <summary>
    /// Adiciona um link para a própria entidade.
    /// O link para a própria entidade, ou "self", refere-se à URL de acesso
    /// à própria entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="href">A URL de referência do link.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddLinkSelf(this Entity entity, string href, Action<Link> builder)
    {
      if (entity.Links == null)
      {
        entity.Links = new LinkCollection();
      }

      var link = new Link();
      link.Href = href;
      builder.Invoke(link);

      entity.Links.Insert(0, link);
      return entity;
    }

    /// <summary>
    /// Adiciona um link para a própria entidade.
    /// O link para a própria entidade, ou "self", refere-se à URL de acesso
    /// à própria entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="href">A URL de referência do link.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddLinkSelf(this Entity entity, string href, params string[] rels)
    {
      if (entity.Links == null)
      {
        entity.Links = new LinkCollection();
      }

      var link = new Link();
      link.Href = href;
      link.Rel = new NameCollection();
      link.Rel.Add(RelNames.Self);
      link.Rel.AddMany(rels);

      entity.Links.Insert(0, link);
      return entity;
    }

    /// <summary>
    /// Adiciona um link para a própria entidade.
    /// O link para a própria entidade, ou "self", refere-se à URL de acesso
    /// à própria entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="href">A URL de referência do link.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddLinkSelf(this Entity entity, string href, Rel rel, params Rel[] otherRels)
    {
      if (entity.Links == null)
      {
        entity.Links = new LinkCollection();
      }

      var link = new Link();
      link.Href = href;
      link.Rel = new NameCollection();
      link.Rel.Add(RelNames.Self);
      link.Rel.Add(rel.GetName());
      link.Rel.AddMany(otherRels.Select(RelExtensions.GetName));

      entity.Links.Insert(0, link);
      return entity;
    }

    #endregion
  }
}