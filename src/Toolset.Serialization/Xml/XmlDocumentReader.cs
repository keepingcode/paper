using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using Toolset.Collections;

namespace Toolset.Serialization.Xml
{
  public class XmlDocumentReader : Reader, sArray
  {
    private readonly XmlReader reader;
    private readonly IEnumerator<Node> enumerator;

    private Scope scope;

    public override Node Current => enumerator.Current;

    #region Construtores extras ...

    public XmlDocumentReader(TextReader textReader)
      : this(textReader, GetXmlSerializationSettings(null))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public XmlDocumentReader(TextReader textReader, SerializationSettings settings)
      : this((XmlReader)XmlTextReader.Create(textReader), GetXmlSerializationSettings(settings))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public XmlDocumentReader(Stream textStream)
      : this(new StreamReader(textStream), GetXmlSerializationSettings(null))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public XmlDocumentReader(Stream textStream, SerializationSettings settings)
      : this(new StreamReader(textStream), GetXmlSerializationSettings(settings))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public XmlDocumentReader(string filename)
      : this(new StreamReader(filename), GetXmlSerializationSettings(null))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public XmlDocumentReader(string filename, SerializationSettings settings)
      : this(new StreamReader(filename), GetXmlSerializationSettings(settings))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    #endregion

    public XmlDocumentReader(XmlReader xmlReader)
      : this(xmlReader, GetXmlSerializationSettings(null))
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public XmlDocumentReader(XmlReader reader, XmlSerializationSettings settings)
      : base(settings)
    {
      this.reader = reader;
      this.enumerator = EmitNodes().NonNull().GetEnumerator();
      this.scope = new Scope { Type = NodeType.Unknown };
    }

    private static XmlSerializationSettings GetXmlSerializationSettings(SerializationSettings settings)
    {
      return settings is XmlSerializationSettings
        ? (XmlSerializationSettings)settings : new XmlSerializationSettings(settings);
    }

    public new XmlSerializationSettings Settings => (XmlSerializationSettings)base.Settings;

    private int depth;

    protected override bool DoRead()
    {
      return enumerator.MoveNext();
    }

    private IEnumerable<Node> EmitNodes()
    {
      var ok = reader.Read();
      if (!ok)
        yield break;

      foreach (var node in EmitNodes(NodeType.DocumentStart)) yield return Settings.IsFragment ? null : node;

      do
      {
        switch (reader.NodeType)
        {
          case XmlNodeType.Text:
            {
              foreach (var node in EmitNodes(NodeType.Value, value: reader.Value)) yield return node;
              break;
            }
          case XmlNodeType.Element:
            {
              switch (scope.Type)
              {
                case NodeType.Document:
                  {
                    foreach (var node in EmitNodes(NodeType.ObjectStart)) yield return node;
                    break;
                  }
                case NodeType.Collection:
                case NodeType.Object:
                  {
                    if (scope.Type == NodeType.Object)
                    {
                      foreach (var node in EmitNodes(NodeType.PropertyStart, fallback: true)) yield return node;
                    }

                    if (IsArray())
                    {
                      foreach (var node in EmitNodes(NodeType.CollectionStart)) yield return node;
                    }
                    else if (reader.HasAttributes)
                    {
                      foreach (var node in EmitNodes(NodeType.ObjectStart)) yield return node;

                      for (int i = 0; i < reader.AttributeCount; i++)
                      {
                        reader.MoveToAttribute(i);

                        var name = $"@{reader.Name}";
                        var value = reader.Value;

                        foreach (var node in EmitNodes(NodeType.PropertyStart, name: name)) yield return node;
                        foreach (var node in EmitNodes(NodeType.Value, value: value)) yield return node;
                        foreach (var node in EmitNodes(NodeType.PropertyEnd)) yield return node;
                      }

                      reader.MoveToElement();
                    }
                    else if (reader.IsEmptyElement)
                    {
                      foreach (var node in EmitNodes(NodeType.Value, value: null)) yield return node;

                      if (scope.Type == NodeType.Collection)
                      {
                        break;
                      }
                    }
                    else
                    {
                      foreach (var node in EmitNodes(NodeType.ObjectStart, imaginary: true)) yield return node;
                    }

                    if (reader.IsEmptyElement)
                    {
                      foreach (var node in EmitNodes(scope.Type | NodeType.End)) yield return node;
                    }
                    break;
                  }
                //case NodeType.Collection:
                //  {
                //    if (IsArray())
                //    {
                //      foreach (var node in EmitNodes(NodeType.CollectionStart)) yield return node;
                //      if (reader.IsEmptyElement)
                //      {
                //        foreach (var node in EmitNodes(NodeType.CollectionEnd)) yield return node;
                //      }
                //    }
                //    else if (reader.IsEmptyElement && !reader.HasAttributes)
                //    {
                //      foreach (var node in EmitNodes(NodeType.Value, value: null)) yield return node;
                //    }
                //    else
                //    {
                //      foreach (var node in EmitNodes(NodeType.ObjectStart, imaginary: true)) yield return node;
                //    }
                //    break;
                //  }
              }
              break;
            }
          case XmlNodeType.EndElement:
            {
              foreach (var node in EmitNodes(scope.Type | NodeType.End)) yield return node;
              break;
            }
        }
      } while (reader.Read());

      foreach (var node in EmitNodes(NodeType.DocumentEnd)) yield return Settings.IsFragment ? null : node;
    }

    private IEnumerable<Node> EmitNodes(NodeType type, string name = null, string value = null, bool fallback = false, bool imaginary = false)
    {
      if (scope.Imaginary)
      {
        scope.Imaginary = false;
        if (type == NodeType.Value)
        {
          scope.Discarded = true;
        }
        else if (type == NodeType.Value || type.HasFlag(NodeType.End))
        {
          scope.Discarded = true;
          yield return new Node(NodeType.Value, null);
        }
        else
        {
          yield return new Node(scope.Type | NodeType.Start, scope.Name);
        }
      }

      if (type.HasFlag(NodeType.Start))
      {
        scope = new Scope
        {
          Parent = scope,
          Name = name ?? reader.Name,
          Type = type & ~NodeType.Start,
          Imaginary = imaginary,
          Fallback = fallback
        };
        if (!scope.Imaginary)
        {
          yield return new Node(scope.Type | NodeType.Start, scope.Name);
        }
      }
      else if (type.HasFlag(NodeType.End))
      {
        if (!scope.Discarded)
        {
          yield return new Node(type);
        }
        scope = scope.Parent;

        while (scope.Fallback)
        {
          yield return new Node(scope.Type | NodeType.End);
          scope = scope.Parent;
        }
      }
      else
      {
        yield return new Node(type, value?.Trim());
      }
    }

    private bool IsArray()
    {
      bool isArray = false;
      for (int i = 0; i < reader.AttributeCount; i++)
      {
        reader.MoveToAttribute(i);
        if (reader.Name.EqualsIgnoreCase("IsArray"))
        {
          var value = reader.Value;
          isArray = Change.To<bool>(value);
          break;
        }
      }
      reader.MoveToElement();
      return isArray;
    }

    public override void Close()
    {
      if (!Settings.KeepOpen)
      {
        reader?.Close();
      }
    }

    private class Scope
    {
      public Scope Parent { get; set; }

      public NodeType Type { get; set; }

      public string Name { get; set; }

      public bool Imaginary { get; set; }

      public bool Fallback { get; set; }

      public bool Discarded { get; set; }
    }
  }
}
