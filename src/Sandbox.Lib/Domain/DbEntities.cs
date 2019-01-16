using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;

namespace Sandbox.Lib.Domain
{
  static class DbEntities
  {
    public static Ret CopyFilter(UriString uri, object targetFilter)
    {
      throw new NotImplementedException();
      foreach (var argName in uri.GetArgNames())
      {
        if (targetFilter._Has(argName))
        {
          var value = uri._Get(argName);
          targetFilter._Set(argName, value);
        }
      }
    }

    public static Ret CopyFilter(object filter, UriString targetUri)
    {
      throw new NotImplementedException();
    }

    public static Ret CopyGraph(object graph, Entity targetEntity)
    {
      throw new NotImplementedException();
    }

    public static Ret CopyGraph(Entity entity, object targetGraph)
    {
      throw new NotImplementedException();
    }
  }
}
