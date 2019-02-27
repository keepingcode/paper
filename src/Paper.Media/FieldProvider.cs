using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Paper.Media
{
  /// <summary>
  /// Provedor de dados de um campo.
  /// </summary>
  [DataContract(Namespace = Namespaces.Default)]
  public class FieldProvider : IMediaObject, IHyperLink
  {
    /// <summary>
    /// Título do provedor de dados.
    /// </summary>
    [DataMember(EmitDefaultValue = false, Order = 10)]
    public string Title { get; set; }

    /// <summary>
    /// Classe do provedor de dados.
    /// </summary>
    [DataMember(EmitDefaultValue = false, Order = 20)]
    public NameCollection Class { get; set; }

    /// <summary>
    /// Relacionamentos entre o provedor de dados e o campo ou entidade.
    /// </summary>
    [DataMember(EmitDefaultValue = false, Order = 30)]
    public NameCollection Rel { get; set; }

    /// <summary>
    /// URL de referência do provedor de dados.
    /// </summary>
    [DataMember(EmitDefaultValue = false, Order = 40)]
    public Href Href { get; set; }

    /// <summary>
    /// Nome das chaves de relacionamento entre o dado e
    /// o campo.
    /// </summary>
    [DataMember(EmitDefaultValue = false, Order = 50)]
    [CaseVariantString]
    public NameCollection Keys { get; set; }
  }
}
