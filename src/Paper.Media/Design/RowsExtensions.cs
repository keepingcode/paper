using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media.Design
{
  /// <summary>
  /// Extensões de desenho de registros que acompanham a entidade.
  /// 
  /// -   Os registros são representados como entidades filhas.
  /// -   Entidades que carregam registros recebem a classe <see cref="ClassNames.Rows"/>.
  /// -   Para cada coluna de registro uma entidade filha correspondente é acrescentada.
  ///     -   A entidade filha contém a propriedade "Name" para vinculação do nome da coluna.
  ///     -   A entidade filha contém a classe <see cref="ClassNames.Header"/>.
  ///     -   A entidade filha contém a relação <see cref="RelNames.Row"/>.
  /// </summary>
  public static class RowsExtensions
  {
    public const string HeaderNamesProperty = "__RowHeaders";
    public const string PaginationProperty = "__RowsPage";

    #region GetRows

    public static ICollection<T> GetRows<T>(this Entity entity)
      where T : new()
    {
      var items = new List<T>();
      if (entity.Entities != null)
      {
        var records =
          from item in entity.Entities
          where item.Class.Has(ClassNames.Data)
          where item.Class.Has(typeof(T).FullName)
          select item;
        foreach (var record in records)
        {
          var item = record.GetData<T>();
          items.Add(item);
        }
      }
      return items;
    }

    #endregion

    #region ForEach...

    /// <summary>
    /// Itera pela coleção de registros adicionados à entidade.
    /// </summary>
    /// <param name="entity">A entidade inspecionada.</param>
    /// <param name="inspection">A função de inspeção do item.</param>
    /// <returns>A própria instãncia da entidade inspecionada.</returns>
    public static Entity ForEachRow(this Entity entity, Action<Entity> inspection)
    {
      if (entity.Entities == null)
        return entity;

      var rows =
        from child in entity.Entities
        where child.Class.Contains(ClassNames.Row)
           && child.Rel.Contains(RelNames.Row)
        select child;

      rows.ForEach(inspection);

      return entity;
    }

    /// <summary>
    /// Itera sob a coleção de cabeçalhos de registro na entidade.
    /// </summary>
    /// <param name="entity">A entidade inspecionada.</param>
    /// <param name="inspection">A função de inspeção do item.</param>
    /// <returns>A própria instância da entidade inspecionada.</returns>
    public static Entity ForEachRowHeader(this Entity entity, Action<Entity, HeaderInfo> inspection)
    {
      if (entity.Entities == null)
        return entity;

      var headers =
        from child in entity.Entities
        where child.Class?.Contains(ClassNames.Header) == true
           && child.Rel?.Contains(RelNames.Row) == true
        select child;

      headers.ForEach(child =>
      {
        var properties =
          child.Properties
          ?? (child.Properties = new PropertyCollection());
        inspection(child, new HeaderInfo(properties));
      });

      return entity;
    }

    #endregion

    #region AddRow e AddRows

    /// <summary>
    /// Adiciona a entidade como um registro da entidade principal.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="row">O registro adicionado.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRow(this Entity entity, Entity row)
    {
      return DoAddRows(entity, row.AsSingle(), null);
    }

    /// <summary>
    /// Adiciona o registro como entidade filha da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="builder">O construtor do registro.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRow(this Entity entity, Action<Entity> builder)
    {
      return DoAddRows(entity, new Entity().AsSingle(), builder);
    }

    /// <summary>
    /// Adiciona a entidade como um registro da entidade principal.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rows">Os registros adicionados.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRows(this Entity entity, params Entity[] rows)
    {
      return DoAddRows(entity, rows, null);
    }

    /// <summary>
    /// Adiciona a entidade como um registro da entidade principal.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rows">Os registros adicionados.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRows(this Entity entity, IEnumerable<Entity> rows)
    {
      return DoAddRows(entity, rows, null);
    }

    /// <summary>
    /// Adiciona o registro como entidade filha da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rowBuilders">Os construtores de registro.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRows(this Entity entity, params Action<Entity>[] rowBuilders)
    {
      var rowsFactory = rowBuilders.Select(builder =>
      {
        var child = new Entity();
        builder.Invoke(child);
        return child;
      });
      return DoAddRows(entity, rowsFactory, null);
    }

    /// <summary>
    /// Adiciona os registros à entidade.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rows">As fábricas de registros.</param>
    /// <param name="rowInspector">
    /// Um método de inspeção dos registros.
    /// O método pode ser usado para aplicar mudanças aos registros criados.
    /// </param>
    /// <returns>A própria entidade modificada.</returns>
    private static Entity DoAddRows(this Entity entity, IEnumerable<Entity> rows, Action<Entity> rowInspector)
    {
      if (entity.Entities == null)
      {
        entity.Entities = new EntityCollection();
      }
      if (entity.Properties == null)
      {
        entity.Properties = new PropertyCollection();
      }

      entity.AddClass(Class.Rows);

      foreach (var row in rows)
      {
        row.AddRel(Rel.Row);
        row.AddClass(Class.Row);

        rowInspector?.Invoke(row);

        entity.Entities.Add(row);
      }

      return entity;
    }

    #endregion

    #region AddRow e AddRows by Graph

    /// <summary>
    /// Adiciona o registro como entidade filha da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="graph">Os dados do registro adicionado à entidade.</param>
    /// <param name="builder">O construtor do registro.</param>
    /// <param name="select">Nome das propriedades consideradas.</param>
    /// <param name="except">Nome das propriedades excluídas.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRow<T>(this Entity entity, T graph, Action<Entity> builder, string[] select = null, string[] except = null)
    {
      DoAddGraphs(entity, graph.AsSingle(), (e, x) => builder.Invoke(e), select, except);
      return entity;
    }

    /// <summary>
    /// Adiciona o registro como entidade filha da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="graph">Os dados do registro adicionado à entidade.</param>
    /// <param name="builder">O construtor do registro.</param>
    /// <param name="select">Nome das propriedades consideradas.</param>
    /// <param name="except">Nome das propriedades excluídas.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRow<T>(this Entity entity, T graph, Action<Entity, T> builder = null, string[] select = null, string[] except = null)
    {
      DoAddGraphs(entity, graph.AsSingle(), builder, select, except);
      return entity;
    }

    /// <summary>
    /// Adiciona o registro como entidade filha da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="builder">
    /// O construtor dos registros.
    /// O método é invocado para cada registro criado.
    /// </param>
    /// <param name="rows">Os registros adicionados.</param>
    /// <param name="select">Nome das propriedades consideradas.</param>
    /// <param name="except">Nome das propriedades excluídas.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRows<T>(this Entity entity, IEnumerable<T> rows, Action<Entity> builder, string[] select = null, string[] except = null)
    {
      DoAddGraphs(entity, rows, (e, x) => builder.Invoke(e), select, except);
      return entity;
    }

    /// <summary>
    /// Adiciona o registro como entidade filha da entidade indicada.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="builder">
    /// O construtor dos registros.
    /// O método é invocado para cada registro criado.
    /// </param>
    /// <param name="rows">Os registros adicionados.</param>
    /// <param name="select">Nome das propriedades consideradas.</param>
    /// <param name="except">Nome das propriedades excluídas.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRows<T>(this Entity entity, IEnumerable<T> rows, Action<Entity, T> builder = null, string[] select = null, string[] except = null)
    {
      DoAddGraphs(entity, rows, builder, select, except);
      return entity;
    }

    private static void DoAddGraphs<T>(Entity entity, IEnumerable<T> rows, Action<Entity, T> builder, string[] select = null, string[] except = null)
    {
      if (entity.Entities == null)
      {
        entity.Entities = new EntityCollection();
      }
      if (entity.Properties == null)
      {
        entity.Properties = new PropertyCollection();
      }

      entity.AddClass(Class.Rows);

      if (entity.Properties["__rowHeaders"] == null)
      {
        entity.AddRowHeadersFrom<T>();
      }

      foreach (var row in rows)
      {
        var item = new Entity();
        item.AddData(row, select: select, except: except);
        item.AddRel(Rel.Rows);
        item.AddClass(Class.Row);
        entity.Entities.Add(item);

        builder?.Invoke(item, row);
      }
    }

    #endregion

    #region AddRowHeader 

    /// <summary>
    /// Adiciona informações sobre campos.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="headers">Os dados adicionados à entidade.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRowHeaders(this Entity entity, IEnumerable<HeaderInfo> headers)
    {
      if (headers != null)
      {
        foreach (var header in headers)
        {
          HeaderUtil.AddHeaderToEntity(
              entity
            , HeaderNamesProperty
            , header.Name
            , header.Title
            , header.DataType
            , RelNames.Row
            , options => header.CopyToHeaderOptions(options)
          );
        }
      }
      return entity;
    }

    /// <summary>
    /// Adiciona informações sobre um campo.
    /// </summary>
    /// <typeparam name="T">Um tipo para inferência dos campos.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRowHeadersFrom<T>(this Entity entity, Action<HeaderBuilder> builder = null)
    {
      var properties = Property.UnwrapPropertyInfo(typeof(T));
      foreach (var property in properties)
      {
        HeaderUtil.AddHeaderToEntity(
            entity
          , HeaderNamesProperty
          , property.Name
          , property.Title
          , DataTypeNames.GetDataTypeName(property.Type)
          , RelNames.Row
          , builder
        );
      }
      return entity;
    }

    /// <summary>
    /// Adiciona informações sobre um campo.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="typeOrInstance">Um tipo ou instância para inferência dos campos.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRowHeadersFrom(this Entity entity, object typeOrInstance, Action<HeaderBuilder> builder = null)
    {
      var properties = Property.UnwrapPropertyInfo(typeOrInstance);
      foreach (var property in properties)
      {
        HeaderUtil.AddHeaderToEntity(
            entity
          , HeaderNamesProperty
          , property.Name
          , property.Title
          , DataTypeNames.GetDataTypeName(property.Type)
          , RelNames.Row
          , builder
        );
      }
      return entity;
    }

    /// <summary>
    /// Adiciona informações sobre um campo.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="name">Os dados adicionados à entidade.</param>
    /// <param name="builder">Construtor do cabeçalho.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRowHeader(this Entity entity, string name, Action<HeaderBuilder> builder = null)
    {
      HeaderUtil.AddHeaderToEntity(
          entity
        , HeaderNamesProperty
        , name
        , null
        , null
        , RelNames.Row
        , builder
      );
      return entity;
    }

    /// <summary>
    /// Adiciona informações sobre um campo.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="header">Informações cobre o campo.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRowHeader(this Entity entity, HeaderInfo header)
    {
      HeaderUtil.AddHeaderToEntity(
          entity
        , HeaderNamesProperty
        , header.Name
        , header.Title
        , header.DataType
        , RelNames.Row
        , options => header.CopyToHeaderOptions(options)
      );
      return entity;
    }

    /// <summary>
    /// Adiciona informações sobre um campo.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="name">Os dados adicionados à entidade.</param>
    /// <param name="title">Título do campo.</param>
    /// <param name="dataType">Tipo do dado do campo.</param>
    /// <param name="hidden">Marcao ou desmarca o campo como oculto.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRowHeader(this Entity entity, string name, string title = null, string dataType = null, bool hidden = false)
    {
      HeaderUtil.AddHeaderToEntity(
          entity
        , HeaderNamesProperty
        , name
        , title
        , dataType
        , RelNames.Row
        , opt => opt.AddHidden(hidden)
      );
      return entity;
    }

    /// <summary>
    /// Adiciona informações sobre um campo oculto.
    /// </summary>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="name">Os dados adicionados à entidade.</param>
    /// <param name="title">Título do campo.</param>
    /// <param name="dataType">Tipo do dado do campo.</param>
    /// <returns>A própria entidade modificada.</returns>
    public static Entity AddRowHeaderHidden(this Entity entity, string name, string title = null, string dataType = null)
    {
      HeaderUtil.AddHeaderToEntity(
          entity
        , HeaderNamesProperty
        , name
        , title
        , dataType
        , RelNames.Row
        , opt => opt.AddHidden()
      );
      return entity;
    }
    
    #endregion

    #region Pagination

    public static Entity AddRowsPage(this Entity entity, int? limit, int? offset)
    {
      return AddRowsPage(entity, Page.CreateOffset(limit, offset));
    }

    public static Entity AddRowsPage(this Entity entity, Page pagination)
    {
      var exists = (entity.Properties?[PaginationProperty]?.Value != null);
      if (exists)
      {
        entity.Properties.Remove(PaginationProperty);
      }

      if (pagination.IsLimitSet)
      {
        entity.AddProperty($"{PaginationProperty}.Limit", pagination.Limit);
      }
      else
      {
        entity.AddProperty($"{PaginationProperty}.PageSize", pagination.Size);
      }

      if (pagination.IsOffsetSet)
      {
        entity.AddProperty($"{PaginationProperty}.Offset", pagination.Offset);
      }
      else
      {
        entity.AddProperty($"{PaginationProperty}.Page", pagination.Number);
      }
      return entity;
    }

    public static Entity AddLinkNextRows(this Entity entity, string href, int? limit, int? offset)
    {
      return AddLinkNextRows(entity, href, Page.CreateOffset(limit, offset));
    }

    public static Entity AddLinkNextRows(this Entity entity, string href, Page pagination)
    {
      href = new Route(href).SetArg(pagination.ToString());
      entity.AddLink(href, "Próximo", Rel.Next);
      return entity;
    }

    public static Entity AddLinkPreviousRows(this Entity entity, string href, int? limit, int? offset)
    {
      return AddLinkPreviousRows(entity, href, Page.CreateOffset(limit, offset));
    }

    public static Entity AddLinkPreviousRows(this Entity entity, string href, Page pagination)
    {
      href = new Route(href).SetArg(pagination.ToString());
      entity.AddLink(href, "Anterior", Rel.Previous);
      return entity;
    }

    public static Entity AddLinkFirstRows(this Entity entity, string href, int? limit, int? offset)
    {
      return AddLinkFirstRows(entity, href, Page.CreateOffset(limit, offset));
    }

    public static Entity AddLinkFirstRows(this Entity entity, string href, Page pagination)
    {
      href = new Route(href).SetArg(pagination.ToString());
      entity.AddLink(href, "Primeiro", Rel.First);
      return entity;
    }

    #endregion
  }
}
