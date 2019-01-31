using System;
using System.Collections.Generic;
using System.Text;

namespace Toolset.Serialization.Json
{
  public enum Style
  {
    /// <summary>
    /// Constrói um objeto sem elemento raiz.
    /// Exemplo:
    /// {
    ///   "prop1": "value1",
    ///   "prop2": "value2"
    /// }
    /// </summary>
    Flat,

    /// <summary>
    /// Constrói um objeto com um elemento raiz representando o tipo do objeto.
    /// Exemplo:
    /// {
    ///   "type1": {
    ///     "prop1": "value1",
    ///     "prop2": "value2"
    ///   }
    /// }
    /// </summary>
    Typed
  }
}
