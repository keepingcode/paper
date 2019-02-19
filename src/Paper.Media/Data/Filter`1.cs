using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Data
{
  public class Filter<T> : Filter
  {
    public Filter()
    {
      this.AddFieldsFrom<T>();
    }
  }
}