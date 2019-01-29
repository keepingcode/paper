using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media
{
  public class Href
  {
    public string Value { get; private set; }

    public override string ToString()
    {
      return Value;
    }

    public static implicit operator string(Href href)
    {
      return href.Value;
    }

    public static implicit operator Uri(Href href)
    {
      return new Uri(href.Value, UriKind.RelativeOrAbsolute);
    }

    public static implicit operator Href(string href)
    {
      return new Href { Value = href };
    }

    public static implicit operator Href(Uri href)
    {
      return new Href { Value = href.ToString() };
    }
  }
}
