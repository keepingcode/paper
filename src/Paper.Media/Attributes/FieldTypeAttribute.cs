using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class FieldTypeAttribute : Attribute
  {
    public FieldTypeAttribute(FieldType type)
    {
      this.FieldType = type.GetName();
    }

    public string FieldType { get; }
  }
}
