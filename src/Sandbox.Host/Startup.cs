﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Host.Core;
using Sandbox.Lib;

namespace Sandbox.Host
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

      app.Map(ApiInfo.Prefix, nestApp => nestApp.UseMiddleware<SandboxMiddleware>());

      app.UseRewriter(new RewriteOptions()
        .AddRedirect(@"^$", $"{ApiInfo.Prefix}/Index", (int)HttpStatusCode.Found)
      );

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseDirectoryBrowser();
    }
  }
}