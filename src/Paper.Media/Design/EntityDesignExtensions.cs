﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;
using Toolset.Reflection;

namespace Paper.Media.Design
{
  public static class EntityDesignExtensions
  {
    #region With*

    /// <summary>
    /// Obtém a propriedade da entidade instanciando a coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <returns>
    /// A instância da entidade.
    /// É garantido que a proprieadade está instanciada.
    /// </returns>
    public static NameCollection WithClass<TEntity>(this TEntity entity)
      where TEntity : IMediaObject
    {
      return entity.Class ?? (entity.Class = new NameCollection());
    }

    /// <summary>
    /// Obtém a propriedade da entidade instanciando a coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <returns>
    /// A instância da entidade.
    /// É garantido que a proprieadade está instanciada.
    /// </returns>
    public static NameCollection WithRel<TEntity>(this TEntity entity)
      where TEntity : IMediaObject
    {
      return entity.Rel ?? (entity.Rel = new NameCollection());
    }

    /// <summary>
    /// Obtém a propriedade da entidade instanciando a coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <returns>
    /// A instância da entidade.
    /// É garantido que a proprieadade está instanciada.
    /// </returns>
    public static PropertyMap WithProperties<TEntity>(this TEntity entity)
      where TEntity : IPropertyMap
    {
      return entity.Properties ?? (entity.Properties = new PropertyMap());
    }

    /// <summary>
    /// Obtém a propriedade da entidade instanciando a coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="keyPath">O caminho do mapa de proprieades com partes separadas por ponto.</param>
    /// <returns>
    /// A instância da entidade.
    /// É garantido que a proprieadade está instanciada.
    /// </returns>
    public static PropertyMap WithProperties<TEntity>(this TEntity entity, string keyPath)
      where TEntity : IPropertyMap
    {
      return WithProperties(entity, keyPath.Split('.'));
    }

    /// <summary>
    /// Obtém a propriedade da entidade instanciando a coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <param name="keyPath">O caminho do mapa de proprieades.</param>
    /// <returns>
    /// A instância da entidade.
    /// É garantido que a proprieadade está instanciada.
    /// </returns>
    public static PropertyMap WithProperties<TEntity>(this TEntity entity, IEnumerable<string> keyPath)
      where TEntity : IPropertyMap
    {
      var map = entity.WithProperties();
      foreach (var token in keyPath)
      {
        var current = map[token];
        if (current is PropertyMap currentMap)
        {
          map = currentMap;
          continue;
        }
        if (current == null)
        {
          map = (PropertyMap)(map[token] = new PropertyMap());
          continue;
        }
        throw new MediaException("A propriedade não é um mapa: " + token);
      }
      return map;
    }

    /// <summary>
    /// Obtém a propriedade da entidade instanciando a coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <returns>
    /// A instância da entidade.
    /// É garantido que a proprieadade está instanciada.
    /// </returns>
    public static EntityCollection WithEntities(this Entity entity)
    {
      return entity.Entities ?? (entity.Entities = new EntityCollection());
    }

    /// <summary>
    /// Obtém a propriedade da entidade instanciando a coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <returns>
    /// A instância da entidade.
    /// É garantido que a proprieadade está instanciada.
    /// </returns>
    public static EntityActionCollection WithActions(this Entity entity)
    {
      return entity.Actions ?? (entity.Actions = new EntityActionCollection());
    }

    /// <summary>
    /// Obtém a propriedade da entidade instanciando a coleção caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <returns>
    /// A instância da entidade.
    /// É garantido que a proprieadade está instanciada.
    /// </returns>
    public static LinkCollection WithLinks(this Entity entity)
    {
      return entity.Links ?? (entity.Links = new LinkCollection());
    }

    #endregion

    #region Title

    public static TEntity SetTitle<TEntity>(this TEntity entity, string title)
      where TEntity : IMediaObject
    {
      entity.Title = title;
      return entity;
    }

    #endregion

    #region Class

    /// <summary>
    /// Adiciona classes à entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddClass<TEntity>(this TEntity entity, params string[] classes)
      where TEntity : IMediaObject
    {
      entity.WithClass().AddMany(classes.NonNull());
      return entity;
    }

    /// <summary>
    /// Adiciona classes à entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddClass<TEntity>(this TEntity entity, IEnumerable<string> classes)
      where TEntity : IMediaObject
    {
      entity.WithClass().AddMany(classes.NonNull());
      return entity;
    }

    /// <summary>
    /// Adiciona classes à entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddClass<TEntity>(this TEntity entity, params Class[] classes)
      where TEntity : IMediaObject
    {
      entity.WithClass().AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Adiciona classes à entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddClass<TEntity>(this TEntity entity, IEnumerable<Class> classes)
      where TEntity : IMediaObject
    {
      entity.WithClass().AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Adiciona classes à entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddClass<TEntity>(this TEntity entity, params Type[] classes)
      where TEntity : IMediaObject
    {
      entity.WithClass().AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Adiciona classes à entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes adicionadas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddClass<TEntity>(this TEntity entity, IEnumerable<Type> classes)
      where TEntity : IMediaObject
    {
      entity.WithClass().AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetClass<TEntity>(this TEntity entity, params string[] classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.AddMany(classes.NonNull());
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetClass<TEntity>(this TEntity entity, IEnumerable<string> classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.AddMany(classes.NonNull());
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetClass<TEntity>(this TEntity entity, params Class[] classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetClass<TEntity>(this TEntity entity, IEnumerable<Class> classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetClass<TEntity>(this TEntity entity, params Type[] classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetClass<TEntity>(this TEntity entity, IEnumerable<Type> classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Redefine as classes de metadado da entidade.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetMetaClass<TEntity>(this TEntity entity, params string[] classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsMetaClass);
      @class.AddMany(classes.NonNull());
      return entity;
    }

    /// <summary>
    /// Redefine as classes de metadado da entidade.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetMetaClass<TEntity>(this TEntity entity, IEnumerable<string> classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsMetaClass);
      @class.AddMany(classes.NonNull());
      return entity;
    }

    /// <summary>
    /// Redefine as classes de metadado da entidade.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetMetaClass<TEntity>(this TEntity entity, params Class[] classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsMetaClass);
      @class.AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Redefine as classes de metadado da entidade.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetMetaClass<TEntity>(this TEntity entity, IEnumerable<Class> classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsMetaClass);
      @class.AddMany(classes.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Redefine as classes de usuário da entidade.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetUserClass<TEntity>(this TEntity entity, params string[] classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsUserClass);
      @class.AddMany(classes.NonNull());
      return entity;
    }

    /// <summary>
    /// Redefine as classes de usuário da entidade.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetUserClass<TEntity>(this TEntity entity, IEnumerable<string> classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsUserClass);
      @class.AddMany(classes.NonNull());
      return entity;
    }

    /// <summary>
    /// Redefine as classes de usuário da entidade.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetUserClass<TEntity>(this TEntity entity, params Type[] classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsUserClass);
      @class.AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    /// <summary>
    /// Redefine as classes de usuário da entidade.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade.</typeparam>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <param name="classes">As classes redefinidas.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetUserClass<TEntity>(this TEntity entity, IEnumerable<Type> classes)
      where TEntity : IMediaObject
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsUserClass);
      @class.AddMany(classes.Select(Conventions.MakeName));
      return entity;
    }

    #region Special Cases

    /// <summary>
    /// Adiciona uma classe à entidade.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Entity AddClass<TClass>(this Entity entity)
    {
      entity.WithClass().Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Adiciona uma classe à entidade.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Field AddClass<TClass>(this Field entity)
    {
      entity.WithClass().Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Adiciona uma classe à entidade.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Link AddClass<TClass>(this Link entity)
    {
      entity.WithClass().Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Entity SetClass<TClass>(this Entity entity)
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Field SetClass<TClass>(this Field entity)
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Redefine as classes da entidade.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Link SetClass<TClass>(this Link entity)
    {
      var @class = entity.WithClass();
      @class.Clear();
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Redefine as classes de usuário da entidade.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Entity SetUserClass<TClass>(this Entity entity)
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsUserClass);
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Redefine as classes de usuário da entidade.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Field SetUserClass<TClass>(this Field entity)
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsUserClass);
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    /// <summary>
    /// Redefine as classes de usuário da entidade.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <param name="entity">A instância da entidade modificada.</param>
    /// <typeparam name="TClass">O tipo da classe.</typeparam>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static Link SetUserClass<TClass>(this Link entity)
    {
      var @class = entity.WithClass();
      @class.RemoveWhen(ClassNames.IsUserClass);
      @class.Add(Conventions.MakeName(typeof(TClass)));
      return entity;
    }

    #endregion

    #endregion

    #region Rel

    /// <summary>
    /// Adiciona relacionamentos à classe.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos adicionados.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddRel<TEntity>(this TEntity entity, params string[] rels)
      where TEntity : IMediaObject
    {
      entity.WithRel().AddMany(rels.NonNull());
      return entity;
    }

    /// <summary>
    /// Adiciona relacionamentos à classe.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos adicionados.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddRel<TEntity>(this TEntity entity, IEnumerable<string> rels)
      where TEntity : IMediaObject
    {
      entity.WithRel().AddMany(rels.NonNull());
      return entity;
    }

    /// <summary>
    /// Adiciona relacionamentos à classe.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos adicionados.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddRel<TEntity>(this TEntity entity, params Rel[] rels)
      where TEntity : IMediaObject
    {
      entity.WithRel().AddMany(rels.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Adiciona relacionamentos à classe.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos adicionados.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity AddRel<TEntity>(this TEntity entity, IEnumerable<Rel> rels)
      where TEntity : IMediaObject
    {
      entity.WithRel().AddMany(rels.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Redefine os relacionamentos da classe.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos definidos.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetRel<TEntity>(this TEntity entity, params string[] rels)
      where TEntity : IMediaObject
    {
      var rel = entity.WithRel();
      rel.Clear();
      rel.AddMany(rels.NonNull());
      return entity;
    }

    /// <summary>
    /// Redefine os relacionamentos da classe.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos definidos.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetRel<TEntity>(this TEntity entity, IEnumerable<string> rels)
      where TEntity : IMediaObject
    {
      var rel = entity.WithRel();
      rel.Clear();
      rel.AddMany(rels.NonNull());
      return entity;
    }

    /// <summary>
    /// Redefine os relacionamentos da classe.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos definidos.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetRel<TEntity>(this TEntity entity, params Rel[] rels)
      where TEntity : IMediaObject
    {
      var rel = entity.WithRel();
      rel.Clear();
      rel.AddMany(rels.Select(x => x.GetName()));
      return entity;
    }

    /// <summary>
    /// Redefine os relacionamentos da classe.
    /// </summary>
    /// <typeparam name="TEntity">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos definidos.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static TEntity SetRel<TEntity>(this TEntity entity, IEnumerable<Rel> rels)
      where TEntity : IMediaObject
    {
      var rel = entity.WithRel();
      rel.Clear();
      rel.AddMany(rels.Select(x => x.GetName()));
      return entity;
    }

    #endregion

    #region property

    public static TEntity SetProperty<TEntity>(this TEntity entity, string key, object value)
      where TEntity : IPropertyMap
    {
      return SetProperty(entity, key.Split('.'), value);
    }

    public static TEntity SetProperty<TEntity>(this TEntity entity, IEnumerable<string> keyPath, object value)
      where TEntity : IPropertyMap
    {
      var mapKey = keyPath.SkipLast(1);
      var key = keyPath.Last();
      var map = entity.WithProperties(mapKey);
      map[key] = value;
      return entity;
    }

    #endregion

    #region properties

    public static TEntity AddProperties<TEntity>(this TEntity entity, object graph, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where TEntity : IPropertyMap
    {
      var map = entity.WithProperties();
      var properties = (PropertyMap)PropertyMap.CreateCompatibleValue(graph, select, except);
      map.AddMany(properties);
      return entity;
    }

    #endregion
  }
}