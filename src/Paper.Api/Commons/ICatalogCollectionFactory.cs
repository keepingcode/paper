using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Commons
{
  public interface ICatalogCollectionFactory<T>
    where T : class
  {
    ICatalogCollection<T> CreateCollection();
  }
}