using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class ForkReader : TransformReader
  {
    public ForkReader(Reader reader, IEnumerable<Writer> writers)
      : base(reader, new ForkTransform(writers))
    {
    }

    public ForkReader(Reader reader, Writer writer, params Writer[] others)
      : base(reader, new ForkTransform(writer, others))
    {
    }
  }
}