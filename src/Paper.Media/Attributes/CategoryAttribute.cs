using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class CategoryAttribute : Attribute
  {
    public CategoryAttribute(string category)
    {
      this.Category = category;
    }

    public string Category { get; }
  }
}
