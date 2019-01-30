using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Rendering
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class CatalogAttribute : Attribute
  {
    public CatalogAttribute(string guid)
    {
      this.Id = Guid.Parse(guid);
    }

    public Guid Id { get; }
  }
}