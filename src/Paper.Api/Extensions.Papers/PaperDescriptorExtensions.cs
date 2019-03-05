using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Paper.Api.Rendering;
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

    public static object Create(this PaperDescriptor paper, IObjectFactory factory, Type type)
    {
      var creator = paper.Factories.FirstOrDefault(x => type.IsAssignableFrom(x.ReturnType));
      var instance = creator?.Invoke(paper.Paper, null)
                  ?? factory.CreateObject(type);
      return instance;
    }
  }
}
