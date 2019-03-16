using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class PatternAttribute : Attribute
  {
    public PatternAttribute(string pattern)
    {
      this.Pattern = pattern;
    }

    public string Pattern { get; }
  }
}
