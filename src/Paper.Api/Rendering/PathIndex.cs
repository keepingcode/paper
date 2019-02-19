using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public class PathIndex<TValue>
    where TValue : class
  {
    private readonly Node<TValue> entries = new Node<TValue>(null, null);

    private IEnumerable<Node<TValue>> Traverse(Node<TValue> node)
    {
      if (node != entries)
      {
        yield return node;
      }
      foreach (var entry in node)
      {
        foreach (var child in Traverse(entry.Value))
        {
          yield return child;
        }
      }
    }

    public IEnumerable<string> GetPaths()
    {
      foreach (var node in Traverse(entries))
      {
        yield return node.Path;
      }
    }

    public void Visit(Action<string, TValue> visitor)
    {
      foreach (var node in Traverse(entries))
      {
        foreach (var child in node.Values)
        {
          visitor.Invoke(node.Path, child.Value);
        }
      }
    }

    public void Add(string path, TValue value)
    {
      Insert(path, value, overwrite: false);
    }

    public void Set(string path, TValue value)
    {
      Insert(path, value, overwrite: true);
    }

    private void Insert(string path, TValue value, bool overwrite)
    {
      Node<TValue> node = entries;
      foreach (var token in path.ToLower().Split('/').NonNullOrEmpty())
      {
        var key = token.Contains("{") ? "*" : token;
        if (!node.ContainsKey(key))
        {
          node[key] = new Node<TValue>(node, key);
        }
        node = node[key];
      }

      if (node.Value != null)
      {
        if (node.Value.Equals(value))
          return;

        if (!overwrite)
          throw new Exception(
            $"Não é possível mapear a entrada de índice '{path}' " +
            $"para '{(value?.ToString() ?? "null")}' " +
            $"porque ela já está mapeada para '{node.Value}'"
          );
      }

      node.Path = path;
      node.Value = value;
    }

    public void Remove(string path)
    {
      var node = FindNodeExact(path);
      if (node != null)
      {
        node.Path = null;
        node.Value = null;
      }
    }

    public TValue FindExact(string path)
    {
      var node = FindNodeExact(path);
      return node?.Value;
    }

    public IEnumerable<TValue> Find(string path)
    {
      var nodes = FindNodeMatches(path);
      return nodes.Select(x => x.Value);
    }

    private Node<TValue> FindNodeExact(string path)
    {
      Node<TValue> index = entries;
      foreach (var token in path.ToLower().Split('/').NonNullOrEmpty())
      {
        var key = index.ContainsKey(token) ? token : index.ContainsKey("*") ? "*" : null;
        if (key == null)
          return null;
        index = index[key];
      }
      return index;
    }

    private IEnumerable<Node<TValue>> FindNodeMatches(string path)
    {
      var tokens = path.ToLower().Split('/').NonNullOrEmpty();
      var trail = new Stack<Node<TValue>>();
      var step = entries;
      foreach (var token in tokens)
      {
        var node = step[token] ?? step["*"];
        if (node == null)
          break;

        if (node.Value != null)
        {
          trail.Push(node);
        }

        step = node;
      }
      return trail;
    }

    public class Node<T> : Map<string, Node<T>>
      where T : class
    {
      public Node(Node<T> parent, string key)
      {
        this.Key = key;
      }

      public Node<T> Parent { get; }

      public string Key { get; }

      public bool IsRoot => (Parent == null);

      public string Path { get; set; }

      public T Value { get; set; }
    }
  }
}