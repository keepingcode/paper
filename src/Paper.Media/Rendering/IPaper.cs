using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Media.Rendering
{
  public interface IPaper
  {
    string Route { get; }

    Task RenderAsync(RenderingContext context, NextAsync next);
  }
}
