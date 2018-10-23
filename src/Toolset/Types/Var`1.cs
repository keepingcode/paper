using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Toolset.Types
{
  public class Var<T> : Var
  {
    public Var()
      : base(typeRestriction: typeof(T))
    {
    }

    public Var(T value)
      : base(typeRestriction: typeof(T))
    {
      this.Value = value;
    }

    public Var(IList<T> list)
      : base(typeRestriction: typeof(T))
    {
      this.List = list;
    }

    public Var(Range<T> range)
      : base(typeRestriction: typeof(T))
    {
      this.RawValue = range;
    }

    public new T Value
    {
      get => base.Value is T value ? value : default(T);
      set => base.Value = value;
    }

    public new IList<T> List
    {
      get => base.List is IList<T> value ? value : null;
      set => base.List = (IList)(object)value;
    }

    public new Range<T> Range
    {
      get => base.Range;
      set => base.Range = value;
    }
  }
}
