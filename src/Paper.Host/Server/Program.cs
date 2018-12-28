using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Paper.Core;
using Toolset;

namespace Paper.Host.Server
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        WebHost.CreateDefaultBuilder(args)
          .UseUrls("http://localhost:8080/", "http://192.168.15.8:8080/")
          //.UsePaperWebAppSettings()
          .UsePaperSettings()
          .UseStartup<Startup>()
          .Build()
          .Run();
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}