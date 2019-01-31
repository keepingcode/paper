using System;
using System.Collections.Generic;
using System.Text;

namespace Toolset
{
  public static class Default
  {
    public static object Of(Type type)
    {
      return type.IsValueType ? Activator.CreateInstance(type) : null;
    }
  }
}
