using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media.Data;

namespace Paper.Media.Design
{
  public interface IHeaderInfo
  {
    string Name { get; }

    string Title { get; }

    string DataType { get; }

    bool Hidden { get; }

    SortOrder? Order { get; }
  }
}
