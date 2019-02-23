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
    /// <returns>O resultado da serializacao.</returns>
    public static string Serialize(this ISerializer serializer, Entity entity)
    {
      using (var memory = new MemoryStream())
      using (var reader = new StreamReader(memory))
      {
        serializer.Serialize(entity, memory, Encoding.UTF8);
        memory.Position = 0;
        var text = reader.ReadToEnd();
        return text;
      }
    }

    /// <summary>
    /// Serializa a entidade para a saída indicada.
    /// </summary>
    /// <param name="serializer">Serializador de documento.</param>
    /// <param name="entity">Entidade a ser serializada.</param>
    /// <param name="output">Stream de saída.</param>
    public static void Serialize(this ISerializer serializer, Entity entity, Stream output)
    {
      serializer.Serialize(entity, output, Encoding.UTF8);
    }

    /// <summary>
    /// Serializa a entidade para a saída indicada.
    /// </summary>
    /// <param name="serializer">Serializador de documento.</param>
    /// <param name="entity">Entidade a ser serializada.</param>
    /// <param name="output">Stream de saída.</param>
    /// <param name="encoding">Codificação de dados.</param>
    public static void Serialize(this ISerializer serializer, Entity entity, Stream output, string encoding)
    {
      serializer.Serialize(entity, output, Encoding.GetEncoding(encoding));
    }

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
        serializer.Serialize(entity, memory, writer.Encoding);
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
    /// <param name="input">O texto contendo a entidade serializada.</param>
    public static Entity Deserialize(this ISerializer serializer, string input)
    {
      using (var memory = new MemoryStream())
      using (var writer = new StreamWriter(memory, Encoding.UTF8))
      {
        writer.Write(input);
        writer.Flush();
        memory.Position = 0;

        var entity = serializer.Deserialize(memory, Encoding.UTF8);
        return entity;
      }
    }

    /// <summary>
    /// Serializa a entidade para a saída indicada.
    /// </summary>
    /// <param name="serializer">Serializador de documento.</param>
    /// <param name="input">Stream de entrada.</param>
    /// <param name="encoding">Codificação de dados.</param>
    public static Entity Deserialize(this ISerializer serializer, Stream input, string encoding)
    {
      return serializer.Deserialize(input, Encoding.GetEncoding(encoding));
    }

    /// <summary>
    /// Serializa a entidade para a saída indicada.
    /// </summary>
    /// <param name="serializer">Serializador de documento.</param>
    /// <param name="input">Stream de entrada.</param>
    public static Entity Deserialize(this ISerializer serializer, Stream input)
    {
      return serializer.Deserialize(input, Encoding.UTF8);
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
        var writer = new StreamWriter(memory, Encoding.UTF8);
        var buffer = new char[8 * 1024];
        var len = 0;

        while ((len = reader.ReadBlock(buffer, 0, buffer.Length)) > 0)
        {
          writer.Write(buffer, 0, len);
        }
        writer.Flush();

        memory.Position = 0;
        return serializer.Deserialize(memory, Encoding.UTF8);
      }
    }
  }
}
