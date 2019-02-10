using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Design
{
  /// <summary>
  /// Construtor de uma entidade que representa um cabeçalho.
  /// </summary>
  public class HeaderBuilder
  {
    internal HeaderBuilder(Entity entity)
    {
      this.HeaderEntity = entity;
    }

    private Entity HeaderEntity { get; }

    /// <summary>
    /// Nome da coluna.
    /// </summary>
    public string Name
    {
      get => HeaderEntity.Properties?["Name"]?.Value as string;
      set => HeaderEntity.AddProperty("Name", value);
    }

    /// <summary>
    /// Adiciona o título do cabeçalho.
    /// </summary>
    /// <param name="builder">O construtor do cabeçalho.</param>
    /// <param name="title">O valor do campo.</param>
    /// <returns>A própria instância do construtor do cabeçalho.</returns>
    public HeaderBuilder AddTitle(string title)
    {
      HeaderEntity.AddProperty("Title", title);
      return this;
    }

    /// <summary>
    /// Adiciona o tipo de dado de um campo.
    /// </summary>
    /// <param name="builder">O construtor do cabeçalho.</param>
    /// <param name="dataType">O valor do campo.</param>
    /// <returns>A própria instância do construtor do cabeçalho.</returns>
    public HeaderBuilder AddDataType(string dataType)
    {
      HeaderEntity.AddProperty("DataType", dataType);
      return this;
    }

    /// <summary>
    /// Adiciona o tipo de dado de um campo.
    /// </summary>
    /// <param name="builder">O construtor do cabeçalho.</param>
    /// <param name="type">O tipo do campo.</param>
    /// <returns>A própria instância do construtor do cabeçalho.</returns>
    public HeaderBuilder AddDataType(Type type)
    {
      var dataType = DataTypeNames.GetDataTypeName(type);
      HeaderEntity.AddProperty("DataType", dataType);
      return this;
    }

    /// <summary>
    /// Marca ou desmarca um campo como visível.
    /// </summary>
    /// <param name="builder">O construtor do cabeçalho.</param>
    /// <param name="hidden">O valor do campo.</param>
    /// <returns>A própria instância do construtor do cabeçalho.</returns>
    public HeaderBuilder AddHidden(bool hidden = true)
    {
      if (hidden)
      {
        HeaderEntity.AddProperty("Hidden", hidden);
      }
      else
      {
        HeaderEntity.Properties?.Remove("Hidden");
      }
      return this;
    }
  }
}