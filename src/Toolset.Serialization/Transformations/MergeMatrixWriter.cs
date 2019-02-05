using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class MergeMatrixWriter : TransformWriter
  {
    public MergeMatrixWriter(Writer writer)
      : base(writer, new MergeMatrixTransform())
    {
    }

    public MergeMatrixWriter(Writer writer, SerializationSettings settings)
      : base(writer, new MergeMatrixTransform(), settings)
    {
    }
  }
}