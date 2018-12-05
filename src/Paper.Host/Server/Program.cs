using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
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
        var builder = WebHost.CreateDefaultBuilder(args)
          .UseInkeeper()
          .UsePaperSettings()
          .UseStartup<Startup>();

        builder.Build().Run();
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}