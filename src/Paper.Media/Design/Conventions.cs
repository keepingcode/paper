using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Paper.Media;
using Toolset;

namespace Paper.Media.Design
{
  public static class Conventions
  {
    public static string MakeName(string name)
    {
      name = name.Split("|").First();
      if (name.StartsWithIgnoreCase("DF"))
      {
        name = name.Substring(2);
      }
      name = name.ChangeCase(TextCase.PascalCase);
      return name;
    }

    public static string MakeName(object graph)
    {
      if (graph == null)
      {
        return null;
      }
      else if (graph is MemberInfo member)
      {
        return MakeName(member);
      }
      else if (graph is ParameterInfo parameter)
      {
        return MakeName(parameter);
      }
      else
      {
        return MakeName(graph.GetType());
      }
    }

    public static string MakeName(MemberInfo member)
    {
      if (member is Type type)
      {
        if (type.FullName.Contains("AnonymousType"))
          return null;

        var name = type.FullName.Split(',').First().Replace("+", ".");
        return name;
      }
      else
      {
        return member.Name;
      }
    }

    public static string MakeName(ParameterInfo parameter)
    {
      return parameter.Name;
    }

    public static string MakeName(DataColumn column)
    {
      var name = column.Caption ?? column.ColumnName ?? ("Col" + column.Ordinal);
      return MakeName(name);
    }

    public static string MakeTitle(string name)
    {
      if (name.Contains("|"))
      {
        name = name.Split("|").Last();
      }
      if (name.StartsWithIgnoreCase("DF"))
      {
        name = name.Substring(2);
      }

      name = name.ChangeCase(TextCase.ProperCase | TextCase.PreserveSpecialCharacters);
      return name;
    }

    public static string MakeTitle(object graph)
    {
      if (graph == null)
      {
        return null;
      }
      else if (graph is MemberInfo member)
      {
        return MakeTitle(member);
      }
      else if (graph is ParameterInfo parameter)
      {
        return MakeTitle(parameter);
      }
      else
      {
        return MakeTitle(graph.GetType());
      }
    }

    public static string MakeTitle(MemberInfo member)
    {
      if (member is Type type)
      {
        if (type.FullName.Contains("AnonymousType"))
          return null;

        var name = type.Name
            .Replace("Paper", "")
            .Replace("Entity", "")
            .ChangeCase(TextCase.ProperCase);
        return name;
      }
      else
      {
        var attr = member
          .GetCustomAttributes(true)
          .OfType<DisplayNameAttribute>()
          .FirstOrDefault();

        var name = attr?.DisplayName ?? member.Name;
        return MakeTitle(name);
      }
    }

    public static string MakeTitle(ParameterInfo parameter)
    {
      var attr = parameter
        .GetCustomAttributes(true)
        .OfType<DisplayNameAttribute>()
        .FirstOrDefault();

      var name = attr?.DisplayName ?? parameter.Name;
      return MakeTitle(name);
    }

    public static string MakeTitle(DataColumn column)
    {
      var name = column.Caption ?? column.ColumnName ?? ("Col" + column.Ordinal);
      return MakeTitle(name);
    }

    public static string MakeDataType(Type type)
    {
      return DataTypeNames.FromType(type) ?? DataTypeNames.String;
    }

    public static string MakeDataType(MemberInfo member)
    {
      Type type;
      if (member is Type)
      {
        type = (Type)member;
      }
      else if (member is FieldInfo field)
      {
        type = field.FieldType;
      }
      else if (member is PropertyInfo property)
      {
        type = property.PropertyType;
      }
      else
      {
        return null;
      }
      return DataTypeNames.FromType(type) ?? DataTypeNames.String;
    }

    public static string MakeDataType(ParameterInfo parameter)
    {
      var type = parameter.ParameterType;
      return DataTypeNames.FromType(type) ?? DataTypeNames.String;
    }

    public static string MakeDataType(DataColumn col)
    {
      return DataTypeNames.FromType(col.DataType) ?? DataTypeNames.String;
    }
  }
}