using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Commons;
using Paper.Api.Rendering;

namespace Paper.Api.Extensions.Papers
{
  public class PaperCatalog : Catalog<PaperDescriptor>, IPaperCatalog
  {
    public PaperCatalog()
      : base(descriptor => descriptor.PathTemplate)
    {
    }

    public override void ImportExposedCollections(IObjectFactory factory)
    {
      base.ImportExposedCollections<IPaper>(factory, paper => new PaperDescriptor(paper));
    }
  }
}