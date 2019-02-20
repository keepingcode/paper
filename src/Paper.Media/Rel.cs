using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Media
{
  public enum Rel
  {
    #region Convencionados

    About = 1,
    Alternate,
    Appendix,
    Archives,
    Author,
    Bookmark,
    Chapter,
    Collection,
    Contents,
    Copyright,
    Current,
    Describes,
    Edit,
    EditForm,

    /// <summary>
    /// Relaciona um elemento como primeiro termo ou uma ação de navegação ao primeiro termo.
    /// </summary>
    First,

    Glossary,
    Help,

    /// <summary>
    /// Relaciona um elemento como um ícone.
    /// </summary>
    Icon,

    Index,

    /// <summary>
    /// Relaciona uma entidade como item de uma coleção.
    /// Como uma linha de uma tabela, um cartão em uma lista de cartões ou um item em uma lista.
    /// </summary>
    Item,

    /// <summary>
    /// Relaciona um elemento como último termo ou uma ação de navegação ao último termo.
    /// </summary>
    Last,

    LatestVersion,
    License,
    Memento,

    /// <summary>
    /// Relaciona uma ação de avanço ou navegação para frente.
    /// </summary>
    Next,

    Original,
    Payment,

    /// <summary>
    /// Relaciona uma ação de retorno ou navegação para trás.
    /// </summary>
    Prev,

    Preview,
    Previous,
    Profile,
    Related,
    Replies,
    Search,
    Section,

    /// <summary>
    /// Relaciona um objeto a ele mesmo ou uma característica ao seu objeto.
    /// </summary>
    Self,

    Service,
    Start,
    Subsection,
    Tag,
    Type,
    Up,

    #endregion

    #region Estruturais básicos

    /// <summary>
    /// Relaciona um elemento como link em um objeto.
    /// </summary>
    Link = 1000,

    /// <summary>
    /// Relaciona um elemento como primário em seu grupo de elementos.
    /// </summary>
    Primary,

    /// <summary>
    /// Relaciona um elemento como secundário em seu grupo de elementos.
    /// </summary>
    Secondary,

    /// <summary>
    /// Relaciona um elemento como terciário em seu grupo de elementos.
    /// </summary>
    Tertiary,

    /// <summary>
    /// Relaciona um elemento de ação ou dá um significado de ação a um link.
    /// </summary>
    Action,

    #endregion

    #region Estruturais avançados

    #endregion

    #region Extensões

    #endregion
  }
}