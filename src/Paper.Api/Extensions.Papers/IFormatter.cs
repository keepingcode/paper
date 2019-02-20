
using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Rendering;
using Paper.Media;

namespace Paper.Api.Extensions.Papers
{
  public interface IFormatter
  {
    void Format(IPaperContext context, IObjectFactory factory, Entity entity);
  }
}