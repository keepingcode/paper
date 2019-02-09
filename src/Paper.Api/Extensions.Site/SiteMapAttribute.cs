using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Commons;

namespace Paper.Api.Extensions.Site
{
  public class SiteMapAttribute : CatalogAttribute
  {
    public SiteMapAttribute(string collectionName)
      : base(collectionName)
    {
    }
  }
}