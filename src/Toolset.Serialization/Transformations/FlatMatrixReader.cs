using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class FlatMatrixReader : TransformReader
  {
    public FlatMatrixReader(Reader reader)
      : base(reader, new FlatMatrixTransform())
    {
    }

    public FlatMatrixReader(Reader reader, Func<string, bool> fieldFilter)
      : base(reader, new FlatMatrixTransform(fieldFilter))
    {
    }

    public FlatMatrixReader(Reader reader, string[] fields)
      : base(reader, new FlatMatrixTransform(fields))
    {
    }

    public FlatMatrixReader(Reader reader, SerializationSettings settings)
      : base(reader, new FlatMatrixTransform(), settings)
    {
    }

    public FlatMatrixReader(Reader reader, SerializationSettings settings, Func<string, bool> fieldFilter)
      : base(reader, new FlatMatrixTransform(fieldFilter), settings)
    {
    }

    public FlatMatrixReader(Reader reader, SerializationSettings settings, string[] fields)
      : base(reader, new FlatMatrixTransform(fields), settings)
    {
    }

    public FlatMatrixReader(Reader reader, MatrixSettings settings)
      : base(reader, new FlatMatrixTransform(), settings)
    {
    }

    public FlatMatrixReader(Reader reader, MatrixSettings settings, Func<string, bool> fieldFilter)
      : base(reader, new FlatMatrixTransform(fieldFilter), settings)
    {
    }

    public FlatMatrixReader(Reader reader, MatrixSettings settings, string[] fields)
      : base(reader, new FlatMatrixTransform(fields), settings)
    {
    }
  }
}