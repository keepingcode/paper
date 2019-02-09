using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;
using Toolset.Reflection;

namespace Paper.Media.Design
{
  public static class RecordExtensions
  {
    /// <summary>
    /// Obtém a coleção de classes ou cria e retorna uma caso esteja nula.
    /// </summary>
    /// <param name="entity">A entidade alvo.</param>
    /// <returns>A coleção de nomes.</returns>
    private static EntityCollection GetEntities(this Entity entity)
    {
      return entity.Entities ?? (entity.Entities = new EntityCollection());
    }

    #region Record and Headers

    public static Entity SetRecordAndHeaders<T>(this Entity entity, T record, IEnumerable<string> headerRel = null, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      headerRel = ClassNames.Record.AsSingle().Concat(headerRel.NonNull());
      SetRecord<T>(entity, record, recordBuilder, select, except);
      HeaderExtensions.AddHeaders(entity, record.GetType(), headerRel, headerBuilder, select, except);
      return entity;
    }

    public static Entity SetRecordAndHeaders<T>(this Entity entity, T record, IEnumerable<Rel> headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return SetRecordAndHeaders<T>(entity, record, headerRel.Select(x => x.GetName()), recordBuilder, headerBuilder, select, except);
    }

    public static Entity SetRecordAndHeaders<T>(this Entity entity, T record, string headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return SetRecordAndHeaders<T>(entity, record, headerRel.AsSingle().NonNull(), recordBuilder, headerBuilder, select, except);
    }

    public static Entity SetRecordAndHeaders<T>(this Entity entity, T record, Rel headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return SetRecordAndHeaders<T>(entity, record, headerRel.GetName().AsSingle(), recordBuilder, headerBuilder, select, except);
    }

    public static Entity AddRecordAndHeaders<T>(this Entity entity, T record, IEnumerable<string> headerRel = null, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      headerRel = ClassNames.Record.AsSingle().Concat(headerRel.NonNull());
      AddRecord<T>(entity, record, recordBuilder, select, except);
      HeaderExtensions.AddHeaders(entity, record.GetType(), headerRel, headerBuilder, select, except);
      return entity;
    }

    public static Entity AddRecordAndHeaders<T>(this Entity entity, T record, IEnumerable<Rel> headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return SetRecordAndHeaders<T>(entity, record, headerRel.Select(x => x.GetName()), recordBuilder, headerBuilder, select, except);
    }

    public static Entity AddRecordAndHeaders<T>(this Entity entity, T record, string headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return AddRecordAndHeaders<T>(entity, record, headerRel.AsSingle().NonNull(), recordBuilder, headerBuilder, select, except);
    }

    public static Entity AddRecordAndHeaders<T>(this Entity entity, T record, Rel headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return AddRecordAndHeaders<T>(entity, record, headerRel.GetName().AsSingle(), recordBuilder, headerBuilder, select, except);
    }

    public static Entity AddRecordsAndHeaders<T>(this Entity entity, IEnumerable<T> records, IEnumerable<string> headerRel = null, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      var type = records.FirstOrDefault()?.GetType() ?? typeof(T);
      headerRel = ClassNames.Record.AsSingle().Concat(headerRel.NonNull());
      AddRecords<T>(entity, records, recordBuilder, select, except);
      HeaderExtensions.AddHeaders(entity, type, headerRel, headerBuilder, select, except);
      return entity;
    }

    public static Entity AddRecordsAndHeaders<T>(this Entity entity, IEnumerable<T> records, IEnumerable<Rel> headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return AddRecordsAndHeaders<T>(entity, records, headerRel.Select(x => x.GetName()), recordBuilder, headerBuilder, select, except);
    }

    public static Entity AddRecordsAndHeaders<T>(this Entity entity, IEnumerable<T> records, string headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return AddRecordsAndHeaders<T>(entity, records, headerRel.AsSingle().NonNull(), recordBuilder, headerBuilder, select, except);
    }

    public static Entity AddRecordsAndHeaders<T>(this Entity entity, IEnumerable<T> records, Rel headerRel, Action<T, Entity> recordBuilder = null, Action<HeaderBuilder> headerBuilder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      return AddRecordsAndHeaders<T>(entity, records, headerRel.GetName().AsSingle(), recordBuilder, headerBuilder, select, except);
    }

    #endregion

    #region Record

    /// <summary>
    /// Constrói uma estrutura de registro na entidade para representar o objeto indicado.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto origem.</typeparam>
    /// <param name="entity">A entidade destino.</param>
    /// <param name=ClassNames.Record>O objeto origem.</param>
    /// <param name="builder">Método de construção da entidade.</param>
    /// <returns>A própria entidade inspecionada</returns>
    public static Entity SetRecord<T>(this Entity entity, T record, Action<T, Entity> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      entity.AddClass(ClassNames.Record);
      entity.AddProperties(record, select: select, except: except);

      var properties = entity._Get<PropertyCollection>("Properties");
      if (properties != null)
      {
        entity.AddProperties(properties);
      }

      builder?.Invoke(record, entity);

      if (entity.Title == null)
      {
        entity.SetTitle(record._Get<string>("Title"));
      }

      if (!entity.HasUserClass())
      {
        entity.AddClass(record.GetType());
      }

      if (entity.Links?.Any(x => x.Rel.Has(Rel.Self)) != true)
      {
        var href = record._Get<Href>("Href");
        if (href != null)
        {
          entity.AddLinkSelf(href);
        }
      }

      return entity;
    }

    /// <summary>
    /// Acrescenta uma entidade filha à entidade indicada para representar os registros indicados.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto origem.</typeparam>
    /// <param name="entity">A entidade destino.</param>
    /// <param name=ClassNames.Record>O objeto origem.</param>
    /// <param name="builder">Método de construção da entidade.</param>
    /// <returns>A própria entidade inspecionada</returns>
    public static Entity AddRecord<T>(this Entity entity, T record, Action<T, Entity> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      var entities = entity.GetEntities();
      var child = new Entity();
      child.SetRecord(record, builder, select, except);
      entities.Add(child);
      return entity;
    }

    /// <summary>
    /// Acrescenta uma entidade filha à entidade indicada para representar os registros indicados.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto origem.</typeparam>
    /// <param name="entity">A entidade destino.</param>
    /// <param name="records">Os objetos origens.</param>
    /// <param name="builder">Método de construção da entidade.</param>
    /// <returns>A própria entidade inspecionada</returns>
    public static Entity AddRecords<T>(this Entity entity, IEnumerable<T> records, Action<T, Entity> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
      where T : class
    {
      var entities = entity.GetEntities();
      foreach (var record in records)
      {
        var child = new Entity();
        child.SetRecord(record, builder, select, except);
        entities.Add(child);
      }
      return entity;
    }

    #endregion
  }
}
