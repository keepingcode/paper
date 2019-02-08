using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Api.Extensions.Site
{
  public interface IRoute : IPropertyHolder
  {
    Href Href { get; }

    string Title { get; }
  }
}