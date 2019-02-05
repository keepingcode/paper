using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class FlatMatrixWriter : TransformWriter
  {
    public FlatMatrixWriter(Writer writer)
      : base(writer, new FlatMatrixTransform())
    {
    }

    public FlatMatrixWriter(Writer writer, Func<string, bool> fieldFilter)
      : base(writer, new FlatMatrixTransform(fieldFilter))
    {
    }

    public FlatMatrixWriter(Writer writer, string[] fields)
      : base(writer, new FlatMatrixTransform(fields))
    {
    }

    public FlatMatrixWriter(Writer writer, SerializationSettings settings)
      : base(writer, new FlatMatrixTransform(), settings)
    {
    }

    public FlatMatrixWriter(Writer writer, SerializationSettings settings, Func<string, bool> fieldFilter)
      : base(writer, new FlatMatrixTransform(fieldFilter), settings)
    {
    }

    public FlatMatrixWriter(Writer writer, SerializationSettings settings, string[] fields)
      : base(writer, new FlatMatrixTransform(fields), settings)
    {
    }
  }
}