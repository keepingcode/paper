using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolset
{
  public class Var<T> : Var
  {
    public Var()
      : base(typeof(T), null)
    {
    }

    public Var(T value)
      : base(typeof(T), value)
    {

    }

    public Var(IList<T> value)
      : base(typeof(T), value)
    {
    }

    public Var(Range<T> value)
      : base(typeof(T), value)
    {
    }

    public Var(Var any)
      : base(typeof(T), any.RawValue)
    {
    }

    public new T Value
    {
      get => (T)base.Value;
      set => base.Value = value;
    }

    public new IList<T> Array
    {
      get => (IList<T>)base.Array;
      set => base.Array = (IList)value;
    }

    public new Range<T> Range
    {
      get => (Range<T>)base.Range;
      set => base.Range = value;
    }

    public static implicit operator Var<T>(T value)
    {
      return new Var<T>(value);
    }

    public static implicit operator Var<T>(T[] value)
    {
      return new Var<T>(value);
    }

    public static implicit operator Var<T>(List<T> value)
    {
      return new Var<T>(value);
    }

    public static implicit operator Var<T>(Range<T> value)
    {
      return new Var<T>(value);
    }

    public static implicit operator T(Var<T> value)
    {
      return value.Value;
    }

    public static implicit operator T[] (Var<T> value)
    {
      return value.Array as T[] ?? value.Array?.ToArray();
    }

    public static implicit operator List<T>(Var<T> value)
    {
      return value.Array as List<T> ?? value.Array?.ToList();
    }

    public static implicit operator Range<T>(Var<T> value)
    {
      return value.Range;
    }
  }
}
