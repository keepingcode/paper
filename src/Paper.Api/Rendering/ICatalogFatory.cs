﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  public interface ICatalogFatory
  {
    Catalog CreateCatalog();
  }
}