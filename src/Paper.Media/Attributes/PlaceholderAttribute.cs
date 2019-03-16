using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class PlaceholderAttribute : Attribute
  {
    public PlaceholderAttribute(string text)
    {
      this.Placeholder = text;
    }

    public string Placeholder { get; }
  }
}
