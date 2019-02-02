using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Toolset.Serialization.Xml;

namespace Toolset.Serialization
{
  public class SneakPeekTextReader : TextReaderComposite
  {
    private SneakPeekTextReader(string peekedText, params TextReader[] readers)
      : base(readers)
    {
      this.PeekedText = peekedText;
    }

    public string PeekedText { get; }

    public static SneakPeekTextReader CreateReader(TextReader reader, int peekSize)
    {
      var memory = new MemoryStream();
      var writer = new StreamWriter(memory);
      var buffer = new List<char>(peekSize);

      char ch;
      while ((reader.Peek() > -1) && (buffer.Count(x => !char.IsWhiteSpace(x)) < peekSize))
      {
        buffer.Add(ch = (char)reader.Read());
      }

      var peekedText = new string(buffer.ToArray());
      var peekedTextReader = new StringReader(peekedText);
      return new SneakPeekTextReader(peekedText, peekedTextReader, reader);
    }
  }
}
