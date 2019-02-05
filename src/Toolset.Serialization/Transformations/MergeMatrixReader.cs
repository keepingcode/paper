using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class MergeMatrixReader : TransformReader
  {
    public MergeMatrixReader(Reader reader)
      : base(reader, new MergeMatrixTransform())
    {
    }

    public MergeMatrixReader(Reader reader, SerializationSettings settings)
      : base(reader, new MergeMatrixTransform(), settings)
    {
    }
  }
}