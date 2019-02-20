using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Media;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Extensions.Papers
{
  public static class Tables
  {
    public static IFormatter MakeTable<T>(IEnumerable<T> records)
    {
      var recordType = TypeOf.CollectionElement(records);
      return MakeTable(recordType);
    }

    public static IFormatter MakeTable<TRecord>()
    {
      return MakeTable(typeof(TRecord));
    }

    public static IFormatter MakeTable(Type recordType)
    {
      return Formatter.Format((context, factory, entity) =>
      {
        entity.AddClass(ClassNames.Table);
        entity.Children().ForEach(e => e.AddRel(ClassNames.Item));
        entity.AddHeaders(recordType, Class.Table);
      });
    }
  }
}