using System;
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
      return ConvertAny(value, targetType, Default.Of(targetType));
    }

    public static object To(object value, Type targetType, object defaultValue)
    {
      return ConvertAny(value, targetType, defaultValue);
    }

    public static T To<T>(object value)
    {
      return (T)ConvertAny(value, typeof(T), default(T));
    }

    public static T To<T>(object value, T defaultValue)
    {
      return (T)ConvertAny(value, typeof(T), defaultValue);
    }

    public static object ToOrDefault(object value, Type targetType)
    {
      try
      {
        return ConvertAny(value, targetType, Default.Of(targetType));
      }
      catch
      {
        return Default.Of(targetType);
      }
    }

    public static object ToOrDefault(object value, Type targetType, object defaultValue)
    {
      try
      {
        return ConvertAny(value, targetType, defaultValue);
      }
      catch
      {
        return defaultValue;
      }
    }

    public static T ToOrDefault<T>(object value)
    {
      try
      {
        return (T)ConvertAny(value, typeof(T), default(T));
      }
      catch
      {
        return default(T);
      }
    }

    public static T ToOrDefault<T>(object value, T defaultValue)
    {
      try
      {
        return (T)ConvertAny(value, typeof(T), defaultValue);
      }
      catch
      {
        return defaultValue;
      }
    }

    public static bool TryTo(object value, Type targetType, out object result)
    {
      try
      {
        result = ConvertAny(value, targetType, Default.Of(targetType));
        return true;
      }
      catch
      {
        result = Default.Of(targetType);
        return false;
      }
    }

    public static bool TryTo<T>(object value, out T result)
    {
      try
      {
        result = (T)ConvertAny(value, typeof(T), default(T));
        return true;
      }
      catch
      {
        result = default(T);
        return false;
      }
    }

    private static object ConvertAny(object value, Type targetType, object defaultValue)
    {
      if (defaultValue == null)
      {
        defaultValue = Default.Of(targetType);
      }

      if (value == null || value == DBNull.Value)
      {
        return defaultValue;
      }

      if (Is.Collection(targetType))
      {
        var elementType = TypeOf.CollectionElement(targetType);
        if (targetType.IsArray)
        {
          var sourceList = value as IList ?? new[] { value };
          var targetList = (IList)Activator.CreateInstance(targetType, sourceList.Count);
          for (int i = 0; i < sourceList.Count; i++)
          {
            targetList[i] = ConvertSingleValue(sourceList[i], elementType);
          }
          return targetList;
        }
        else
        {
          var sourceList = value as IList ?? new[] { value };
          var targetList = (IList)Activator.CreateInstance(targetType);
          foreach (var sourceElement in sourceList)
          {
            var targetElement = ConvertSingleValue(sourceElement, elementType);
            targetList.Add(targetElement);
          }
          return targetList;
        }
      }
      else
      {
        return ConvertSingleValue(value, targetType) ?? defaultValue;
      }
    }

    private static object ConvertSingleValue(object value, Type targetType)
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
                || text.Equals("y", StringComparison.InvariantCultureIgnoreCase)
                || text.Equals("yes", StringComparison.InvariantCultureIgnoreCase)
                || text.Equals("on", StringComparison.InvariantCultureIgnoreCase)
                || text.Equals("ok", StringComparison.InvariantCultureIgnoreCase)
                || text.Equals("sim", StringComparison.InvariantCultureIgnoreCase);
          }
          else if (sourceType.IsValueType)
          {
            return !value.Equals(Default.Of(sourceType));
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

        if (targetType == typeof(TimeSpan))
        {
          if (value is string timeSpan && Regex.IsMatch(timeSpan, @"(\d\.)?\d{2}:\d{2}.*"))
          {
            return DateTime.Parse(timeSpan);
          }
          if (value is long ticks)
          {
            return TimeSpan.FromTicks(ticks);
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