﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;
using System.Reflection;
using Paper.Media.Routing;

namespace Paper.Media.Design.Mappings
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
  public class FieldRequiredAttribute : FieldAttribute
  {
    public bool Required { get; }

    public FieldRequiredAttribute(bool allow = true)
    {
      Required = allow;
    }

    internal override void RenderField(Field field, PropertyInfo property, object host)
    {
      field.AddRequired(Required);
    }
  }
}