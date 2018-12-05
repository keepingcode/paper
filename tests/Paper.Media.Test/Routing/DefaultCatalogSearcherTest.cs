using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Toolset.Collections;
using Xunit;

namespace Paper.Media.Routing
{
  public class DefaultCatalogSearcherTest
  {
    [Fact]
    public void SearchCatalog_NaRaiz_DeveRetornarOCatalogoLocal()
    {
      // Given
      var searcher = new DefaultCatalogSearcher();
      // When
      var catalog = searcher.SearchCatalog("/");
      // Then
      var expected = typeof(ExposedCatalog);
      var obtained = catalog?.GetType();
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void SearchCatalog_NumCaminhoConhecido_DeveRetornarOCatalogo()
    {
      // Given
      var mock = new Mock<ICatalog>();
      var searcher = new DefaultCatalogSearcher();
      searcher.AddCatalog("/My/Site", mock.Object);
      // When
      var catalog = searcher.SearchCatalog("/My/Site");
      // Then
      var expected = mock.Object;
      var obtained = catalog;
      Assert.Equal(expected, obtained);
    }

    [Fact]
    public void SearchCatalog_NumCaminhoDesconhecido_DeveRetornarNulo()
    {
      // Given
      var mock = new Mock<ICatalog>();
      var searcher = new DefaultCatalogSearcher();
      searcher.AddCatalog("/My/Site", mock.Object);
      // When
      var catalog = searcher.SearchCatalog("/My/Site/Other");
      // Then
      Assert.Null(catalog);
    }
  }
}