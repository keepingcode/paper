﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Toolset.Collections;
using System.Reflection;
using Paper.Media.Routing;

namespace Paper.Media.Design.Mappings
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
  public class FieldTitleAttribute : FieldAttribute
  {
    public string Value { get; }

    public FieldTitleAttribute(string title)
    {
      this.Value = title;
    }

    internal override void RenderField(Field field, PropertyInfo property, object host)
    {
      field.AddTitle(Value);
    }
  }
}