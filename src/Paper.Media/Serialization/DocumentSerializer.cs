using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Paper.Media.Utilities;
using Toolset;
using Toolset.Reflection;
using Toolset.Serialization;
using Toolset.Serialization.Csv;
using Toolset.Serialization.Excel;
using Toolset.Serialization.Json;
using Toolset.Serialization.Transformations;
using Toolset.Serialization.Xml;

namespace Paper.Media.Serialization
{
  public class DocumentSerializer : ISerializer
  {
    public const string Json = "application/json";
    public const string JsonSiren = "application/vnd.siren+json";
    public const string Xml = "application/xml";
    public const string XmlSiren = "application/vnd.siren+xml";
    public const string Csv = "text/csv";
    public const string Excel = "application/vnd.ms-excel";

    private readonly SerializationOptions options;

    public DocumentSerializer(string mimeType)
      : this(mimeType, (SerializationOptions)null)
    {
    }

    public DocumentSerializer(string mimeType, bool indent)
      : this(mimeType, new SerializationOptions { Indent = indent })
    {
    }

    public DocumentSerializer(string mimeType, SerializationOptions options)
    {
      this.MimeType = ParseFormat(mimeType ?? "application/vnd.siren+json");
      this.options = options;
    }

    public string MimeType { get; }

    private string ParseFormat(string mimeType)
    {
      if (mimeType.Contains("siren+xml"))
        return XmlSiren;
      if (mimeType.Contains("siren"))
        return JsonSiren;
      if (mimeType.Contains("json"))
        return Json;
      if (mimeType.Contains("xml"))
        return Xml;
      if (mimeType.Contains("csv"))
        return Csv;
      if (mimeType.Contains("excel"))
        return Excel;
      return JsonSiren;
    }

    public void Serialize(Entity entity, Stream output)
    {
      using (var writer = CreateWriter(output))
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

    private Writer CreateWriter(Stream output)
    {
      switch (MimeType)
      {
        case Xml:
        case XmlSiren:
          return new XmlDocumentWriter(output,
            new XmlSerializationSettings
            {
              KeepOpen = true,
              Indent = options?.Indent == true,
              IndentChars = options?.TextCase
            }
          );

        case Csv:
          return new CsvWriter(output,
            new CsvSerializationSettings
            {
              KeepOpen = true,
              HasHeaders = true
            }
          );

        case Excel:
          return new ExcelWriter(output,
            new ExcelSerializationSettings
            {
              KeepOpen = true,
              HasHeaders = true
            }
          );

        default:
          return new JsonWriter(output,
            new JsonSerializationSettings
            {
              KeepOpen = true,
              Indent = options?.Indent == true,
              IndentChars = options?.TextCase
            }
          );
      }
    }

    private void WritePayload(Writer writer, Entity entity)
    {
      writer.WriteDocumentStart(entity.Title ?? "Payload");
      writer.WriteObjectStart("Payload");

      writer.WritePropertyStart("Rows");
      writer.WriteCollectionStart();

      var isData = entity.Class?.Contains(ClassNames.Data) == true;
      if (isData)
      {
        WritePayloadProperties(writer, entity);
      }
      writer.WriteCollectionEnd();
      writer.WritePropertyEnd();

      var rows = entity.Entities?.Where(e => e.Class.Contains(ClassNames.Row));
      if (rows?.Any() == true)
      {
        writer.WritePropertyStart("Rows");
        writer.WriteCollectionStart();
        foreach (var row in rows)
        {
          WritePayloadProperties(writer, row);
        }
      }

      writer.WriteCollectionEnd();
      writer.WritePropertyEnd();

      writer.WriteObjectEnd();
      writer.WriteDocumentEnd();
    }

    private void WritePayloadProperties(Writer writer, Entity entity)
    {
      if (entity.Properties is PropertyCollection properties)
      {
        writer.WriteObjectStart("Row");

        var type = entity.Class?.FirstOrDefault(x => char.IsUpper(x.First()));
        if (type != null)
        {
          writer.WriteProperty("@Type", type);
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

    public Entity Deserialize(Stream input)
    {
      return null;
    }
  }
}