using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
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
    public readonly string mimeType;

    public MediaSerializer()
      : this(null, null)
    {
      // Use o outro construtor.
    }

    public MediaSerializer(string mimeType)
      : this(mimeType, null)
    {
      // Use o outro construtor.
    }

    public MediaSerializer(string mimeType, SerializationOptions options)
    {
      var validMimeType = ParseFormat(mimeType);
      this.mimeType = validMimeType;
      this.options = options ?? new SerializationOptions();
    }

    public static bool IsSupportedFormat(string mimeType)
    {
      return SupportedMimeTypes.Contains(mimeType);
    }

    public static string ParseFormat(string mimeType)
    {
      if (string.IsNullOrEmpty(mimeType))
        return null;
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
        writer.WriteDocumentStart(entity.Title ?? entity.GetType().Name);
        if (entity != null)
        {
          var isPayload = !mimeType.Contains("siren");
          if (isPayload)
          {
            var payload = Payload.FromEntity(entity);
            Write(writer, payload);
          }
          else
          {
            Write(writer, entity);
          }
        }
        writer.WriteDocumentEnd();
        writer.Flush();
      }
    }

    private void Write(Writer writer, object element, string elementName = null)
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
        writer.WriteObjectStart(elementName ?? element.GetType().Name);
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
        var attr = element._GetAttribute<CollectionDataContractAttribute>();
        var suggestedName = attr?.ItemName;

        writer.WriteCollectionStart(element.GetType().Name);
        foreach (var item in list)
        {
          Write(writer, item, suggestedName);
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
      var textCase = this.mimeType.Contains("json") ? TextCase.CamelCase : TextCase.PascalCase;

      switch (mimeType)
      {
        case Xml:
        case XmlSiren:
          return new XmlDocumentWriter(output,
            new XmlSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              TextCase = textCase,
              Indent = options.Indent,
              IndentChars = options.IndentChars
            }
          );

        case Csv:
          return new CsvWriter(output,
            new CsvSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              HasHeaders = true,
              TextCase = textCase
            }
          );

        case Excel:
          return new ExcelWriter(output,
            new ExcelSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              HasHeaders = true,
              TextCase = textCase
            }
          );

        default:
          return new JsonWriter(output,
            new JsonSerializationSettings
            {
              Encoding = encoding,
              KeepOpen = true,
              TextCase = textCase,
              Indent = options.Indent,
              IndentChars = options.IndentChars
            }
          );
      }
    }

    #endregion

    #region Readers

    public Entity Deserialize(Stream input, Encoding encoding)
    {
      bool forceHypermedia;
      using (var reader = CreateReader(input, encoding, out forceHypermedia))
      {
        GraphWriter graphWriter = null;

        using (var writer = new DelayedWriter())
        {
          writer.Intercept += (o, e) =>
          {
            if (e.Node.Type.HasFlag(NodeType.Property))
            {
              var isPayload =
                   "data".EqualsIgnoreCase(e.Node.Value as string)
                || "rows".EqualsIgnoreCase(e.Node.Value as string)
                || "form".EqualsIgnoreCase(e.Node.Value as string);

              graphWriter = isPayload ? new GraphWriter(typeof(Payload)) : new GraphWriter(typeof(Entity));
              writer.SetWriter(graphWriter);
            }
          };

          reader.CopyTo(writer);
        }

        var graph = graphWriter?.Graphs.Cast<object>().FirstOrDefault();
        var entity = graph is Payload payload ? payload.ToEntity() : (Entity)graph;
        return entity;
      }
    }

    private T Read<T>(Reader reader)
      where T : class, new()
    {
      using (var writer = new GraphWriter<T>())
      {
        reader.CopyTo(writer);
        return writer.Graphs.FirstOrDefault();
      }
    }

    private Reader CreateReader(Stream input, Encoding encoding, out bool forceHypermedia)
    {
      Reader reader;

      forceHypermedia = false;

      var settings = new SerializationSettings { Encoding = encoding, KeepOpen = true };

      switch (mimeType)
      {
        case Json:
        case JsonSiren:
          {
            reader = new JsonReader(input, settings);
            break;
          }

        case Xml:
        case XmlSiren:
          {
            reader = new XmlDocumentReader(input, settings);
            break;
          }

        case Csv:
          {
            reader = new CsvReader(input, settings);
            break;
          }

        case Excel:
          {
            throw new MediaException(HttpStatusCode.NotAcceptable);
          }

        default:
          {
            reader = Reader.CreateReader(input, settings);
            break;
          }
      }

      if (reader is CsvReader)
      {
        reader = new TransformReader(reader, new CsvRowsTransform());
      }

      return reader;
    }

    #endregion
  }
}