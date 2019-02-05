using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public class ChainWriter : TransformWriter
  {
    public ChainWriter(Writer writer, IEnumerable<ITransform> transforms)
      : base(writer, new ChainTransform(transforms))
    {
    }

    public ChainWriter(Writer writer, ITransform transform, params ITransform[] others)
      : base(writer, new ChainTransform(transform, others))
    {
    }
  }
}
