using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public class ChainReader : TransformReader
  {
    public ChainReader(Reader reader, IEnumerable<ITransform> transforms)
      : base(reader, new ChainTransform(transforms))
    {
    }

    public ChainReader(Reader reader, ITransform transform, params ITransform[] others)
      : base(reader, new ChainTransform(transform, others))
    {
    }
  }
}