using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class FlatTableReader : TransformReader
  {
    public FlatTableReader(Reader reader)
      : base(reader, new FlatTableTransform())
    {
    }

    public FlatTableReader(Reader reader, Func<string, bool> fieldFilter)
      : base(reader, new FlatTableTransform(fieldFilter))
    {
    }

    public FlatTableReader(Reader reader, string[] fields)
      : base(reader, new FlatTableTransform(fields))
    {
    }

    public FlatTableReader(Reader reader, SerializationSettings settings)
      : base(reader, new FlatTableTransform(), settings)
    {
    }

    public FlatTableReader(Reader reader, SerializationSettings settings, Func<string, bool> fieldFilter)
      : base(reader, new FlatTableTransform(fieldFilter), settings)
    {
    }

    public FlatTableReader(Reader reader, SerializationSettings settings, string[] fields)
      : base(reader, new FlatTableTransform(fields), settings)
    {
    }

    public FlatTableReader(Reader reader, MatrixSettings settings)
      : base(reader, new FlatTableTransform(), settings)
    {
    }

    public FlatTableReader(Reader reader, MatrixSettings settings, Func<string, bool> fieldFilter)
      : base(reader, new FlatTableTransform(fieldFilter), settings)
    {
    }

    public FlatTableReader(Reader reader, MatrixSettings settings, string[] fields)
      : base(reader, new FlatTableTransform(fields), settings)
    {
    }
  }
}