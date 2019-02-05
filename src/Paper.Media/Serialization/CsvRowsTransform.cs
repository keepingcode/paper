using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Serialization;

namespace Paper.Media.Serialization
{
  class CsvRowsTransform : ITransform
  {
    private readonly Queue<object> queue = new Queue<object>();

    private bool collecting;
    private int row;
    private string[] fields = new string[0];

    public SerializationSettings Settings { get; set; }

    public IEnumerable<Node> TransformNode(Node node)
    {
      return Classify(node).DefaultIfEmpty();
    }

    private IEnumerable<Node> Classify(Node node)
    {
      switch (node.Type)
      {
        case NodeType.CollectionStart:
          {
            collecting = true;
            yield return new Node(NodeType.DocumentStart, "rows");
            yield return new Node(NodeType.ObjectStart, "rows");
            yield return new Node(NodeType.PropertyStart, "rows");
            yield return new Node(NodeType.CollectionStart, "rows");
            yield break;
          }
        case NodeType.CollectionEnd:
          {
            collecting = false;
            yield return new Node(NodeType.CollectionEnd);
            yield return new Node(NodeType.PropertyEnd);
            yield return new Node(NodeType.ObjectEnd);
            yield return new Node(NodeType.DocumentEnd);
            yield break;
          }
        case NodeType.ObjectStart:
        case NodeType.PropertyStart:
        case NodeType.PropertyEnd:
          {
            yield break;
          }
        case NodeType.Value:
          {
            if (collecting)
            {
              queue.Enqueue(node.Value);
            }
            yield break;
          }
        case NodeType.ObjectEnd:
          {
            if (collecting)
            {
              if (queue.OfType<string>().FirstOrDefault() == "@class")
              {
                fields = queue.Select(x => x.ToString()).ToArray();
                queue.Clear();
              }
              else
              {
                yield return new Node(NodeType.ObjectStart, $"Row{++row}");

                var fieldMaker = EmitFields().GetEnumerator();
                while (queue.Count > 0)
                {
                  var value = queue.Dequeue();
                  fieldMaker.MoveNext();

                  yield return new Node(NodeType.PropertyStart, fieldMaker.Current);
                  yield return new Node(NodeType.Value, value);
                  yield return new Node(NodeType.PropertyEnd);
                }

                yield return new Node(NodeType.ObjectEnd);
              }
            }
            yield break;
          }
      }
    }

    private IEnumerable<string> EmitFields()
    {
      foreach (var field in fields)
      {
        yield return field;
      }
      int index = fields.Length;
      while (true)
      {
        yield return $"field{++index}";
      }
    }

    public IEnumerable<Node> Complete()
    {
      yield break;
    }
  }
}