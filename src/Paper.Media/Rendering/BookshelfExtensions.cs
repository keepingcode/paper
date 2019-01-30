using System;
using System.Collections.Generic;
using System.Text;
using Toolset;
using Toolset.Reflection;

namespace Paper.Media.Rendering
{
  public static class BookshelfExtensions
  {
    public static void AddExposedCatalogs(this Bookshelf bookshelf, IFactory factory)
    {
      FindCatalogFatories(bookshelf, factory);
      FindCatalogs(bookshelf, factory);
      FindPapers(bookshelf, factory);
    }

    private static void FindCatalogFatories(Bookshelf bookshelf, IFactory factory)
    {
      var types = ExposedTypes.GetTypes<ICatalogFatory>();
      foreach (var type in types)
      {
        try
        {
          var catalogFactory = (ICatalogFatory)factory.CreateObject(type, bookshelf);
          var catalog = catalogFactory.CreateCatalog();
          bookshelf.AddCatalog(catalog);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }

    private static void FindCatalogs(Bookshelf bookshelf, IFactory factory)
    {
      var types = ExposedTypes.GetTypes<Catalog>();
      foreach (var type in types)
      {
        try
        {
          var catalog = (Catalog)factory.CreateObject(type, bookshelf);
          bookshelf.AddCatalog(catalog);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }

    private static void FindPapers(Bookshelf bookshelf, IFactory factory)
    {
      var types = ExposedTypes.GetTypes<IPaper>();
      foreach (var type in types)
      {
        try
        {
          var paper = (IPaper)factory.CreateObject(type, bookshelf);
          var catalog = paper._GetAttribute<CatalogAttribute>();
          var catalogId = catalog?.Id ?? DeterministicGuid.GetGuid(type.Assembly.FullName);
          bookshelf.AddCatalog(catalogId.ToString(), paper);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }
  }
}