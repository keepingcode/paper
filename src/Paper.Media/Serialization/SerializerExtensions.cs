using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Paper.Media.Serialization
{
  public static class SerializerExtensions
  {
    /// <summary>
    /// Serializa a entidade para a saída indicada.
    /// </summary>
    /// <param name="serializer">Serializador de documento.</param>
    /// <param name="entity">Entidade a ser serializada.</param>
    /// <param name="writer">Stream de saída.</param>
    public static void Serialize(this ISerializer serializer, Entity entity, TextWriter writer)
    {
      using (var memory = new MemoryStream())
      {
        serializer.Serialize(entity, memory);
        memory.Position = 0;

        var reader = new StreamReader(memory);
        var buffer = new char[8 * 1024];
        var len = 0;

        while ((len = reader.ReadBlock(buffer, 0, buffer.Length)) > 0)
        {
          writer.Write(buffer, 0, len);
        }
        writer.Flush();
      }
    }

    /// <summary>
    /// Serializa a entidade para a saída indicada.
    /// </summary>
    /// <param name="serializer">Serializador de documento.</param>
    /// <param name="reader">Stream de entrada.</param>
    public static Entity Deserialize(this ISerializer serializer, TextReader reader)
    {
      using (var memory = new MemoryStream())
      {
        var writer = new StreamWriter(memory);
        var buffer = new char[8 * 1024];
        var len = 0;

        while ((len = reader.ReadBlock(buffer, 0, buffer.Length)) > 0)
        {
          writer.Write(buffer, 0, len);
        }
        writer.Flush();

        memory.Position = 0;
        return serializer.Deserialize(memory);
      }
    }
  }
}
