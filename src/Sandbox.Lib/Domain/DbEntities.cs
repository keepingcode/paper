using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public static void CopyTable(Table table, Entity entity)
    {
      var tableInfo = table._Get<TableInfo>("TableInfo");
      var filterInfo = table._Get<FilterInfo>("FilterInfo");

      entity.AddClass(Class.Data);
      entity.AddClass(Conventions.MakeName(table));

      foreach (var fieldName in tableInfo.FieldNames)
      {
        var property = table._GetPropertyInfo(fieldName);
        var propertyValue = table._Get(fieldName);

        //var attribute = property._GetAttribute<HiddenAttribute>(fieldName);

        var name = Conventions.MakeName(property);
        var type = Conventions.MakeDataType(property);
        var title = Conventions.MakeTitle(property);

        var hidden =
          //(attribute != null)
          //||
          fieldName.Equals(tableInfo.PkName) && tableInfo.PkAutoIncrement;

        entity.AddDataHeader(name, builder => builder
          .AddDataType(type)
          .AddTitle(title)
          .AddHidden(hidden)
        );

        entity.AddProperty(name, propertyValue);
      }
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
  }
}
