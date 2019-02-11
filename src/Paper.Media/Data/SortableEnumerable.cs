﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Data
{
  internal class SortableEnumerable<T> : ISortableEnumerable<T>
  {
    public IEnumerable<T> Source { get; }

    public SortableEnumerable(IEnumerable<T> source)
    {
      this.Source = source;
    }

    public IEnumerator<T> GetEnumerator()
    {
      return Source.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Source.GetEnumerator();
    }
  }
}
