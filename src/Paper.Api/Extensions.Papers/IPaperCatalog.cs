using System;
using System.Collections.Generic;
using System.Text;
using Paper.Api.Commons;

namespace Paper.Api.Extensions.Papers
{
  public interface IPaperCatalog : ICatalog<PaperDescriptor>
  {
    //PaperDescriptor FindByType(Type type);
    //PaperDescriptor FindByType<T>();
  }
}