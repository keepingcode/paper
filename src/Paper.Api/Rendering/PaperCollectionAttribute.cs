using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class PaperCollectionAttribute : Attribute
  {
    public PaperCollectionAttribute(string name)
    {
      this.Name = name;
    }

    public string Name { get; }
  }
}