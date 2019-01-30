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
    First,
    Glossary,
    Help,
    Icon,
    Index,
    Item,
    Last,
    LatestVersion,
    License,
    Memento,
    Next,
    Original,
    Payment,
    Prev,
    Preview,
    Previous,
    Profile,
    Related,
    Replies,
    Search,
    Section,
    Self,
    Service,
    Start,
    Subsection,
    Tag,
    Type,
    Up,

    #endregion

    #region Personalizados

    Blueprint = 1000,

    Link,

    /// <summary>
    /// Relacionamento entre um link e um cabeçalho.
    /// </summary>
    HeaderLink,

    /// <summary>
    /// Estabecele relacionamento entre o alvo e uma proprieade da entidade.
    /// </summary>
    Property,

    /// <summary>
    /// Relacionamento entre o registro e a entidade pai.
    /// </summary>
    Row,

    /// <summary>
    /// Relacionamento entre o registro e a entidade cards.
    /// </summary>
    Card,

    /// <summary>
    /// Estabelece relacionamento entre o alvo e o dado.
    /// </summary>
    Data,

    /// <summary>
    /// Estabelece relacionamento entre o alvo e a tabela.
    /// </summary>
    Rows,

    /// <summary>
    /// Estabelece relacionamento entre o alvo e a lista de cartões.
    /// </summary>
    Cards,

    /// <summary>
    /// Relaciona um link a um dado.
    /// </summary>
    DataLink,

    /// <summary>
    /// Relaciona um link a um registro.
    /// </summary>
    RowLink,

    /// <summary>
    /// Representação de um link primário, numa coleção de links
    /// </summary>
    PrimaryLink,

    /// <summary>
    /// Representação de um link secundário, numa coleção de links
    /// </summary>
    SecondaryLink,

    /// <summary>
    /// Representação de um link terciário, numa coleção de links
    /// </summary>
    TertiaryLink,

    #endregion
  }
}