using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class MatrixWriter : TransformWriter
  {
    public MatrixWriter(Writer writer)
      : base(writer, new MatrixTransform())
    {
    }

    public MatrixWriter(Writer writer, SerializationSettings settings)
      : base(writer, new MatrixTransform(), settings)
    {
    }
  }
}