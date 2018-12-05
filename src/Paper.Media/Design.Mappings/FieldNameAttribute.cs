using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;
using System.Reflection;
using Paper.Media.Routing;

namespace Paper.Media.Design.Mappings
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
  public class FieldNameAttribute : FieldAttribute
  {
    public string Value { get; }

    public FieldNameAttribute(string name)
    {
      Value = name;
    }

    internal override void RenderField(Field field, PropertyInfo property, object host)
    {
      if (Value != null)
      {
        field.Name = Value;
      }
    }
  }
}