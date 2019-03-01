using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Toolset;

namespace Paper.Api.Extensions.Papers
{
  public static class PaperDescriptorExtensions
  {
    public static MethodInfo GetMethod(this PaperDescriptor paper, string methodName)
    {
      var method = methodName.EqualsIgnoreCase("Index")
        ? paper.IndexMethod
        : paper.Actions.FirstOrDefault(a => a.Name.EqualsIgnoreCase(methodName));
      return method;
    }
  }
}
