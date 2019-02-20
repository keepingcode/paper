using System;
using System.Linq;
using System.Collections.Generic;

namespace Paper.Media
{
  /// <summary>
  /// Classes conhecidas de entidades.
  /// </summary>
  public enum Class
  {
    #region Estruturais básicas

    /// <summary>
    /// Nome de classe que representa um registro.
    /// </summary>
    Record,

    /// <summary>
    /// Nome de classe que representa dados de um formulário de edição.
    /// </summary>
    Form,

    /// <summary>
    /// Nome de classe para ume entidade que se comporta como um cabeçalho.
    /// </summary>
    Header,

    /// <summary>
    /// Nome de classe para uma entidade que se comporta como status de execução.
    /// </summary>
    Status,

    /// <summary>
    /// Nome de classe para uma entidade que se comporta como erro.
    /// </summary>
    Error,

    /// <summary>
    /// Nome de classe para uma ação que se comporta como filtro de lista.
    /// </summary>
    Filter,

    /// <summary>
    /// Nome de classe para uma ação, entidade ou link que se comporta como um hiperlink.
    /// </summary>
    Hyperlink,

    /// <summary>
    /// Nome de classe para uma entidade que transporta um valor literal apenas, como um 
    /// texto, número, etc.
    /// </summary>
    Literal,

    /// <summary>
    /// Classe de uma entidade que representa um propriedade ou coluna de dados.
    /// </summary>
    Field,

    #endregion

    #region Estruturais avançadas

    /// <summary>
    /// Nome de classe para ume entidade que representa a configuração de um site.
    /// </summary>
    Blueprint,

    /// <summary>
    /// Nome de classe para ume entidade que se comporta como uma coleção de registros.
    /// </summary>
    Table,

    /// <summary>
    /// Nome de classe para uma entidade que se comporta como lista.
    /// </summary>
    List,

    /// <summary>
    /// Nome de classe para uma entidade que representa um item de lista.
    /// </summary>
    Item,

    /// <summary>
    /// Nome de classe para ume entidade que se comporta como uma coleção de cards.
    /// </summary>
    Cards,

    /// <summary>
    /// Nome de classe para ume entidade que se comporta como um registro de coleção de cards.
    /// </summary>
    Card

    #endregion

    #region Extensões

    #endregion
  }
}