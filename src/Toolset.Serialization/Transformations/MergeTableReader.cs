using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class MergeTableReader : TransformReader
  {
    public MergeTableReader(Reader reader)
      : base(reader, new MergeTableTransform())
    {
    }

    public MergeTableReader(Reader reader, SerializationSettings settings)
      : base(reader, new MergeTableTransform(), settings)
    {
    }
  }
}