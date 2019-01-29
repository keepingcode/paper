using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Paper.Media.Serialization
{
  public class MediaSerializer : ISerializer
  {
    public enum Formats
    {
      Json,
      Xml
    }

    public MediaSerializer()
    {
      this.DefaultFormat = Formats.Json;
    }

    public MediaSerializer(Formats defaultFormat)
    {
      DefaultFormat = defaultFormat;
    }

    public MediaSerializer(string defaultFormat)
    {
      var isJson = defaultFormat?.Contains("json") ?? true;
      DefaultFormat = isJson ? Formats.Json : Formats.Xml;
    }

    public Formats DefaultFormat { get; }

    public void Serialize(Entity entity, string mediaType, TextWriter output)
    {
      bool isJson = this.DefaultFormat == Formats.Json;

      if (mediaType != null)
      {
        if (mediaType.Contains("json"))
        {
          isJson = true;
        }
        else if (mediaType.Contains("xml"))
        {
          isJson = false;
        }
      }

      if (isJson)
      {
        var serializer = new SirenSerializer();
        serializer.Serialize(entity, output);
      }
      else
      {
        var serializer = new DataContractSerializer(typeof(Entity), new[] { typeof(DBNull) });
        using (var writer = XmlWriter.Create(output, new XmlWriterSettings { CloseOutput = false }))
        {
          serializer.WriteObject(writer, entity);
          writer.Flush();
        }
      }

      output.Flush();
    }

    public Entity Deserialize(string mediaType, TextReader input)
    {
      bool isJson = this.DefaultFormat == Formats.Json;

      if (mediaType != null)
      {
        if (mediaType.Contains("json"))
        {
          isJson = true;
        }
        else if (mediaType.Contains("xml"))
        {
          isJson = false;
        }
      }

      if (isJson)
      {
        var serializer = new SirenSerializer();
        var entity = serializer.Deserialize(input);
        return entity;
      }
      else
      {
        var serializer = new DataContractSerializer(typeof(Entity), new[] { typeof(DBNull) });
        using (var writer = XmlReader.Create(input, new XmlReaderSettings { CloseInput = false }))
        {
          var entity = (Entity)serializer.ReadObject(writer);
          return entity;
        }
      }
    }

    #region Implementação de ISerializer

    public string Serialize(Entity entity)
    {
      using (var writer = new StringWriter())
      {
        Serialize(entity, null, writer);
        return writer.ToString();
      }
    }

    public void Serialize(Entity entity, Stream output)
    {
      var writer = new StreamWriter(output, Encoding.UTF8, 8 * 1024, true);
      Serialize(entity, null, writer);
    }

    public void Serialize(Entity entity, Stream output, Encoding encoding)
    {
      var writer = new StreamWriter(output, encoding, 8 * 1024, true);
      Serialize(entity, null, writer);
    }

    public void Serialize(Entity entity, TextWriter output)
    {
      Serialize(entity, null, output);
    }

    public Entity Deserialize(string text)
    {
      using (var reader = new StringReader(text))
      {
        var entity = Deserialize(null, reader);
        return entity;
      }
    }

    public Entity Deserialize(Stream input)
    {
      var reader = new StreamReader(input, Encoding.UTF8);
      var entity = Deserialize(null, reader);
      return entity;
    }

    public Entity Deserialize(Stream input, Encoding encoding)
    {
      var reader = new StreamReader(input, encoding);
      var entity = Deserialize(null, reader);
      return entity;
    }

    public Entity Deserialize(TextReader input)
    {
      var entity = Deserialize(null, input);
      return entity;
    }

    #endregion

    #region Outras implementações de Serialize()

    public void Serialize(Entity entity, string mediaType, Stream output)
    {
      Serialize(entity, mediaType, output, Encoding.UTF8);
    }

    public void Serialize(Entity entity, string mediaType, Stream output, Encoding encoding)
    {
      var writer = new StreamWriter(output, encoding, 8 * 1024, true);
      Serialize(entity, mediaType, writer);
    }

    public string Serialize(Entity entity, string mediaType)
    {
      using (var writer = new StringWriter())
      {
        Serialize(entity, mediaType, writer);
        return writer.ToString();
      }
    }

    #endregion
  }
}