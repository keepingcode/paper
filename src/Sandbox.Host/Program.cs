using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Sandbox.Host.Core;
using Toolset;

namespace Sandbox.Host
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        var exePath = typeof(Program).Assembly.Location;
        var contentPath = Path.GetDirectoryName(exePath);

        WebHost.CreateDefaultBuilder(args)
          // .UsePaperHost("http://localhost:8080/")
          // .UsePaperSettings(opt => opt
          //   .UsePathBase("/Mlogic")
          //   .UseRemotePaperServer("http://localhost/Mlogic/Beta")
          // )
          .UseUrls("http://localhost:8080/")
          .UseContentRoot(contentPath)
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