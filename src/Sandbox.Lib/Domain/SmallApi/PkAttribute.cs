using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Lib.Domain.SmallApi
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class PkAttribute : Attribute
  {
    public PkAttribute(AutoIncrement autoIncrement)
    {
      this.IsAutoIncrement = (autoIncrement == AutoIncrement.Yes);
    }

    public bool IsAutoIncrement { get; }
  }
}
