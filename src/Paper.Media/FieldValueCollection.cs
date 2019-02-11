using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Toolset.Collections;

namespace Paper.Media
{
  /// <summary>
  /// Coleção de valores para campos multi-valorados.
  /// </summary>
  [CollectionDataContract(Namespace = Namespaces.Default, Name = "Values")]
  public class FieldValueCollection : Collection<FieldValue>
  {
    public FieldValueCollection()
    {
    }

    public FieldValueCollection(IEnumerable<FieldValue> items)
    : base(items)
    {
    }
  }
}