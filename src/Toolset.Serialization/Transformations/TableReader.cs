using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class TableReader : TransformReader
  {
    public TableReader(Reader reader)
      : base(reader, new TableTransform())
    {
    }

    public TableReader(Reader reader, SerializationSettings settings)
      : base(reader, new TableTransform(), settings)
    {
    }
  }
}