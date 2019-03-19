using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
  public class OptionAttribute : Attribute
  {
    public OptionAttribute(object value)
    {
      this.Value = value;
    }

    public OptionAttribute(object value, string title)
    {
      this.Value = value;
      this.Title = title;
    }

    public OptionAttribute(object value, string title, bool selected)
    {
      this.Value = value;
      this.Title = title;
      this.Selected = selected;
    }

    public object Value { get; set; }

    public string Title { get; set; }

    public bool? Selected { get; set; }
  }
}