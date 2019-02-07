using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Api.Commons;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public class PaperCatalog : Catalog<IPaper>, IPaperCatalog
  {
    public PaperCatalog()
      : base(paper => paper.Route)
    {
    }
  }
}