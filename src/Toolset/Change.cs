﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Toolset.Reflection;

namespace Toolset
{
  public static class Change
  {
    public static object To(object value, Type targetType)
    {
      return To(value, targetType, Default.Of(targetType));
    }

    public static object To(object value, Type targetType, object defaultValue)
    {
      if (defaultValue == null)
      {
        defaultValue = Default.Of(targetType);
      }

      if (value == null || value == DBNull.Value)
      {
        return defaultValue;
      }

      if (typeof(IList).IsAssignableFrom(targetType)
       || typeof(IList<>).IsAssignableFrom(targetType))
      {
        var genericListType = (
          from type in targetType.GetInterfaces()
          where type.IsGenericType
             && type.GetGenericTypeDefinition() == typeof(IList<>)
          select type
        ).FirstOrDefault();

        if (targetType.IsArray)
        {
          var elementType = targetType.GetElementType();
          var sourceList = value as IList ?? new[] { value };
          var targetList = (IList)Activator.CreateInstance(targetType, sourceList.Count);
          for (int i = 0; i < sourceList.Count; i++)
          {
            targetList[i] = ConvertTo(sourceList[i], elementType);
          }
          return targetList;
        }
        else if (genericListType != null)
        {
          var elementType = genericListType.GetGenericArguments().First();
          var sourceList = value as IList ?? new[] { value };
          var targetList = (IList)Activator.CreateInstance(targetType);
          foreach (var sourceElement in sourceList)
          {
            var targetElement = ConvertTo(sourceElement, elementType);
            targetList.Add(targetElement);
          }
          return targetList;
        }
        else
        {
          var sourceList = value as IList ?? new[] { value };
          var targetList = (IList)Activator.CreateInstance(targetType, sourceList.Count);
          for (int i = 0; i < sourceList.Count; i++)
          {
            targetList[i] = sourceList[i];
          }
          return targetList;
        }
      }
      else
      {
        return ConvertTo(value, targetType) ?? defaultValue;
      }
    }

    public static T To<T>(object value)
    {
      return To<T>(value, default(T));
    }

    public static T To<T>(object value, T defaultValue)
    {
      return (T)To(value, typeof(T), defaultValue);
    }

    public static object ToOrDefault(object value, Type targetType)
    {
      return ToOrDefault(value, targetType, Default.Of(targetType));
    }

    public static object ToOrDefault(object value, Type targetType, object defaultValue)
    {
      try
      {
        return To(value, targetType, defaultValue);
      }
      catch
      {
        return defaultValue;
      }
    }

    public static T ToOrDefault<T>(object value)
    {
      return ToOrDefault<T>(value, default(T));
    }

    public static T ToOrDefault<T>(object value, T defaultValue)
    {
      try
      {
        return To<T>(value, defaultValue);
      }
      catch
      {
        return defaultValue;
      }
    }

    private static object ConvertTo(object value, Type targetType)
    {
      Type sourceType = value?.GetType();
      try
      {
        if (value == null || value == DBNull.Value)
        {
          return null;
        }

        if (targetType.IsAssignableFrom(sourceType))
        {
          return value;
        }

        if (targetType == typeof(bool))
        {
          if (value is string text)
          {
            return text == "1"
                || text.Equals("true", StringComparison.InvariantCultureIgnoreCase)
                || text.Equals("yes", StringComparison.InvariantCultureIgnoreCase)
                || text.Equals("on", StringComparison.InvariantCultureIgnoreCase)
                || text.Equals("ok", StringComparison.InvariantCultureIgnoreCase)
                || text.Equals("sim", StringComparison.InvariantCultureIgnoreCase);
          }
          else if (value is IConvertible convertible)
          {
            var number = convertible.ToDecimal(CultureInfo.InvariantCulture);
            return number.CompareTo(0M) != 0;
          }
          else
          {
            return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
          }
        }

        if (targetType == typeof(string))
        {
          if (value is DateTime dateAndTime)
          {
            if (dateAndTime.Hour == 0
             && dateAndTime.Minute == 0
             && dateAndTime.Second == 0
             && dateAndTime.Millisecond == 0)
            {
              return dateAndTime.ToString("yyyy-MM-dd");
            }
            else
            {
              return dateAndTime.ToString("yyyy-MM-ddTHH:mm:ss");
            }
          }
          else
          {
            if (value._HasMethod("ToString", typeof(IFormatProvider)))
            {
              return value._Call("ToString", CultureInfo.InvariantCulture);
            }
            else
            {
              return value.ToString();
            }
          }
        }

        if (targetType == typeof(Guid))
        {
          return Guid.Parse(value.ToString());
        }

        if (targetType == typeof(Version))
        {
          return Version.Parse(value.ToString());
        }

        if (targetType == typeof(DateTime) && value is string dateTime)
        {
          if (Regex.IsMatch(dateTime, @"\d{4}-\d{2}-\d{2}.*"))
          {
            return DateTime.Parse(dateTime, CultureInfo.InvariantCulture);
          }
        }

        if (targetType == typeof(TimeSpan) && value is string timeSpan)
        {
          if (Regex.IsMatch(timeSpan, @"(\d\.)?\d{2}:\d{2}.*"))
          {
            return DateTime.Parse(timeSpan);
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
      catch (InvalidCastException ex)
      {
        throw new InvalidCastException(
          $"Impossível converter {sourceType.FullName} em {targetType.FullName}.",
          ex
        );
      }
    }
  }
}