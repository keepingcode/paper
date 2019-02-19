using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Api.Extensions.Site.Rendering
{
  public class MediaMatcher
  {
    public string Path { get; set; }

    public Modifier Modifier { get; set; }

    public NameCollection Class { get; set; }

    public NameCollection Rel { get; set; }

    public List<string> IncludedCatalogs { get; set; }

    public List<string> ExcludedCatalogs { get; set; }
  }
}