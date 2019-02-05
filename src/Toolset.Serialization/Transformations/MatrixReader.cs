using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class MatrixReader : TransformReader
  {
    public MatrixReader(Reader reader)
      : base(reader, new MatrixTransform())
    {
    }

    public MatrixReader(Reader reader, SerializationSettings settings)
      : base(reader, new MatrixTransform(), settings)
    {
    }
  }
}