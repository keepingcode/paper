using System;
using System.Collections.Generic;
using System.Diagnostics;
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



      Debug.WriteLine("");
      Console.WriteLine("");
      Debug.WriteLine("-----");
      Console.WriteLine("-----");
      Debug.WriteLine("Rotas:");
      Console.WriteLine("Rotas:");
      foreach (var path in this.GetPaths())
      {
        Debug.WriteLine(path);
        Console.WriteLine(path);
      }
      Debug.WriteLine("-----");
      Console.WriteLine("-----");
      Debug.WriteLine("");
      Console.WriteLine("");
    }
  }
}