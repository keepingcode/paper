using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media.Design;

namespace Paper.Media.Routing
{
  /// <summary>
  /// Contexto de rederização de rota do Paper.
  /// </summary>
  public interface IContext
  {
    ///// <summary>
    ///// Instância do Paper em renderização.
    ///// O Paper contém as especificações para renderização do <see cref="Entity"/>
    ///// que reprsentação a interface de usuário.
    ///// </summary>
    //IPaper Paper { get; }

    /// <summary>
    /// Argumentos do Paper extraídos da URI de requisição.
    /// </summary>
    IArgs RequestArgs { get; }

    RequestUri RequestUri { get; }

    /// <summary>
    /// Provedor de objetos do Paper.
    /// </summary>
    IProvider Provider { get; }

    /// <summary>
    /// Catálogo dos Papers conhecidos.
    /// Cada Paper contém instruções de uma rota específica.
    /// </summary>
    ICatalog Catalog { get; }

    /// <summary>
    /// Instância da fábrica de objetos pela injeção de dependência.
    /// </summary>
    IObjectFactory Factory { get; }

    /// <summary>
    /// Cache de objetos durante uma requisição.
    /// </summary>
    ICache Cache { get; }
  }
}