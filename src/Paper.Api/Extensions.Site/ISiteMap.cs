using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Extensions.Site
{
  public interface ISiteMap : IRoute
  {
    ICollection<IRoute> Items { get; }
  }
}
