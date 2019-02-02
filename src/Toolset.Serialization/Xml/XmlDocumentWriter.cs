using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Toolset.Serialization.Xml
{
  public sealed class XmlDocumentWriter : Writer
  {
    private readonly XmlWriter writer;
    private readonly Stack<NodeType> stack;

    #region Construtores extras ...

    public XmlDocumentWriter(Stream textStream)
      : this(textStream, (SerializationSettings)null)
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public XmlDocumentWriter(TextWriter textWriter)
      : this(textWriter, (SerializationSettings)null)
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public XmlDocumentWriter(XmlWriter textWriter)
      : this(textWriter, (SerializationSettings)null)
    {
      // nada a fazer aqui. use o outro construtor.
    }

    #endregion

    public XmlDocumentWriter(Stream textStream, SerializationSettings settings)
      : base(settings)
    {
      this.writer = XmlWriter.Create(textStream,
        new XmlWriterSettings
        {
          Indent = this.Settings.Indent,
          IndentChars = this.Settings.IndentChars,
          Encoding = this.Settings.Encoding,
          OmitXmlDeclaration = this.Settings.IsFragment
        }
      );
      this.stack = new Stack<NodeType>();
      base.IsValid = true;
    }

    public XmlDocumentWriter(TextWriter textWriter, SerializationSettings settings)
      : base(settings)
    {
      this.writer = XmlWriter.Create(textWriter,
        new XmlWriterSettings
        {
          Indent = this.Settings.Indent,
          IndentChars = this.Settings.IndentChars,
          Encoding = this.Settings.Encoding,
          OmitXmlDeclaration = this.Settings.IsFragment
        }
      );
      this.stack = new Stack<NodeType>();
      base.IsValid = true;
    }

    public XmlDocumentWriter(XmlWriter textWriter, SerializationSettings settings)
      : base(settings)
    {
      this.writer = textWriter;
      this.stack = new Stack<NodeType>();
      base.IsValid = true;
    }

    public new XmlSerializationSettings Settings
    {
      get { return base.Settings.As<XmlSerializationSettings>(); }
    }

    protected override void DoWrite(Node node)
    {
      switch (node.Type)
      {
        case NodeType.DocumentStart:
        case NodeType.DocumentEnd:
          // nada a fazer
          break;

        case NodeType.ObjectStart:
          {
            var parentKind = stack.FirstOrDefault();
            var parentIsProperty = parentKind.HasFlag(NodeType.Property);

            if (!parentIsProperty)
            {
              var name = (node.Value ?? "Element").ToString();
              var tagName = ValueConventions.CreateName(name, Settings, TextCase.PascalCase);

              writer.WriteStartElement(tagName);
            }

            stack.Push(NodeType.Object);
            break;
          }

        case NodeType.ObjectEnd:
          {
            stack.Pop();

            var parentKind = stack.FirstOrDefault();
            var parentIsProperty = parentKind.HasFlag(NodeType.Property);

            if (!parentIsProperty)
            {
              writer.WriteEndElement();
            }
            break;
          }

        case NodeType.CollectionStart:
          {
            var parentKind = stack.FirstOrDefault();
            var parentIsProperty = parentKind.HasFlag(NodeType.Property);

            if (!parentIsProperty)
            {
              var name = (node.Value ?? "Array").ToString();
              var tagName = ValueConventions.CreateName(name, Settings, TextCase.PascalCase);
              writer.WriteStartElement(tagName);
            }

            var attName = ValueConventions.CreateName("IsArray", Settings, TextCase.PascalCase);
            writer.WriteAttributeString(attName, "true");
            
            stack.Push(NodeType.Collection);
            break;
          }

        case NodeType.CollectionEnd:
          {
            stack.Pop();

            var parentKind = stack.FirstOrDefault();
            var parentIsProperty = parentKind.HasFlag(NodeType.Property);

            if (!parentIsProperty)
            {
              writer.WriteEndElement();
            }

            break;
          }

        case NodeType.PropertyStart:
          {
            var name = (node.Value ?? "Property").ToString();
            var tagName = ValueConventions.CreateName(name, Settings, TextCase.PascalCase);

            var isAttribute = name.StartsWith("@");
            if (isAttribute)
            {
              writer.WriteStartAttribute(tagName.Substring(1));
              stack.Push(NodeType.Property | (NodeType)0x4000);
            }
            else
            {
              writer.WriteStartElement(tagName);
              stack.Push(NodeType.Property);
            }
            
            break;
          }

        case NodeType.PropertyEnd:
          {
            var isAttribute = stack.Peek().HasFlag((NodeType)0x4000);
            if (isAttribute)
            {
              writer.WriteEndAttribute();
            }
            else
            {
              writer.WriteEndElement();
            }
            stack.Pop();
            break;
          }

        case NodeType.Value:
          {
            var isArrayElement = stack.Peek().HasFlag(NodeType.Collection);
            if (isArrayElement)
            {
              writer.WriteStartElement("Item");
            }

            if (node.Value == null)
            {
              writer.WriteValue(null);
            }
            else if (node.Value is XContainer)
            {
              var xml = (XContainer)node.Value;
              var cdata = xml.ToString(SaveOptions.DisableFormatting);
              writer.WriteCData(cdata);
            }
            else
            {
              var text = ValueConventions.CreateText(node.Value, Settings);
              writer.WriteValue(text);
            }

            if (isArrayElement)
            {
              writer.WriteEndElement();
            }

            break;
          }

        default:
          throw new SerializationException("Token não esperado: " + node);
      }

      if (Settings.AutoFlush)
      {
        DoFlush();
      }

    }

    protected override void DoWriteComplete()
    {
      writer.Flush();
    }

    protected override void DoFlush()
    {
      writer.Flush();
    }

    protected override void DoClose()
    {
      writer.Flush();
      if (!Settings.KeepOpen)
      {
        writer.Close();
      }
    }
  }
}
