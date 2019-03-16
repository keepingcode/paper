using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class TitleAttribute : Attribute
  {
    public TitleAttribute(string title)
    {
      this.Title = title;
    }

    public string Title { get; }
  }
}
