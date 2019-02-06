using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Reflection;

namespace Paper.Api.Rendering
{
  public static class BookshelfExtensions
  {
    public static void AddExposedPaperCollections(this Bookshelf bookshelf, IFactory factory)
    {
      FindPaperCollectionFatories(bookshelf, factory);
      FindPapereCollection(bookshelf, factory);
      FindPapers(bookshelf, factory);
    }

    private static void FindPaperCollectionFatories(Bookshelf bookshelf, IFactory factory)
    {
      var types = ExposedTypes.GetTypes<IPaperCollectionFatory>();
      foreach (var type in types)
      {
        try
        {
          var collectionFactory = (IPaperCollectionFatory)factory.CreateObject(type, bookshelf);
          var collection = collectionFactory.CreateCollection();
          bookshelf.AddCollection(collection);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }

    private static void FindPapereCollection(Bookshelf bookshelf, IFactory factory)
    {
      var types = ExposedTypes.GetTypes<PaperCollection>();
      foreach (var type in types)
      {
        try
        {
          var collection = (PaperCollection)factory.CreateObject(type, bookshelf);
          bookshelf.AddCollection(collection);
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
          var paper = (IPaper)factory.CreateObject(type);
          var collection = paper._GetAttribute<PaperCollectionAttribute>();
          var collectionName = collection?.Name ?? type.Assembly.FullName.Split(',').First();
          bookshelf.AddCollection(collectionName, paper);
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }
  }
}