using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class AllowManyAttribute : Attribute
  {
    public AllowManyAttribute()
    {
      this.AllowMany = true;
    }

    public AllowManyAttribute(bool allowMany)
    {
      this.AllowMany = allowMany;
    }

    public bool AllowMany { get; }
  }
}
