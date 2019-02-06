using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Api.Site
{
  public class Menu : IMenu
  {
    public ICatalog Parent { get; set; }

    public Href Href { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
  }
}
