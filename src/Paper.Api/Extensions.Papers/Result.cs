using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Api.Extensions.Papers
{
  internal struct Result
  {
    public Type ValueType { get; set; }
    public object Value { get; set; }
  }
}