using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Paper.Api.Rendering;

namespace Paper.Core
{
  public class ObjectFactory : IObjectFactory
  {
    public IServiceProvider ServiceProvider { get; set; }

    public object CreateObject(Type type, params object[] args)
    {
      return ActivatorUtilities.CreateInstance(ServiceProvider, type, args);
    }

    public object GetInstance(Type type)
    {
      return ServiceProvider.GetService(type);
    }
  }
}