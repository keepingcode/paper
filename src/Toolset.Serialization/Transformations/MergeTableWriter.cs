﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolset.Serialization.Transformations
{
  public sealed class MergeTableWriter : TransformWriter
  {
    public MergeTableWriter(Writer writer)
      : base(writer, new MergeTableTransform())
    {
    }

    public MergeTableWriter(Writer writer, SerializationSettings settings)
      : base(writer, new MergeTableTransform(), settings)
    {
    }
  }
}