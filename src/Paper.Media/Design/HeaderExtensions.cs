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

    //public static Entity AddHeader(this Entity entity, string name, string title = null, string rel = null, string dataType = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    //{
    //  var rels = rel.AsSingle().NonNull();
    //  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    //  return entity;
    //}

    //public static Entity AddHeader(this Entity entity, string name, Rel rel, string title = null, string dataType = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    //{
    //  var rels = rel.GetName().AsSingle();
    //  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    //  return entity;
    //}

    //public static Entity AddHeader(this Entity entity, string name, IEnumerable<string> rel, string title = null, string dataType = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    //{
    //  var rels = rel.NonNull();
    //  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    //  return entity;
    //}

    //public static Entity AddHeader(this Entity entity, string name, IEnumerable<Rel> rel, string title = null, string dataType = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    //{
    //  var rels = rel.Select(x => x.GetName());
    //  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    //  return entity;
    //}

    //public static Entity AddHeader(this Entity entity, string name, Type dataType, string title = null, string rel = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    //{
    //  var rels = rel.AsSingle().NonNull();
    //  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    //  return entity;
    //}

    //public static Entity AddHeader(this Entity entity, string name, Rel rel, Type dataType, string title = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    //{
    //  var rels = rel.GetName().AsSingle();
    //  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    //  return entity;
    //}

    //public static Entity AddHeader(this Entity entity, string name, IEnumerable<string> rel, Type dataType, string title = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    //{
    //  var rels = rel.NonNull();
    //  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    //  return entity;
    //}

    //public static Entity AddHeader(this Entity entity, string name, IEnumerable<Rel> rel, Type dataType, string title = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    //{
    //  var rels = rel.Select(x => x.GetName());
    //  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    //  return entity;
    //}

    public static Entity AddHeader(this Entity entity, string name, Action<HeaderBuilder> builder)
    {
      DoAddHeader(entity, name, null, Enumerable.Empty<string>(), null, builder);
      return entity;
    }

    private static void DoAddHeader(Entity entity, string name, string title, IEnumerable<string> rel, string dataType, params Action<HeaderBuilder>[] builders)
    {
      HeaderUtil.AddHeaderToEntity2(entity, name, title, dataType, rel, builders);
    }

    #endregion

    #region Headers

    public static Entity AddHeaders<TRecord>(this Entity entity, Rel rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      DoAddHeaders(entity, typeof(TRecord), rel.ToString().AsSingle(), builder, select, except);
      return entity;
    }

    public static Entity AddHeaders<TRecord>(this Entity entity, string rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      DoAddHeaders(entity, typeof(TRecord), rel.AsSingle().NonNull(), builder, select, except);
      return entity;
    }

    public static Entity AddHeaders<TRecord>(this Entity entity, IEnumerable<Rel> rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      DoAddHeaders(entity, typeof(TRecord), rel.Select(x => x.GetName()), builder, select, except);
      return entity;
    }

    public static Entity AddHeaders<TRecord>(this Entity entity, IEnumerable<string> rel = null, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      DoAddHeaders(entity, typeof(TRecord), rel, builder, select, except);
      return entity;
    }

    public static Entity AddHeaders(this Entity entity, object recordOrType, Rel rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      DoAddHeaders(entity, recordOrType, rel.ToString().AsSingle(), builder, select, except);
      return entity;
    }

    public static Entity AddHeaders(this Entity entity, object recordOrType, string rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      DoAddHeaders(entity, recordOrType, rel.AsSingle().NonNull(), builder, select, except);
      return entity;
    }

    public static Entity AddHeaders(this Entity entity, object recordOrType, IEnumerable<Rel> rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      DoAddHeaders(entity, recordOrType, rel.Select(x => x.GetName()), builder, select, except);
      return entity;
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

    private static void DoAddHeaders(Entity entity, object recordOrType, IEnumerable<string> rel, Action<HeaderBuilder> builder, IEnumerable<string> select, IEnumerable<string> except)
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
    }

    #endregion
  }
}
