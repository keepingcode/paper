using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Site
{
  public interface ICatalog // : IMenu, IEnumerable<IMenu>
  {
    ICollection<IMenu> Pages { get; }
  }
}
