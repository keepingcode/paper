using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Toolset.Serialization.Xml;
using System.Text.RegularExpressions;

namespace Toolset.Serialization
{
  public class FormattedTextReader : TextReaderComposite
  {
    public const string XmlFormat = "xml";
    public const string JsonFormat = "json";
    public const string CsvFormat = "csv";
    public const string UnknownFormat = "unknown";

    private FormattedTextReader(string format, params TextReader[] readers)
      : base(readers)
    {
      this.DocumentFormat = format;
    }

    public string DocumentFormat
    {
      get;
      private set;
    }

    public static FormattedTextReader Create(Stream stream)
    {
      var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
      return CreateReader(reader);
    }

    public static FormattedTextReader Create(Stream stream, Encoding encoding)
    {
      var reader = new StreamReader(stream, encoding ?? Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
      return CreateReader(reader);
    }

    public static FormattedTextReader CreateReader(TextReader reader)
    {
      string format;

      var sneakPeekReader = SneakPeekTextReader.CreateReader(reader, peekSize: 1);
      var ch = sneakPeekReader.PeekedText.TrimStart().FirstOrDefault();

      if (ch == '{' || ch == '[')
      {
        format = JsonFormat;
      }
      else if (ch == '<')
      {
        format = XmlFormat;
      }
      else if (ch >= ' ' || ch <= '~')
      {
        format = CsvFormat;
      }
      else
      {
        format = UnknownFormat;
      }

      return new FormattedTextReader(format, sneakPeekReader);
    }
  }
}
