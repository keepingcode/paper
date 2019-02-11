using System;
using System.Collections.Generic;
using System.Text;
using Toolset.Collections;

namespace Paper.Media
{
  public class PropertyValueCollection : Collection<object>
  {
    public PropertyValueCollection()
    {
    }

    public PropertyValueCollection(IEnumerable<object> collection)
      : base(collection)
    {
    }

    public PropertyValueCollection(int capacity)
      : base(capacity)
    {
    }
  }
}
