﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Design;
using Toolset;

namespace Paper.Api.Papers
{
  [Expose]
  public class SandboxPaper : IPaper
  {
    public string Route { get; } = "/Sandbox";

    public async Task RenderAsync(RenderingContext context, NextAsync next)
    {
      var req = context.Request;
      var res = context.Response;

      var entity = await req.ReadEntityAsync();

      await res.WriteEntityAsync(entity);
    }
  }
}
