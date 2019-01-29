using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Paper.Media;
using Paper.Media.Design;
using Sandbox.Lib.Domain.SmallApi;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;

namespace Sandbox.Lib.Domain
{
  public static class DbEntities
  {
    public static void CopyTable(Table table, Filter filter, Entity entity)
    {
      var apiBase = new UriString("^/Domain");

      var tableInfo = table._Get<TableInfo>("TableInfo");
      var id = table._Get(tableInfo.PkName);

      entity.AddLinkSelf(MakeDomainRoute(apiBase, table, id));
      entity.AddClass(Class.Data);
      entity.AddClass(Conventions.MakeName(table));

      AddProperties(table, filter, entity, apiBase);
      // /*Nao existe filtro quando Class=Data*/ AddFilter(table, filter, entity, uri);
      AddActions(table, filter, entity, apiBase);
    }

    private static void AddProperties(Table table, Filter filter, Entity entity, UriString apiBase)
    {
      var tableInfo = table._Get<TableInfo>("TableInfo");
      var filterInfo = table._Get<FilterInfo>("FilterInfo");

      var id = table._Get(tableInfo.PkName);

      foreach (var fieldName in tableInfo.FieldNames)
      {
        var property = table._GetPropertyInfo(fieldName);
        var propertyValue = table._Get(fieldName);

        var fkAttr = table._GetAttribute<FkAttribute>(fieldName);
        var titleAttr = table._GetAttribute<TitleAttribute>(fieldName);
        var hiddenAttr = table._GetAttribute<HiddenAttribute>(fieldName);

        var name = Conventions.MakeName(property);
        var type = Conventions.MakeDataType(property);
        var title = titleAttr?.Title ?? Conventions.MakeTitle(property);

        var hidden = (hiddenAttr != null);

        entity.AddDataHeader(name, builder => builder
          .AddDataType(type)
          .AddTitle(title)
          .AddHidden(hidden)
        );

        if (fkAttr?.ReferenceTable != null && propertyValue != null)
        {
          entity.AddLink(new Link
          {
            Href = (string)MakeDomainRoute(apiBase, fkAttr?.ReferenceTable, propertyValue),
            Rel = new[] { RelNames.DataLink, name },
            Title = $"{title} {propertyValue}"
          });
        }

        entity.AddProperty(name, propertyValue);
      }
    }

    private static void AddFilter(Table table, Filter filter, Entity entity, UriString apiBase)
    {
      var tableInfo = table._Get<TableInfo>("TableInfo");
      var filterInfo = table._Get<FilterInfo>("FilterInfo");

      var id = table._Get(tableInfo.PkName);

      entity.AddAction("filter", action =>
      {
        var href = apiBase;

        foreach (var fieldName in filterInfo.FieldNames)
        {
          var property = filterInfo.FilterType._GetPropertyInfo(fieldName);
          var propertyValue = filter?._Get(fieldName);

          var fkAttr =
            filterInfo.FilterType._GetAttribute<FkAttribute>(fieldName)
            ?? table._GetAttribute<FkAttribute>(fieldName);
          var titleAttr =
            filterInfo.FilterType._GetAttribute<TitleAttribute>(fieldName)
            ?? table._GetAttribute<TitleAttribute>(fieldName);

          var name = Conventions.MakeName(property);
          var type = Conventions.MakeDataType(property);
          var title = titleAttr?.Title ?? Conventions.MakeTitle(property);

          action.AddField(name, field =>
          {
            field.AddTitle(title);
            field.AddDataType(type);
            field.AddValue(propertyValue);
          });

          if (propertyValue != null)
          {
            var argName = name.ChangeCase(TextCase.CamelCase);
            href = href.SetArg(argName, propertyValue);
          }
        }

        action.AddHref(MakeDomainRoute(href, table, id));
        action.AddMethod(Method.Get);
        action.AddTitle("Filtro");
      });
    }

    private static void AddActions(Table table, Filter filter, Entity entity, UriString apiBase)
    {
      var tableInfo = table._Get<TableInfo>("TableInfo");
      var filterInfo = table._Get<FilterInfo>("FilterInfo");

      var id = table._Get(tableInfo.PkName);
      var href = MakeDomainRoute(apiBase, table, id);

      entity.AddAction("new", action =>
      {
        action.AddTitle("Criar");
        action.AddRel(Rel.SecondaryLink);
        action.AddHref(href);
        action.AddMethod(Method.Post);
      });

      entity.AddAction("edit", action =>
      {
        action.AddTitle("Editar");
        action.AddHref(href);
        action.AddRel(Rel.PrimaryLink);
        action.AddMethod(Method.Put);
      });

      entity.AddAction("delete", action =>
      {
        action.AddTitle("Remover");
        action.AddHref(href);
        action.AddMethod(Method.Delete);

        action.AddField("question", field =>
        {
          field.AddDataType(DataType.Label);
          field.AddTitle("Confirmação de Remoção");
          field.AddValue("O registro será removido definitivamente.\nTem certeza que deseja continuar?");
          field.AddMultiline(true);
        });
      });
    }

    public static void CopyTable(Entity entity, Table table)
    {

    }

    private static void CopyFilter(UriString uri, object targetFilter)
    {
      foreach (var argName in uri.GetArgNames())
      {
        if (targetFilter._Has(argName))
        {
          var value = uri._Get(argName);
          targetFilter._Set(argName, value);
        }
      }
    }

    private static void CopyFilter(object filter, UriString targetUri)
    {
    }

    private static UriString MakeDomainRoute(UriString baseRoute, object tableTypeOrInstance, object id = null)
    {
      var type = tableTypeOrInstance as Type ?? tableTypeOrInstance.GetType();

      var name =
        type.FullName
          .Split(',')
          .First()
          .Replace(".", "/")
          .Replace("+", "/")
          .Replace("`", "");

      id = (id != null) ? $"/{id}" : null;

      return baseRoute.Combine($"{name}{id}");
    }
  }
}
