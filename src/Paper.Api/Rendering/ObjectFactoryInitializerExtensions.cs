using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Extensions.Papers;
using Paper.Api.Extensions.Site;

namespace Paper.Api.Rendering
{
  public static class ObjectFactoryBuilderExtensions
  {
    public static IObjectFactoryBuilder AddSingleton<TContract>(this IObjectFactoryBuilder builder)
      where TContract : new()
    {
      builder.AddSingleton(typeof(TContract), new TContract());
      return builder;
    }

    public static IObjectFactoryBuilder AddSingleton<TContract>(this IObjectFactoryBuilder builder, TContract instance)
    {
      builder.AddSingleton(typeof(TContract), instance);
      return builder;
    }

    public static IObjectFactoryBuilder AddPaperObjects(this IObjectFactoryBuilder builder)
    {
      var factory = builder.ObjectFactory;

      var pipelineCatalog = new PipelineCatalog();
      var siteMapCatalog = new SiteMapCatalog();
      var paperCatalog = new PaperCatalog();

      builder.AddSingleton<IObjectFactory>(factory);
      builder.AddSingleton<IPipelineCatalog>(pipelineCatalog);
      builder.AddSingleton<ISiteMapCatalog>(siteMapCatalog);
      builder.AddSingleton<IPaperCatalog>(paperCatalog);

      pipelineCatalog.ImportExposedCollections(factory);
      siteMapCatalog.ImportExposedCollections(factory);
      paperCatalog.ImportExposedCollections(factory);

      return builder;
    }
  }
}
