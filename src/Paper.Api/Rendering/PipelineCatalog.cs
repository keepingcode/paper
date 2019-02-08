using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Api.Commons;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public class PipelineCatalog : Catalog<IPipeline>, IPipelineCatalog
  {
    public PipelineCatalog()
      : base(item => item.Route)
    {
    }
  }
}