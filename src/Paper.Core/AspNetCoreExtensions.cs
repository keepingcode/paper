using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static System.Environment;
using Paper.Api.Rendering;
using Microsoft.AspNetCore.Rewrite;
using System.Net;
using Paper.Api.Extensions.Site;
using Paper.Api.Extensions.Papers;

namespace Paper.Core
{
  public static class AspNetCoreExtensions
  {
    #region IWebHostBuilder

    public static IWebHostBuilder UsePaper(this IWebHostBuilder builder, params string[] urls)
    {
      return builder.UseUrls(urls);
    }

    #endregion

    #region IServiceCollection

    public static IServiceCollection AddPaperServices(this IServiceCollection services)
    {
      var factory = new ObjectFactory();
      var pipelineCatalog = new PipelineCatalog();
      var siteMapCatalog = new SiteMapCatalog();
      var paperCatalog = new PaperCatalog();

      services.AddSingleton<IObjectFactory>(factory);
      services.AddSingleton<IPipelineCatalog>(pipelineCatalog);
      services.AddSingleton<ISiteMapCatalog>(siteMapCatalog);
      services.AddSingleton<IPaperCatalog>(paperCatalog);

      factory.ServiceProvider = services.BuildServiceProvider();

      pipelineCatalog.ImportExposedCollections(factory);
      siteMapCatalog.ImportExposedCollections(factory);
      paperCatalog.ImportExposedCollections(factory);

      return services;
    }

    #endregion

    #region IApplicationBuilder

    public static IApplicationBuilder UsePaperApi(this IApplicationBuilder app)
    {
      return UsePaperApi(app, null);
    }

    public static IApplicationBuilder UsePaperApi(this IApplicationBuilder app, PathString prefix)
    {
      return app
        .Map($"{prefix}/Api/1", chain => chain
          .UseRewriter(new RewriteOptions().AddRedirect("^$", "/Catalog"))
          .UseMiddleware<Middleware>()
        );
    }

    #endregion
  }
}