﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Paper.Media.Utilities;
using Toolset;
using Toolset.Reflection;
using Toolset.Serialization;
using Toolset.Serialization.Csv;
using Toolset.Serialization.Excel;
using Toolset.Serialization.Graph;
using Toolset.Serialization.Json;
using Toolset.Serialization.Transformations;
using Toolset.Serialization.Xml;

namespace Paper.Media.Serialization
{
  public class MediaSerializer : ISerializer
  {
    public const string Json = "application/json";
    public const string JsonSiren = "application/vnd.siren+json";
    public const string Xml = "application/xml";
    public const string XmlSiren = "application/vnd.siren+xml";
    public const string Csv = "text/csv";
    public const string Excel = "application/vnd.ms-excel";

    /// <summary>
    ///  Todos os tipos suportados em ordem de precedencia.
    /// </summary>
    public static string[] SupportedMimeTypes { get; } = { JsonSiren, Json, XmlSiren, Xml, Csv, Excel };

    private readonly SerializationOptions options;

    public MediaSerializer()
      : this(null, null)
    {
    }

    public MediaSerializer(string mimeType)
      : this(mimeType, null)
    {
    }

    public MediaSerializer(string mimeType, SerializationOptions options)
    {
      var validMimeType = ParseFormat(mimeType)
        ?? throw new MediaException(HttpStatusCode.NotAcceptable, "Formato não suportado: " + mimeType);
      
      this.MimeType = validMimeType;
      this.options = options ?? new SerializationOptions();
    }

    public string MimeType { get; }

    public static bool IsSupportedFormat(string mimeType)
    {
      return SupportedMimeTypes.Contains(mimeType);
    }

    public static string ParseFormat(string mimeType)
    {
      if (string.IsNullOrEmpty(mimeType))
        return JsonSiren;
      if (mimeType.Contains("siren") && mimeType.Contains("xml"))
        return XmlSiren;
      if (mimeType.Contains("siren"))
        return JsonSiren;
      if (mimeType.Contains("json"))
        return Json;
      if (mimeType.Contains("xml"))
        return Xml;
      if (mimeType.Contains("csv"))
        return Csv;
      if (mimeType.Contains("excel") || mimeType.Contains("xlsx"))
        return Excel;

      return null;
    }

    #region Writers

    public void Serialize(Entity entity, Stream output, Encoding encoding)
    {
      using (var writer = CreateWriter(output, encoding))
      {
        var isPayloadOnly = !MimeType.Contains("siren");
        if (isPayloadOnly)
        {
          WritePayload(writer, entity);
        }
        else
        {
          WriteHypermedia(writer, entity);
        }
        writer.Flush();
      }
    }

    private void WritePayload(Writer writer, Entity entity)
    {
      writer.WriteDocumentStart(entity.Title ?? "Payload");
      writer.WriteObjectStart("Payload");

      WritePayloadData(writer, entity);
      WritePayloadRows(writer, entity);

      writer.WriteObjectEnd();
      writer.WriteDocumentEnd();
    }

    private void WritePayloadData(Writer writer, Entity entity)
    {
      var hasData = entity.Class?.Contains(ClassNames.Data) == true;
      if (!hasData)
        return;
      
      var type = entity.Class?.FirstOrDefault(x => char.IsUpper(x.First()));

      writer.WritePropertyStart(type ?? "Data");
      writer.WriteCollectionStart(type ?? entity.Title);

      WritePayloadProperties(writer, entity);

      writer.WriteCollectionEnd();
      writer.WritePropertyEnd();
    }

    private void WritePayloadRows(Writer writer, Entity entity)
    {
      var hasRows = entity.Entities?.Any(e => e.Class.Contains(ClassNames.Data)) == true;
      if (!hasRows)
        return;

      var groups =
        from child in entity.Entities.Where(e => e.Class.Contains(ClassNames.Data))
        let type = child.Class?.FirstOrDefault(x => char.IsUpper(x.First()))
        group child by type into g
        select new
        {
          type = g.Key,
          rows = g
        };

      foreach (var group in groups)
      {
        var type = group.type;
        var rows = group.rows;

        writer.WritePropertyStart(type ?? "Rows");
        writer.WriteCollectionStart(type ?? entity.Title);

        foreach (var row in rows)
        {
          WritePayloadProperties(writer, row);
        }

        writer.WriteCollectionEnd();
        writer.WritePropertyEnd();
      }
    }

    private void WritePayloadProperties(Writer writer, Entity entity)
    {
      if (entity.Properties is PropertyCollection properties)
      {
        writer.WriteObjectStart("Row");

        if (!(writer is ExcelWriter))
        {
          var type = entity.Class?.FirstOrDefault(x => char.IsUpper(x.First()));
          if (type != null)
          {
            writer.WriteProperty("@Type", type);
          }
        }

        foreach (var property in properties)
        {
          if (!property.Name.StartsWith("_"))
          {
            Write(writer, property);
          }
        }

        writer.WriteObjectEnd();
      }
    }

    private void WriteHypermedia(Writer writer, Entity entity)
    {
      writer.WriteDocumentStart(entity.Title ?? entity.GetType().Name);
      if (entity != null)
      {
        Write(writer, entity);
      }
      writer.WriteDocumentEnd();
    }

    private void Write(Writer writer, object element)
    {
      if (element == null)
      {
        writer.WriteValue(null);
        return;
      }

      var primitiveType = Nullable.GetUnderlyingType(element.GetType());
      if (primitiveType != null)
      {
        element = Change.To(element, primitiveType);
      }

      if (element is CaseVariantString caseString)
      {
        var text = caseString.ChangeCase(writer.Settings.TextCase);
        writer.WriteValue(text);
        return;
      }

      if (element.GetType().IsValueType || StringUtils.IsStringCompatible(element))
      {
        writer.WriteValue(element);
        return;
      }

      if (element is PropertyCollection properties)
      {
        writer.WriteObjectStart(element.GetType().Name);
        foreach (var item in properties)
        {
          Write(writer, item);
        }
        writer.WriteObjectEnd();
        return;
      }

      if (element is Property property)
      {
        writer.WritePropertyStart(property.Name);
        Write(writer, property.Value);
        writer.WritePropertyEnd();
        return;
      }

      if (element is IEnumerable list)
      {
        writer.WriteCollectionStart(element.GetType().Name);
        foreach (var item in list)
        {
          Write(writer, item);
        }
        writer.WriteCollectionEnd();
        return;
      }

      writer.WriteObjectStart(element.GetType().Name);
      foreach (var name in element._GetPropertyNames())
      {
        var value = element._Get(name);
        if (value != null)
        {
          writer.WritePropertyStart(name);
          Write(writer, value);
          writer.WritePropertyEnd();
        }
      }
      writer.WriteObjectEnd();
    }

    private Writer CreateWriter(Stream output, Encoding encoding)
    {
      switch (MimeType)
      {
        case Xml:
        case XmlSiren:
          return new XmlDocumentWriter(output,
            new XmlSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              Indent = options.Indent,
              IndentChars = options.TextCase
            }
          );

        case Csv:
          return new CsvWriter(output,
            new CsvSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              HasHeaders = true
            }
          );

        case Excel:
          return new ExcelWriter(output,
            new ExcelSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              HasHeaders = true
            }
          );

        default:
          return new JsonWriter(output,
            new JsonSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              Indent = options.Indent,
              IndentChars = options.TextCase
            }
          );
      }
    }

    #endregion

    #region Readers

    public Entity Deserialize(Stream input, Encoding encoding)
    {
      Entity entity;
      using (var reader = CreateReader(input, encoding))
      {
        var isPayloadOnly = !MimeType.Contains("siren");
        if (isPayloadOnly)
        {
          entity = ReadPayload(reader);
        }
        else
        {
          entity = ReaderHypermedia(reader);
        }
      }
      return entity;
    }

    private Entity ReadPayload(Reader reader)
    {
      using (var writer = new TraceWriter())
      {
        reader.CopyTo(writer);
      }
      return null;
    }

    private Entity ReaderHypermedia(Reader reader)
    {
      using (var writer = new GraphWriter2<Entity>())
      {
        reader.CopyTo(writer);

        Debug.WriteLine(writer);
      }
      return null;
    }

    private Reader CreateReader(Stream input, Encoding encoding)
    {
      switch (MimeType)
      {
        case Json:
        case JsonSiren:
          return new JsonReader(input,
            new JsonSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true
            }
          );

        case Xml:
        case XmlSiren:
          return new XmlDocumentReader(input,
            new XmlSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true
            }
          );

        case Csv:
          return new CsvReader(input,
            new CsvSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              HasHeaders = true
            }
          );

        case Excel:
          throw new MediaException(HttpStatusCode.NotAcceptable);

        default:
          return Reader.CreateReader(input,
            new CsvSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              HasHeaders = true
            }
          );
      }
    }

    #endregion
  }
}