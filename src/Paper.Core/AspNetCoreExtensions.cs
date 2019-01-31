﻿using Microsoft.AspNetCore.Builder;
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
      var bookshelf = new Bookshelf();
      services.AddSingleton(bookshelf);
      bookshelf.AddExposedCatalogs(new Factory(services.BuildServiceProvider()));
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
          .UseRewriter(new RewriteOptions().AddRedirect("^$", "/Bookshelf"))
          .UseMiddleware<Middleware>()
        );
    }

    #endregion
  }
}