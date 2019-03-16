using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class AllowWildcardAttribute : Attribute
  {
    public AllowWildcardAttribute()
    {
      this.AllowWildcard = true;
    }

    public AllowWildcardAttribute(bool allowWildcard)
    {
      this.AllowWildcard = allowWildcard;
    }

    public bool AllowWildcard { get; }
  }
}
