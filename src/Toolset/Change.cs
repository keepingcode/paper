﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Toolset
{
  public static class Change
  {
    public static object To(object value, Type targetType)
    {
      var sourceType = value?.GetType();

      if (value == null)
        return null;

      // Melhoria para tipos Nullable<T>
      //
      //  var type = targetType;
      //
      //  var isNullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
      //  if (isNullable)
      //    type = Nullable.GetUnderlyingType(type);
      //
      //  ... conversao simples ...
      //
      //  if (isNullable)
      //    value = Activator.CreateInstance(targetType, value);

      if (targetType.IsAssignableFrom(sourceType))
        return value;

      if (targetType == typeof(string))
        return value.ToString();

      if (targetType == typeof(DateTime) && value is string)
      {
        var text = (string)value;
        if (Regex.IsMatch(text, @"\d{4}-\d{2}-\d{2}.*"))
        {
          return DateTime.Parse(text, CultureInfo.InvariantCulture);
        }
      }

      if (targetType == typeof(TimeSpan) && value is string)
      {
        var text = (string)value;
        if (Regex.IsMatch(text, @"(\d\.)?\d{2}:\d{2}.*"))
        {
          return DateTime.Parse(text);
        }
      }

      var flags = BindingFlags.Static | BindingFlags.Public;

      var methods = sourceType.GetMethods(flags).Concat(targetType.GetMethods(flags));
      var casting = (
        from method in methods
        where method.Name == "op_Implicit"
           || method.Name == "op_Explicit"
        where method.GetParameters().Length == 1
           && sourceType.IsAssignableFrom(method.GetParameters().Single().ParameterType)
           && targetType.IsAssignableFrom(method.ReturnType)
        select method
      ).FirstOrDefault();

      if (casting != null)
      {
        var castValue = casting.Invoke(null, new[] { value });
        return castValue;
      }

      targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

      object convertedValue;

      if (targetType.IsEnum)
      {
        var text = value.ToString();
        if (Regex.IsMatch(text, "[0-9]+"))
        {
          int number = int.Parse(text);
          convertedValue = Enum.ToObject(targetType, number);
        }
        else
        {
          convertedValue = Enum.Parse(targetType, text);
        }
      }
      else
      {
        convertedValue = Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
      }

      return convertedValue;
    }

    public static T To<T>(object value)
    {
      var convertedValue = To(value, typeof(T));
      return (convertedValue == null) ? default(T) : (T)convertedValue;
    }
  }
}