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
using System.Runtime.InteropServices;
using Paper.Media.Routing;

namespace Paper.Core
{
  public static class AspNetCoreExtensions
  {
    #region IWebHostBuilder

    public static IWebHostBuilder UseInkeeper(this IWebHostBuilder builder)
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        builder.UseHttpSys(options =>
        {
          options.UrlPrefixes.Add("http://localhost:90");
        });
      }
      return builder;
    }

    public static IWebHostBuilder UsePaperSettings(this IWebHostBuilder builder)
    {
      return UsePaperSettings(builder, opt => { });
    }

    public static IWebHostBuilder UsePaperSettings(this IWebHostBuilder builder, Action<PaperSettingsBuilder> action)
    {
      var settingsBuilder = new PaperSettingsBuilder();

      action.Invoke(settingsBuilder);

      var settings = (PaperSettings)settingsBuilder.Build();

      var baseUri = builder.GetSetting(WebHostDefaults.ServerUrlsKey);
      settings.BaseUri = (baseUri != null) ? new Uri(baseUri) : null;

      builder.ConfigureServices(services =>
        services.AddSingleton<IPaperSettings>(settings)
      );

      var exePath = typeof(AspNetCoreExtensions).Assembly.Location;
      var contentPath = Path.GetDirectoryName(exePath);
      builder.UseContentRoot(contentPath);

      return builder;
    }

    #endregion

    #region IServiceCollection

    public static IServiceCollection AddPaperServices(this IServiceCollection services)
    {
      IPaperRouter router = new PaperRouter();
      services.AddSingleton(router);

      //var serviceProvider = services.BuildServiceProvider();
      //var settings = serviceProvider.GetService<IPaperSettings>();
      //if (settings?.RemotePaperServerUris?.Any() == true)
      //{
      //  //services.AddHostedService<TimedProxyNotificationService>();
      //}

      PrintInfo(router);

      return services;
    }

    private static void PrintInfo(IPaperRouter router)
    {
      foreach (var route in router.Catalog.Routes)
      {
        var blueprint = router.Catalog.GetPaperBlueprint(route);
        Console.WriteLine($"{blueprint.UriTemplate} => {blueprint.PaperType.FullName}");
      }
    }

    #endregion

    #region IApplicationBuilder

    public static IApplicationBuilder UsePaper(this IApplicationBuilder app)
    {
      app = UsePaperMiddlewares(app);
      return app;
    }

    public static IApplicationBuilder UsePaperStaticFiles(this IApplicationBuilder app)
    {
      app.UsePaperMiddlewares();
      app.UseDefaultFiles();
      app.UseStaticFiles();
      return app;
    }

    public static IApplicationBuilder UsePaperMiddlewares(this IApplicationBuilder app)
    {
      var settings = app.ApplicationServices.GetService<IPaperSettings>();
      var pathBase = settings?.PathBase;
      if (pathBase != null)
      {
        app.UsePathBase(pathBase.ToString());
      }
      app.Map("/Api/1", PaperPipeline);
      return app;
    }

    private static void PaperPipeline(IApplicationBuilder app)
    {
      app.UseMiddleware<PaperMiddleware>();
    }

    #endregion
  }
}