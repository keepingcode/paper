using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Lib.Domain.SmallApi
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
