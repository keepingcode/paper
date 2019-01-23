using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Lib.Domain.SmallApi
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class FkAttribute : Attribute
  {
    public FkAttribute(Type referenceTable)
    {
      this.ReferenceTable = referenceTable;
    }

    public Type ReferenceTable { get; }
  }
}
