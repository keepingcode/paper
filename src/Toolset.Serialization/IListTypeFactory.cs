using System;
using System.Collections.Generic;
using System.Text;

namespace Toolset.Serialization
{
  public interface IListTypeFactory
  {
    /// <summary>
    /// Método de fábricação da lista de opções para uso durante a deserialização
    /// via Toolset.Serialization.
    /// 
    /// A propriedade é usada pela classe Toolset.Serialization.Graph.GraphWriter
    /// durante a deserialização de opções.
    /// </summary>
    Type CreateListType(string property);
  }
}
