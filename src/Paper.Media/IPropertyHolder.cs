using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media
{
  public interface IPropertyHolder
  {
    PropertyCollection Properties { get; set; }
  }
}