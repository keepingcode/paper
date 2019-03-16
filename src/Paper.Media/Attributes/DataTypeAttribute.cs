using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class DataTypeAttribute : Attribute
  {
    public DataTypeAttribute()
    {
    }

    public DataTypeAttribute(DataType type)
    {
      this.DataType = type.GetName();
    }

    public string DataType { get; set; }
  }
}
