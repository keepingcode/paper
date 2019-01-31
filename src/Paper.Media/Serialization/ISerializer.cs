using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Media.Serialization
{
  public interface ISerializer
  {
    /// <summary>
    /// Serializa a entidade para a saída indicada.
    /// </summary>
    /// <param name="entity">Entidade a ser serializada.</param>
    /// <param name="encoding">Codificação do conteúdo serializado.</param>
    /// <param name="output">Stream de saída.</param>
    void Serialize(Entity entity, Stream output, Encoding encoding);

    /// <summary>
    /// Deserializa o texto para uma instância de Entity.
    /// </summary>
    /// <param name="input">A entrada para leitura do texto a ser deserializado.</param>
    /// <param name="encoding">Codificação do conteúdo lido.</param>
    /// <returns>A entidade obtida da serialização.</returns>
    Entity Deserialize(Stream input, Encoding encoding);
  }
}
