using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Toolset.Collections;
using Toolset.Reflection;

namespace Toolset.Serialization.Graph
{
  public class GraphWriter<T> : GraphWriter
    where T : class, new()
  {
    public GraphWriter()
      : base(typeof(T))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public GraphWriter(SerializationSettings settings)
      : base(typeof(T), settings)
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public GraphWriter(IEnumerable<Type> knownTypes, SerializationSettings settings)
      : base(typeof(T), knownTypes, settings)
    {
    }

    public new ICollection<T> Graphs => (ICollection<T>)base.Graphs;
  }
}
