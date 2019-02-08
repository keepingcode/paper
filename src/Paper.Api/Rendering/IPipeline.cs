using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Api.Rendering
{
  public interface IPipeline
  {
    string Route { get; }

    Task RenderAsync(Request request, Response response, NextAsync next);
  }
}
