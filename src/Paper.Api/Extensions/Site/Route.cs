using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Api.Extensions.Site
{
  public class Route : IRoute
  {
    public virtual Href Href { get; set; }

    public virtual string Title { get; set; }

    public virtual PropertyCollection Properties { get; set; }
  }
}