using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Api.Extensions.Site
{
  public class SiteMap : ISiteMap
  {
    private List<IRoute> _items;

    public virtual Href Href { get; set; }

    public string Title { get; set; }

    public PropertyCollection Properties { get; set; }

    public virtual List<IRoute> Items
    {
      get => _items ?? (_items = new List<IRoute>());
      set => _items = value;
    }

    ICollection<IRoute> ISiteMap.Items => Items;
  }
}