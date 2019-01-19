using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Reflection;

namespace Toolset
{
  public class Var
  {
    private object _rawValue;

    public Var()
      : this(typeof(object), null)
    {
    }

    public Var(object value)
      : this(typeof(object), value)
    {
    }

    public Var(Var any)
      : this(typeof(object), any.RawValue)
    {
    }

    protected Var(Type rawType, object rawValue)
    {
      this.RawType = rawType;
      this.RawValue = rawValue;
    }

    public Type RawType { get; }

    public VarKind Kind { get; set; }

    public bool IsNull => Kind == VarKind.Null;
    public bool IsValue => Kind == VarKind.Value;
    public bool IsArray => Kind == VarKind.Array;
    public bool IsRange => Kind == VarKind.Range;

    public object RawValue
    {
      get => _rawValue;
      set
      {
        while (value is Var any)
        {
          value = any.RawValue;
        }

        if (value == null || value == DBNull.Value)
        {
          _rawValue = null;
          Kind = VarKind.Null;
          return;
        }

        if (Range.IsRangeCompatible(value))
        {
          var signature = typeof(Range<>);
          var type = signature.MakeGenericType(RawType);

          if (value.GetType() != type)
          {
            var range = (Range)Activator.CreateInstance(type);
            range.Min = value._Get("min");
            range.Max = value._Get("max");
            value = range;
          }

          _rawValue = value;
          Kind = VarKind.Range;
        }
        else if (value is IEnumerable && !(value is string))
        {
          var signature = typeof(IList<>);
          var type = signature.MakeGenericType(RawType);

          var isArrayCompatible =
               typeof(IList).IsAssignableFrom(value.GetType())
            && type.IsAssignableFrom(value.GetType());

          if (!isArrayCompatible)
          {
            signature = typeof(List<>);
            type = signature.MakeGenericType(RawType);

            var sourceList = (value as IList) ?? ((IEnumerable)value).Cast<object>().ToArray();
            var targetList = (IList)Activator.CreateInstance(type, sourceList.Count);
            for (int i = 0; i < sourceList.Count; i++)
            {
              var sourceValue = sourceList[i];
              var targetValue = Change.To(sourceValue, RawType);
              targetList.Add(targetValue);
            }
            value = targetList;
          }

          _rawValue = value;
          Kind = VarKind.Array;
        }
        else
        {
          _rawValue = Change.To(value, RawType);
          Kind = VarKind.Value;
        }
      }
    }

    public object Value
    {
      get => IsValue ? RawValue : Default.Of(RawType);
      set => RawValue = value;
    }

    public IList Array
    {
      get => IsArray ? (IList)RawValue : null;
      set => RawValue = value;
    }

    public Range Range
    {
      get => IsRange ? (Range)RawValue : null;
      set => RawValue = value;
    }

    public override string ToString()
    {
      return IsArray
        ? $"{{{string.Join(", ", Array.Cast<object>())}}}"
        : Change.To<string>(RawValue);
    }

    public static Type GetUnderlyingType(Type type)
    {
      if (!typeof(Var).IsAssignableFrom(type))
        return null;

      if (!type.IsGenericType)
        return typeof(object);

      return type.GetGenericArguments().FirstOrDefault();
    }

    public static implicit operator Var(object[] value)
    {
      return new Var(value);
    }

    public static implicit operator Var(List<object> value)
    {
      return new Var(value);
    }

    public static implicit operator Var(Range value)
    {
      return new Var(value);
    }

    public static implicit operator object[] (Var value)
    {
      return value.Array as object[] ?? value.Array?.Cast<object>().ToArray();
    }

    public static implicit operator List<object>(Var value)
    {
      return value.Array as List<object> ?? value.Array?.Cast<object>().ToList();
    }

    public static implicit operator Range(Var value)
    {
      return value.Range;
    }
  }
}