using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class TableWriter : TransformWriter
  {
    public TableWriter(Writer writer)
      : base(writer, new TableTransform())
    {
    }

    public TableWriter(Writer writer, SerializationSettings settings)
      : base(writer, new TableTransform(), settings)
    {
    }
  }
}