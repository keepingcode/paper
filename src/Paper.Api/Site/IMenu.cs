using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Api.Site
{
  public interface IMenu
  {
    ICatalog Parent { get; }

    Href Href { get; }

    string Title { get; }

    string Description { get; }
  }
}
