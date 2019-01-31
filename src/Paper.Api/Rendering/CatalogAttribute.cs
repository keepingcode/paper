using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class CatalogAttribute : Attribute
  {
    public CatalogAttribute(string name)
    {
      this.Name = name;
    }

    public string Name { get; }
  }
}