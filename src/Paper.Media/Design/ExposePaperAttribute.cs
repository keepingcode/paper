﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;

namespace Paper.Media.Design
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public class ExposePaperAttribute : ExposeAttribute
  {
    private string _uriTemplate;

    public string UriTemplate
    {
      get => _uriTemplate;
      set
      {
        if (value == null)
          _uriTemplate = null;
        else
          _uriTemplate = !value.StartsWith("/") ? ("/" + value) : value;
      }
    }

    public ExposePaperAttribute()
    {
    }

    public ExposePaperAttribute(string uriTemplate)
    {
      this.UriTemplate = uriTemplate;
    }

    public static ExposePaperAttribute Extract(object typeOrObject)
    {
      var type = typeOrObject as Type ?? typeOrObject?.GetType();
      if (type == null)
        return null;

      var attr = type
        .GetCustomAttributes(true)
        .OfType<ExposePaperAttribute>()
        .FirstOrDefault();

      if (attr == null)
      {
        attr = new ExposePaperAttribute();
      }

      if (attr.UriTemplate == null)
      {
        var name = type.FullName;

        if (name.EndsWith("Paper"))
        {
          name = name.Substring(0, name.Length - "Paper".Length);
        }
        if (name.EndsWith("Entity"))
        {
          name = name.Substring(0, name.Length - "Entity".Length);
        }

        attr.UriTemplate = "/" + name.Replace(".", "/");
      }

      return attr;
    }
  }
}