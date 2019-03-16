using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Attributes
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class ProviderAttribute : Attribute
  {
    public ProviderAttribute()
    {
    }

    public ProviderAttribute(string href)
    {
      this.Href = href;
    }

    /// <summary>
    /// Título do provedor de dados.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Classe do provedor de dados.
    /// </summary>
    public string Class { get; set; }

    /// <summary>
    /// Relacionamentos entre o provedor de dados e o campo ou entidade.
    /// </summary>
    public string Rel { get; set; }

    /// <summary>
    /// URL de referência do provedor de dados.
    /// </summary>
    public string Href { get; set; }

    /// <summary>
    /// Nome das chaves de relacionamento entre o dado e
    /// o campo.
    /// </summary>
    public string[] Keys { get; set; }
  }
}
