using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Paper.Media.Rendering;

namespace Paper.Core
{
  public class Factory : IFactory
  {
    private IServiceProvider serviceProvider;

    public Factory(IServiceProvider provider)
    {
      this.serviceProvider = provider;
    }

    public object CreateObject(Type type, params object[] args)
    {
      return ActivatorUtilities.CreateInstance(serviceProvider, type, args);
    }
  }
}
