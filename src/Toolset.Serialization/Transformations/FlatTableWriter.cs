using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class FlatTableWriter : TransformWriter
  {
    public FlatTableWriter(Writer writer)
      : base(writer, new FlatTableTransform())
    {
    }

    public FlatTableWriter(Writer writer, Func<string, bool> fieldFilter)
      : base(writer, new FlatTableTransform(fieldFilter))
    {
    }

    public FlatTableWriter(Writer writer, string[] fields)
      : base(writer, new FlatTableTransform(fields))
    {
    }

    public FlatTableWriter(Writer writer, SerializationSettings settings)
      : base(writer, new FlatTableTransform(), settings)
    {
    }

    public FlatTableWriter(Writer writer, SerializationSettings settings, Func<string, bool> fieldFilter)
      : base(writer, new FlatTableTransform(fieldFilter), settings)
    {
    }

    public FlatTableWriter(Writer writer, SerializationSettings settings, string[] fields)
      : base(writer, new FlatTableTransform(fields), settings)
    {
    }
  }
}