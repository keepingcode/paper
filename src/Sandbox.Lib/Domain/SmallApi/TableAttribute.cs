using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Lib.Domain.SmallApi
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class TableAttribute : Attribute
  {
    public TableAttribute()
    {
    }

    public TableAttribute(string name)
    {
      this.Name = name;
    }

    public string Schema { get; set; }

    public string Name { get; set; }
  }
}
