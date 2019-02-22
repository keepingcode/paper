#if !NETCOREAPP
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore
{
  /// <summary>
  /// Implementação de WebHost.CreateDefaultBuilder para Framework.Net 4+.
  /// O DotNetCore2+ possui uma implementação própria desse método.
  /// 
  /// Referências:
  /// - https://joonasw.net/view/aspnet-core-2-configuration-changes
  /// - https://blog.dudak.me/2017/what-does-webhost-createdefaultbuilder-do/
  /// </summary>
  public static class WebHost
  {
    public static IWebHostBuilder CreateDefaultBuilder(string[] args)
    {
      var builder = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
          var env = hostingContext.HostingEnvironment;

          config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

          if (env.IsDevelopment())
          {
            var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
            if (appAssembly != null)
            {
              config.AddUserSecrets(appAssembly, optional: true);
            }
          }

          config.AddEnvironmentVariables();

          if (args != null)
          {
            config.AddCommandLine(args);
          }
        })
        // Nao suportado no DotNet 4.6+ ?
        //.ConfigureLogging(builder, (hostingContext, logging) =>
        //{
        //  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        //  logging.AddConsole();
        //  logging.AddDebug();
        //})
        .UseIISIntegration();

      if (args != null)
      {
        builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());
      }

      return builder;
    }
  }
}
#endif