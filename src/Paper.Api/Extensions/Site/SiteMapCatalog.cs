using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Commons;

namespace Paper.Api.Extensions.Site
{
  public class SiteMapCatalog : Catalog<ISiteMap>, ISiteMapCatalog
  {
    public SiteMapCatalog()
      : base(item => item.Href)
    {
    }
  }
}
