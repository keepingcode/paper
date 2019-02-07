using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Commons
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class CatalogAttribute : Attribute
  {
    public CatalogAttribute(string collectionName)
    {
      this.CollectionName = collectionName;
    }

    public string CollectionName { get; }
  }
}