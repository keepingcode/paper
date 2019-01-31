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
    public enum Formats { Json, Xml }

    private Formats defaultFormat;

    public MediaSerializer()
    {
      this.defaultFormat = Formats.Json;
    }

    public MediaSerializer(Formats defaultFormat)
    {
      this.defaultFormat = defaultFormat;
    }

    public MediaSerializer(string defaultFormat)
    {
      if (defaultFormat?.Contains("json") == true)
      {
        this.defaultFormat = Formats.Json;
      }
      else if (defaultFormat?.Contains("xml") == true)
      {
        this.defaultFormat = Formats.Xml;
      }
      else
      {
        this.defaultFormat = Formats.Json;
      }
    }

    #region Implementação de ISerializer

    public string Serialize(Entity entity)
    {
      using (var writer = new StringWriter())
      {
        WriteEntity(entity, null, writer);
        return writer.ToString();
      }
    }

    public void Serialize(Entity entity, Stream output)
    {
      var writer = new StreamWriter(output, Encoding.UTF8, 8 * 1024, true);
      WriteEntity(entity, null, writer);
    }

    public void Serialize(Entity entity, Stream output, Encoding encoding)
    {
      var writer = new StreamWriter(output, encoding, 8 * 1024, true);
      WriteEntity(entity, null, writer);
    }

    public void Serialize(Entity entity, TextWriter output)
    {
      WriteEntity(entity, null, output);
    }

    public Entity Deserialize(string text)
    {
      using (var reader = new StringReader(text))
      {
        var entity = ReadEntity(null, reader);
        return entity;
      }
    }

    public Entity Deserialize(Stream input)
    {
      var reader = new StreamReader(input, Encoding.UTF8);
      var entity = ReadEntity(null, reader);
      return entity;
    }

    public Entity Deserialize(Stream input, Encoding encoding)
    {
      var reader = new StreamReader(input, encoding);
      var entity = ReadEntity(null, reader);
      return entity;
    }

    public Entity Deserialize(TextReader input)
    {
      var entity = ReadEntity(null, input);
      return entity;
    }

    #endregion

    #region Outras implementações

    public string Serialize(Entity entity, string mediaType)
    {
      using (var writer = new StringWriter())
      {
        WriteEntity(entity, mediaType, writer);
        return writer.ToString();
      }
    }

    public void Serialize(Entity entity, string mediaType, TextWriter output)
    {
      WriteEntity(entity, mediaType, output);
    }

    public void Serialize(Entity entity, string mediaType, Stream output)
    {
      var writer = new StreamWriter(output, Encoding.UTF8, 8 * 1024, true);
      WriteEntity(entity, mediaType, writer);
    }

    public void Serialize(Entity entity, string mediaType, Stream output, Encoding encoding)
    {
      var writer = new StreamWriter(output, encoding, 8 * 1024, true);
      WriteEntity(entity, mediaType, writer);
    }

    public Entity Deserialize(string mediaType, string text)
    {
      using (var reader = new StringReader(text))
      {
        var entity = ReadEntity(mediaType, reader);
        return entity;
      }
    }

    public Entity Deserialize(string mediaType, Stream input)
    {
      using (var reader = new StreamReader(input, Encoding.UTF8))
      {
        var entity = ReadEntity(mediaType, reader);
        return entity;
      }
    }

    public Entity Deserialize(string mediaType, Stream input, Encoding encoding)
    {
      using (var reader = new StreamReader(input, encoding))
      {
        var entity = ReadEntity(mediaType, reader);
        return entity;
      }
    }

    public Entity Deserialize(string mediaType, TextReader input)
    {
      var entity = ReadEntity(mediaType, input);
      return entity;
    }

    #endregion

    #region Algoritmos

    private void WriteEntity(Entity entity, string mediaType, TextWriter output)
    {
      bool isJson = this.defaultFormat == Formats.Json;

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

    private Entity ReadEntity(string mediaType, TextReader input)
    {
      bool isJson = this.defaultFormat == Formats.Json;

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

    #endregion

  }
}