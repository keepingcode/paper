using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Toolset.Collections;

namespace Paper.Media
{
  /// <summary>
  /// Coleção de entidades.
  /// </summary>
  [CollectionDataContract(Namespace = Namespaces.Default, Name = "Entities")]
  [XmlType]
  public class EntityCollection : Collection<Entity>
  {
    public EntityCollection()
    {
    }

    public EntityCollection(IEnumerable<Entity> items)
    : base(items)
    {
    }

    [XmlAttribute]
    public bool IsArray { get; set; } = true;
  }
}