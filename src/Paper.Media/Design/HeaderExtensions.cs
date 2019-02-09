using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media.Design
{
  public static class HeaderExtensions
  {
    #region ForEach...

    public static Entity ForEachHeader(this Entity entity, IEnumerable<string> rel, Action<Entity, HeaderInfo> inspection)
    {
      if (entity.Entities != null)
      {
        var headers =
          from child in entity.Entities
          where child.Class?.Contains(ClassNames.Header) == true
          where !rel.Any() || rel.All(x => child.Class?.Contains(x) == true)
          select child;

        foreach (var header in headers)
        {
          var properties = header.Properties ?? (header.Properties = new PropertyCollection());
          inspection(header, new HeaderInfo(properties));
        }
      }
      return entity;
    }

    #endregion

    #region Headers

    public static Entity AddHeaders<TRecord>(this Entity entity, Rel rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, typeof(TRecord), rel.ToString().AsSingle(), builder, select, except);
    }

    public static Entity AddHeaders<TRecord>(this Entity entity, string rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, typeof(TRecord), rel.AsSingle().NonNull(), builder, select, except);
    }

    public static Entity AddHeaders<TRecord>(this Entity entity, IEnumerable<Rel> rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, typeof(TRecord), rel.Select(x => x.GetName()), builder, select, except);
    }

    public static Entity AddHeaders<TRecord>(this Entity entity, IEnumerable<string> rel = null, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, typeof(TRecord), rel, builder, select, except);
    }

    public static Entity AddHeaders(this Entity entity, object recordOrType, Rel rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, recordOrType, rel.ToString().AsSingle(), builder, select, except);
    }

    public static Entity AddHeaders(this Entity entity, object recordOrType, string rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, recordOrType, rel.AsSingle().NonNull(), builder, select, except);
    }

    public static Entity AddHeaders(this Entity entity, object recordOrType, IEnumerable<Rel> rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, recordOrType, rel.Select(x => x.GetName()), builder, select, except);
    }

    public static Entity AddHeaders(this Entity entity, object recordOrType, IEnumerable<string> rel = null, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      var type = recordOrType is Type ? (Type)recordOrType : recordOrType.GetType();

      var properties =
        from property in Property.UnwrapPropertyInfo(type)
        where @select == null || property.Name.EqualsAnyIgnoreCase(@select)
        where except == null || !property.Name.EqualsAnyIgnoreCase(except)
        select property;

      foreach (var property in properties)
      {
        HeaderUtil.AddHeaderToEntity2(
            entity
          , property.Name
          , property.Title
          , DataTypeNames.GetDataTypeName(property.Type)
          , rel
          , builder
        );
      }
      return entity;
    }

    #endregion
  }
}
