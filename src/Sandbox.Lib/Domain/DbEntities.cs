using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Paper.Media;
using Sandbox.Lib.Domain.SmallApi;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;

namespace Sandbox.Lib.Domain
{
  public static class DbEntities
  {
    public static void CopyTable(Table table, Entity targetEntity)
    {
      var tableInfo = table.GetType()._Get<TableInfo>("TableInfo");
      var filterInfo = table.GetType()._Get<FilterInfo>("FilterInfo");

      Debug.WriteLine(tableInfo);
      Debug.WriteLine(filterInfo);
    }

    public static void CopyTable(Entity entity, Table table)
    {

    }

    public static void CopyFilter(UriString uri, object targetFilter)
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

    public static void CopyFilter(object filter, UriString targetUri)
    {
    }
  }
}
