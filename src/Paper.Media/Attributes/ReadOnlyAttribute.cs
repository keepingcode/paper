using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class ReadOnlyAttribute : Attribute
  {
    public ReadOnlyAttribute()
    {
      this.ReadOnly = true;
    }

    public ReadOnlyAttribute(bool readOnly)
    {
      this.ReadOnly = readOnly;
    }

    public bool ReadOnly { get; }
  }
}
