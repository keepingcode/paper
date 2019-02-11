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
  /// Extensões de desenho de objetos Entity.
  /// </summary>
  public static class EntityExtensions
  {
    #region Title

    /// <summary>
    /// Define o título da entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="title">O novo título da entidade.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia SetTitle<TMedia>(this TMedia entity, string title)
      where TMedia : IMediaObject
    {
      entity.Title = title;
      return entity;
    }

    /// <summary>
    /// Constrói um título para a entidade a partir do tipo indicado.
    /// O título é lido do atributo de classe [DisplayName], caso não exista,
    /// o título é construído a partir do próprio nome do tipo.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="baseType">
    /// O tipo que será usado como base para definição do título.
    /// </param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia SetTitle<TMedia>(this TMedia entity, Type baseType)
      where TMedia : IMediaObject
    {
      var attribute =
        baseType
          .GetCustomAttributes(true)
          .OfType<DisplayNameAttribute>()
          .FirstOrDefault();

      entity.Title =
        attribute?.DisplayName
        ?? baseType.Name.ChangeCase(TextCase.ProperCase);

      return entity;
    }

    /// <summary>
    /// Constrói um título para a entidade a partir do tipo indicado.
    /// O título é lido do atributo de classe [DisplayName], caso não exista,
    /// o título é construído a partir do próprio nome do tipo.
    /// </summary>
    /// <typeparam name="T">
    /// O tipo que será usado como base para definição do título.
    /// </typeparam>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity SetTitle<T>(this Entity entity)
    {
      return SetTitle(entity, typeof(T));
    }

    /// <summary>
    /// Constrói um título para a entidade a partir do tipo indicado.
    /// O título é lido do atributo de classe [DisplayName], caso não exista,
    /// o título é construído a partir do próprio nome do tipo.
    /// </summary>
    /// <typeparam name="T">
    /// O tipo que será usado como base para definição do título.
    /// </typeparam>
    /// <param name="link">A entidade a ser modificada.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Link SetTitle<T>(this Link link)
    {
      return SetTitle(link, typeof(T));
    }

    /// <summary>
    /// Constrói um título para a entidade a partir do tipo indicado.
    /// O título é lido do atributo de classe [DisplayName], caso não exista,
    /// o título é construído a partir do próprio nome do tipo.
    /// </summary>
    /// <typeparam name="T">
    /// O tipo que será usado como base para definição do título.
    /// </typeparam>
    /// <param name="field">A entidade a ser modificada.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Field SetTitle<T>(this Field field)
    {
      return SetTitle(field, typeof(T));
    }

    #endregion

    #region Class

    /// <summary>
    /// Obtém a coleção de classes ou cria e retorna uma caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <returns>A coleção de nomes.</returns>
    private static NameCollection GetClass<TMedia>(this TMedia entity)
      where TMedia : IMediaObject
    {
      return entity.Class ?? (entity.Class = new NameCollection());
    }
    
    /// <summary>
    /// Adiciona classes à coleção de classes da entidade.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia AddClass<TMedia>(this TMedia entity, params Class[] classes)
      where TMedia : IMediaObject
    {
      entity.GetClass().AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Adiciona classes à coleção de classes da entidade.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia AddClass<TMedia>(this TMedia entity, IEnumerable<Class> classes)
      where TMedia : IMediaObject
    {
      entity.GetClass().AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Adiciona classes à coleção de classes da entidade.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia AddClass<TMedia>(this TMedia entity, params string[] classes)
      where TMedia : IMediaObject
    {
      entity.GetClass().AddMany(classes);
      return entity;
    }

    /// <summary>
    /// Adiciona classes à coleção de classes da entidade.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia AddClass<TMedia>(this TMedia entity, IEnumerable<string> classes)
      where TMedia : IMediaObject
    {
      entity.GetClass().AddMany(classes);
      return entity;
    }

    /// <summary>
    /// Adiciona classes à coleção de classes da entidade.
    /// Os nomes das classes são inferidos dos tipos indicados.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia AddClass<TMedia>(this TMedia entity, params Type[] classes)
      where TMedia : IMediaObject
    {
      entity.GetClass().AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Adiciona classes à coleção de classes da entidade.
    /// Os nomes das classes são inferidos dos tipos indicados.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia AddClass<TMedia>(this TMedia entity, IEnumerable<Type> classes)
      where TMedia : IMediaObject
    {
      entity.GetClass().AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Define as classes da entidade.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia SetClass<TMedia>(this TMedia entity, params Class[] classes)
      where TMedia : IMediaObject
    {
      var @class = entity.GetClass();
      @class.Clear();
      @class.AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Define as classes da entidade.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia SetClass<TMedia>(this TMedia entity, IEnumerable<Class> classes)
      where TMedia : IMediaObject
    {
      var @class = entity.GetClass();
      @class.Clear();
      @class.AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Define as classes da entidade.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia SetClass<TMedia>(this TMedia entity, params string[] classes)
      where TMedia : IMediaObject
    {
      var @class = entity.GetClass();
      @class.Clear();
      @class.AddMany(classes);
      return entity;
    }

    /// <summary>
    /// Define as classes da entidade.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia SetClass<TMedia>(this TMedia entity, IEnumerable<string> classes)
      where TMedia : IMediaObject
    {
      var @class = entity.GetClass();
      @class.Clear();
      @class.AddMany(classes);
      return entity;
    }

    /// <summary>
    /// Define as classes da entidade.
    /// Os nomes das classes são inferidos dos tipos indicados.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia SetClass<TMedia>(this TMedia entity, params Type[] classes)
      where TMedia : IMediaObject
    {
      var @class = entity.GetClass();
      @class.Clear();
      @class.AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Define as classes da entidade.
    /// Os nomes das classes são inferidos dos tipos indicados.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia SetClass<TMedia>(this TMedia entity, IEnumerable<Type> classes)
      where TMedia : IMediaObject
    {
      var @class = entity.GetClass();
      @class.Clear();
      @class.AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Adiciona a classe à coleção de classes da entidade.
    /// O nome da classe é inferido do tipo indicado.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <typeparam name="TClass">O tipo da classe adicionada.</typeparam>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia AddClass<TMedia, TClass>(this TMedia entity)
      where TMedia : IMediaObject
    {
      entity.GetClass().Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Adiciona a classe à coleção de classes da entidade.
    /// O nome da classe é inferido do tipo indicado.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <typeparam name="TClass">O tipo da classe adicionada.</typeparam>
    /// <returns>A própria instância da entidade.</returns>
    public static Entity AddClass<TClass>(this Entity entity)
    {
      entity.GetClass().Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Adiciona a classe à coleção de classes da entidade.
    /// O nome da classe é inferido do tipo indicado.
    /// </summary>
    /// <param name="link">A entidade alvo.</param>
    /// <typeparam name="TClass">O tipo da classe adicionada.</typeparam>
    /// <returns>A própria instância da entidade.</returns>
    public static Link AddClass<TClass>(this Link link)
    {
      link.GetClass().Add(Conventions.MakeName(typeof(TClass)));
      return link;
    }

    /// <summary>
    /// Adiciona a classe à coleção de classes da entidade.
    /// O nome da classe é inferido do tipo indicado.
    /// </summary>
    /// <param name="field">A entidade alvo.</param>
    /// <typeparam name="TClass">O tipo da classe adicionada.</typeparam>
    /// <returns>A própria instância da entidade.</returns>
    public static Field AddClass<TClass>(this Field field)
    {
      field.GetClass().Add(Conventions.MakeName(typeof(TClass)));
      return field;
    }

    /// <summary>
    /// Define a classe da entidade.
    /// O nome da classe é inferido do tipo indicado.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <typeparam name="TClass">O tipo da classe adicionada.</typeparam>
    /// <returns>A própria instância da entidade.</returns>
    public static TMedia SetClass<TMedia, TClass>(this TMedia entity)
      where TMedia : IMediaObject
    {
      var @class = entity.GetClass();
      @class.Clear();
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Define a classe da entidade.
    /// O nome da classe é inferido do tipo indicado.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <typeparam name="TClass">O tipo da classe adicionada.</typeparam>
    /// <returns>A própria instância da entidade.</returns>
    public static Entity SetClass<TClass>(this Entity entity)
    {
      var @class = entity.GetClass();
      @class.Clear();
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Define a classe da entidade.
    /// O nome da classe é inferido do tipo indicado.
    /// </summary>
    /// <param name="link">A entidade alvo.</param>
    /// <typeparam name="TClass">O tipo da classe adicionada.</typeparam>
    /// <returns>A própria instância da entidade.</returns>
    public static Link SetClass<TClass>(this Link link)
    {
      var @class = link.GetClass();
      @class.Clear();
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return link;
    }

    /// <summary>
    /// Define a classe da entidade.
    /// O nome da classe é inferido do tipo indicado.
    /// </summary>
    /// <param name="field">A entidade alvo.</param>
    /// <typeparam name="TClass">O tipo da classe adicionada.</typeparam>
    /// <returns>A própria instância da entidade.</returns>
    public static Field SetClass<TClass>(this Field field)
    {
      var @class = field.GetClass();
      @class.Clear();
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return field;
    }

    #endregion

    #region Rel

    /// <summary>
    /// Obtém a coleção de nomes ou define uma nova coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade destino.</param>
    /// <returns>A coleção de nomes.</returns>
    private static NameCollection GetRel<TMedia>(this TMedia entity)
      where TMedia : IMediaObject
    {
      return entity.Rel ?? (entity.Rel = new NameCollection());
    }

    /// <summary>
    /// Adiciona as relações indicadas à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rels">Nomes das relações.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia AddRel<TMedia>(this TMedia entity, params Rel[] rels)
      where TMedia : IMediaObject
    {
      entity.GetRel().AddMany(rels.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Adiciona as relações indicadas à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rels">Nomes das relações.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia AddRel<TMedia>(this TMedia entity, IEnumerable<Rel> rels)
      where TMedia : IMediaObject
    {
      entity.GetRel().AddMany(rels.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Adiciona as relações indicadas à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rels">Nomes das relações.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia AddRel<TMedia>(this TMedia entity, params string[] rels)
      where TMedia : IMediaObject
    {
      entity.GetRel().AddMany(rels);
      return entity;
    }

    /// <summary>
    /// Adiciona as relações indicadas à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rels">Nomes das relações.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia AddRel<TMedia>(this TMedia entity, IEnumerable<string> rels)
      where TMedia : IMediaObject
    {
      entity.GetRel().AddMany(rels);
      return entity;
    }

    /// <summary>
    /// Refine as relações da entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rels">Nomes das relações.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia SetRel<TMedia>(this TMedia entity, params Rel[] rels)
      where TMedia : IMediaObject
    {
      var rel = entity.GetRel();
      rel.Clear();
      rel.AddMany(rels.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Refine as relações da entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rels">Nomes das relações.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia SetRel<TMedia>(this TMedia entity, IEnumerable<Rel> rels)
      where TMedia : IMediaObject
    {
      var rel = entity.GetRel();
      rel.Clear();
      rel.AddMany(rels.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Refine as relações da entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rels">Nomes das relações.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia SetRel<TMedia>(this TMedia entity, params string[] rels)
      where TMedia : IMediaObject
    {
      var rel = entity.GetRel();
      rel.Clear();
      rel.AddMany(rels);
      return entity;
    }

    /// <summary>
    /// Refine as relações da entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rels">Nomes das relações.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static TMedia SetRel<TMedia>(this TMedia entity, IEnumerable<string> rels)
      where TMedia : IMediaObject
    {
      var rel = entity.GetRel();
      rel.Clear();
      rel.AddMany(rels);
      return entity;
    }

    #endregion

    #region Class

    /// <summary>
    /// Adiciona uma entidade filha à coleção de entidades filhas da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="child">A entidade a ser adicionada à coleção de entidades filhas.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddEntity(this Entity entity, Entity child)
    {
      if (entity.Entities == null)
      {
        entity.Entities = new EntityCollection();
      }
      entity.Entities.Add(child);
      return entity;
    }

    /// <summary>
    /// Adiciona uma entidade filha à coleção de entidades filhas da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rel">O relacionamento entre a entidade filha e a entidade principal.</param>
    /// <param name="builder">Um método de construção da entidade filha.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddEntity(this Entity entity, Rel rel, Action<Entity> builder)
    {
      var child = new Entity();
      child.Rel = new NameCollection();
      child.Rel.Add(rel.GetName());
      builder.Invoke(child);

      if (entity.Entities == null)
      {
        entity.Entities = new EntityCollection();
      }
      entity.Entities.Add(child);

      return entity;
    }

    /// <summary>
    /// Adiciona uma entidade filha à coleção de entidades filhas da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="rel">O relacionamento entre a entidade filha e a entidade principal.</param>
    /// <param name="builder">Um método de construção da entidade filha.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddEntity(this Entity entity, string rel, Action<Entity> builder)
    {
      var child = new Entity();
      child.Rel = new NameCollection();
      child.Rel.Add(rel);
      builder.Invoke(child);

      if (entity.Entities == null)
      {
        entity.Entities = new EntityCollection();
      }
      entity.Entities.Add(child);

      return entity;
    }

    #endregion

    #region Operações especiais

    /// <summary>
    /// Resolve os links relativos segundo o padrão de URLs do Paper:
    /// -   /Path, corresponde a um caminho relativo à API
    ///     Como em:
    ///         http://localhost/Api/1/Path
    /// -   ^/Path, corresponde a um caminho relativo à raiz da URI
    ///     Como em:
    ///         http://localhost/Path
    /// -   ./Path ou ../Path, corresponde a um caminho relativo à URI atual
    ///     Como em:
    ///         http://localhost/Api/1/Meu/Site/Path
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="requestUri">A URI de requisição da entidade.</param>
    /// <param name="apiPath">
    /// O caminho considerado como caminho da API.
    /// Geralmente "/Api/VERSAO", sendo versão o número de versão de API do Paper.
    /// Por padrão: "/Api/1"
    /// </param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity ResolveLinks(this Entity entity, string requestUri, string apiPath = "/Api/1")
    {
      var route = new Route(requestUri).UnsetAllArgsExcept("f", "in", "out");

      var entities = DescendantsAndSelf(entity);
      var links = entities.Select(x => x.Links).NonNull().SelectMany();
      var actions = entities.Select(x => x.Actions).NonNull().SelectMany();

      foreach (var link in links)
      {
        link.Href = ResolveLink(link.Href, route, apiPath);
      }

      foreach (var action in actions)
      {
        action.Href = ResolveLink(action.Href, route, apiPath);
      }

      return entity;
    }

    /// <summary>
    /// Resolve os links relativos segundo o padrão de URLs do Paper:
    /// -   /Path, corresponde a um caminho relativo à API
    ///     Como em:
    ///         http://localhost/Api/1/Path
    /// -   ^/Path, corresponde a um caminho relativo à raiz da URI
    ///     Como em:
    ///         http://localhost/Path
    /// -   ./Path ou ../Path, corresponde a um caminho relativo à URI atual
    ///     Como em:
    ///         http://localhost/Api/1/Meu/Site/Path
    /// </summary>
    /// <param name="href">A URI a ser resolvida.</param>
    /// <param name="currentUri">A URI representando a rota corrente.</param>
    /// <param name="apiPath">
    /// O caminho considerado como caminho da API.
    /// Geralmente "/Api/VERSAO", sendo versão o número de versão de API do Paper.
    /// Por padrão: "/Api/1"
    /// </param>
    /// <returns>A URI resolvida.</returns>
    private static string ResolveLink(string href, Route currentUri, string apiPath)
    {
      if (href.StartsWith("^/"))
      {
        href = currentUri.Combine(href.Substring(1));
      }
      else if (href.StartsWith("/"))
      {
        href = currentUri.Combine(apiPath).Append(href);
      }
      else if (href == ""
            || href.StartsWith(".")
            || href.StartsWith("?"))
      {
        href = currentUri.Combine(href);
      }
      return href;
    }

    /// <summary>
    /// Enumera recursivamente todos os filhos da entidade.
    /// </summary>
    /// <param name="entity">A entidade analisada.</param>
    /// <returns>Todos os filhos da entidade recursivamente.</returns>
    public static IEnumerable<Entity> Descendants(this Entity entity)
    {
      return
        entity?.Entities?.SelectMany(e => Descendants(e).Append(e))
        ?? Enumerable.Empty<Entity>();
    }

    /// <summary>
    /// Enumera recursivamente todos os filhos da entidade e a própria entidade.
    /// </summary>
    /// <param name="entity">A entidade analisada.</param>
    /// <returns>Todos os filhos da entidade recursivamente e a própria entidade.</returns>
    public static IEnumerable<Entity> DescendantsAndSelf(this Entity entity)
    {
      return
        entity?.Entities?.SelectMany(e => DescendantsAndSelf(e)).Append(entity)
        ?? entity.AsSingle();
    }

    #endregion

  }
}
