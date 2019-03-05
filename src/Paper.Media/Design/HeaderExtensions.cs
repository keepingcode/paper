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
    #region Headers

    public static IEnumerable<IHeaderInfo> Headers(this Entity entity, Class @class)
    {
      return Headers(entity, @class.GetName());
    }

    public static IEnumerable<IHeaderInfo> Headers(this Entity entity, string @class)
    {
      var bag = entity?.Properties?[HeaderDesign.BagName] as PropertyMap;
      var headerNames = bag?[@class] as PropertyValueCollection;
      if (headerNames == null)
        yield break;

      foreach (object item in headerNames)
      {
        var headerName = (item as CaseVariantString)?.Value ?? (string)item;

        var header = (
          from child in entity.Children()
          where child.Class.Has(ClassNames.Header)
             && child.Rel.Has(@class)
             && headerName.EqualsIgnoreCase(child.Properties?["name"]?.ToString())
          select child
        ).FirstOrDefault();

        yield return (header != null)
          ? (IHeaderInfo)new HeaderDesign(header)
          : (IHeaderInfo)new HeaderInfo(headerName);
      }
    }

    public static IEnumerable<IHeaderInfo> Headers(this Entity entity)
    {
      var bag = entity?.Properties?[HeaderDesign.BagName] as PropertyMap;
      foreach (var bagName in bag.Keys)
      {
        var headerNames = bag[bagName] as PropertyValueCollection;
        foreach (CaseVariantString headerName in headerNames)
        {
          var header = (
            from child in entity.Children()
            where child.Class.Has(ClassNames.Header)
               && headerName.Value.EqualsIgnoreCase(child.Properties?["name"]?.ToString())
            select child
          ).FirstOrDefault();

          yield return (header != null)
            ? (IHeaderInfo)new HeaderDesign(header)
            : (IHeaderInfo)new HeaderInfo(headerName);
        }
      }
    }

    #endregion

    #region Header

    public static Entity AddHeader(this Entity entity, string name, Class @class, Action<HeaderDesign> options = null)
    {
      return AddHeader(entity, name, @class.GetName(), options);
    }

    public static Entity AddHeader(this Entity entity, string name, string @class, Action<HeaderDesign> options = null)
    {
      var headerBag = entity.WithProperties(HeaderDesign.BagName);
      var headerNames = headerBag.WithCollection(@class);

      var nameExists = headerNames.Any(x => (x as CaseVariantString)?.Value.EqualsIgnoreCase(name) == true);
      if (!nameExists)
      {
        headerNames.Add((CaseVariantString)name);
      }

      var headerEntity = (
        from e in entity?.Entities ?? Enumerable.Empty<Entity>()
        where e.Rel.Has(@class)
        where name.EqualsIgnoreCase((e.Properties?["Name"] as CaseVariantString)?.Value)
        select e
      ).FirstOrDefault();

      if (headerEntity == null && options != null)
      {
        var title = name.ChangeCase(TextCase.ProperCase);
        headerEntity = new Entity();
        headerEntity.AddClass(Class.Header);
        headerEntity.AddRel(@class);
        headerEntity.SetTitle(title);
        headerEntity.SetProperty("Name", (CaseVariantString)name);
        headerEntity.SetProperty("Title", title);
        entity.WithEntities().Add(headerEntity);
      }

      options?.Invoke(new HeaderDesign(headerEntity));

      return entity;
    }

    #endregion

    #region Headers

    public static Entity AddHeaders<TGraph>(this Entity entity, Class @class, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, typeof(TGraph), @class, @select, except);
    }

    public static Entity AddHeaders<TGraph>(this Entity entity, string @class, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, typeof(TGraph), @class, @select, except);
    }

    public static Entity AddHeaders(this Entity entity, object graphOrType, Class @class, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      return AddHeaders(entity, graphOrType, @class.GetName(), select, except);
    }

    public static Entity AddHeaders(this Entity entity, object graphOrType, string @class, IEnumerable<string> select = null, IEnumerable<string> except = null)
    {
      var type = graphOrType is Type ? (Type)graphOrType : graphOrType.GetType();

      var properties =
        from p in type.GetProperties()
        where p.CanRead && !p.GetIndexParameters().Any()
        where @select == null || @select.Contains(p.Name)
        where except == null || !except.Contains(p.Name)
        select p;

      foreach (var property in properties)
      {
        var title = Conventions.MakeTitle(property);
        var hidden = property.Name.StartsWith("_");
        entity.AddHeader(property.Name, @class, opt => opt
          .SetTitle(title)
          .SetHidden(hidden)
          .SetDataType(property.PropertyType)
        );
      }
      return entity;
    }

    #endregion

    #region Legacy

    //#region ForEach...

    //public static Entity ForEachHeader(this Entity entity, IEnumerable<string> rel, Action<Entity, HeaderInfo> inspection)
    //{
    //  if (entity.Entities != null)
    //  {
    //    var headers =
    //      from child in entity.Entities
    //      where child.Class?.Contains(ClassNames.Header) == true
    //      where !rel.Any() || rel.All(x => child.Class?.Contains(x) == true)
    //      select child;

    //    foreach (var header in headers)
    //    {
    //      var properties = header.Properties ?? (header.Properties = new PropertyCollection());
    //      inspection(header, new HeaderInfo(properties));
    //    }
    //  }
    //  return entity;
    //}

    //#endregion

    //#region Headers

    ////public static Entity AddHeader(this Entity entity, string name, string title = null, string rel = null, string dataType = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    ////{
    ////  var rels = rel.AsSingle().NonNull();
    ////  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    ////  return entity;
    ////}

    ////public static Entity AddHeader(this Entity entity, string name, Rel rel, string title = null, string dataType = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    ////{
    ////  var rels = rel.GetName().AsSingle();
    ////  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    ////  return entity;
    ////}

    ////public static Entity AddHeader(this Entity entity, string name, IEnumerable<string> rel, string title = null, string dataType = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    ////{
    ////  var rels = rel.NonNull();
    ////  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    ////  return entity;
    ////}

    ////public static Entity AddHeader(this Entity entity, string name, IEnumerable<Rel> rel, string title = null, string dataType = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    ////{
    ////  var rels = rel.Select(x => x.GetName());
    ////  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    ////  return entity;
    ////}

    ////public static Entity AddHeader(this Entity entity, string name, Type dataType, string title = null, string rel = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    ////{
    ////  var rels = rel.AsSingle().NonNull();
    ////  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    ////  return entity;
    ////}

    ////public static Entity AddHeader(this Entity entity, string name, Rel rel, Type dataType, string title = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    ////{
    ////  var rels = rel.GetName().AsSingle();
    ////  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    ////  return entity;
    ////}

    ////public static Entity AddHeader(this Entity entity, string name, IEnumerable<string> rel, Type dataType, string title = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    ////{
    ////  var rels = rel.NonNull();
    ////  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    ////  return entity;
    ////}

    ////public static Entity AddHeader(this Entity entity, string name, IEnumerable<Rel> rel, Type dataType, string title = null, bool? hidden = null, Action<HeaderBuilder> builder = null)
    ////{
    ////  var rels = rel.Select(x => x.GetName());
    ////  DoAddHeader(entity, name, title, rels, dataType, h => h.AddHidden(hidden == true), builder);
    ////  return entity;
    ////}

    //public static Entity AddHeader(this Entity entity, string name, Action<HeaderBuilder> builder)
    //{
    //  DoAddHeader(entity, name, null, Enumerable.Empty<string>(), null, builder);
    //  return entity;
    //}

    //private static void DoAddHeader(Entity entity, string name, string title, IEnumerable<string> rel, string dataType, params Action<HeaderBuilder>[] builders)
    //{
    //  HeaderUtil.AddHeaderToEntity2(entity, name, title, dataType, rel, builders);
    //}

    //#endregion

    //#region Headers

    //public static Entity AddHeaders<TRecord>(this Entity entity, Rel rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    //{
    //  DoAddHeaders(entity, typeof(TRecord), rel.ToString().AsSingle(), builder, select, except);
    //  return entity;
    //}

    //public static Entity AddHeaders<TRecord>(this Entity entity, string rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    //{
    //  DoAddHeaders(entity, typeof(TRecord), rel.AsSingle().NonNull(), builder, select, except);
    //  return entity;
    //}

    //public static Entity AddHeaders<TRecord>(this Entity entity, IEnumerable<Rel> rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    //{
    //  DoAddHeaders(entity, typeof(TRecord), rel.Select(x => x.GetName()), builder, select, except);
    //  return entity;
    //}

    //public static Entity AddHeaders<TRecord>(this Entity entity, IEnumerable<string> rel = null, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    //{
    //  DoAddHeaders(entity, typeof(TRecord), rel, builder, select, except);
    //  return entity;
    //}

    //public static Entity AddHeaders(this Entity entity, object recordOrType, Rel rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    //{
    //  DoAddHeaders(entity, recordOrType, rel.ToString().AsSingle(), builder, select, except);
    //  return entity;
    //}

    //public static Entity AddHeaders(this Entity entity, object recordOrType, string rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    //{
    //  DoAddHeaders(entity, recordOrType, rel.AsSingle().NonNull(), builder, select, except);
    //  return entity;
    //}

    //public static Entity AddHeaders(this Entity entity, object recordOrType, IEnumerable<Rel> rel, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    //{
    //  DoAddHeaders(entity, recordOrType, rel.Select(x => x.GetName()), builder, select, except);
    //  return entity;
    //}

    //public static Entity AddHeaders(this Entity entity, object recordOrType, IEnumerable<string> rel = null, Action<HeaderBuilder> builder = null, IEnumerable<string> select = null, IEnumerable<string> except = null)
    //{
    //  var type = recordOrType is Type ? (Type)recordOrType : recordOrType.GetType();

    //  var properties =
    //    from property in Property.UnwrapPropertyInfo(type)
    //    where @select == null || property.Name.EqualsAnyIgnoreCase(@select)
    //    where except == null || !property.Name.EqualsAnyIgnoreCase(except)
    //    select property;

    //  foreach (var property in properties)
    //  {
    //    HeaderUtil.AddHeaderToEntity2(
    //        entity
    //      , property.Name
    //      , property.Title
    //      , DataTypeNames.GetDataTypeName(property.Type)
    //      , rel
    //      , builder
    //    );
    //  }
    //  return entity;
    //}

    //private static void DoAddHeaders(Entity entity, object recordOrType, IEnumerable<string> rel, Action<HeaderBuilder> builder, IEnumerable<string> select, IEnumerable<string> except)
    //{
    //  var type = recordOrType is Type ? (Type)recordOrType : recordOrType.GetType();
    //  var properties =
    //    from property in Property.UnwrapPropertyInfo(type)
    //    where @select == null || property.Name.EqualsAnyIgnoreCase(@select)
    //    where except == null || !property.Name.EqualsAnyIgnoreCase(except)
    //    select property;
    //  foreach (var property in properties)
    //  {
    //    HeaderUtil.AddHeaderToEntity2(
    //        entity
    //      , property.Name
    //      , property.Title
    //      , DataTypeNames.GetDataTypeName(property.Type)
    //      , rel
    //      , builder
    //    );
    //  }
    //}

    //#endregion

    #endregion
  }
}
