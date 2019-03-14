using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Paper.Api.Rendering;

namespace Paper.Core
{
  public class ObjectFactoryBuilder : IObjectFactoryBuilder
  {
    private IServiceCollection services;
    private ObjectFactory factory;

    public ObjectFactoryBuilder(IServiceCollection services)
    {
      this.services = services;
      this.factory = new ObjectFactory();
      this.factory.ServiceProvider = services.BuildServiceProvider();
    }

    public IObjectFactory ObjectFactory => factory;

    public IObjectFactoryBuilder AddSingleton(Type contract, object instance)
    {
      this.services.AddSingleton(contract, instance);
      this.factory.ServiceProvider = services.BuildServiceProvider();
      return this;
    }
  }
}
