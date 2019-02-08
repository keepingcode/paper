using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Commons
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
      foreach (var node in DoFindExactMatches(path))
      {
        if (node != null)
        {
          node.Path = null;
          node.Value = null;
        }
      }
    }

    public IEnumerable<TValue> Find(string path)
    {
      var nodes = DoFindMatches(path);
      return nodes.Select(x => x.Value);
    }

    public IEnumerable<TValue> FindExact(string path)
    {
      var nodes = DoFindExactMatches(path);
      return nodes.Select(x => x.Value);
    }

    private IEnumerable<string> Tokenize(string path)
    {
      var tokens = path.ToLower().Split('/').NonNullOrEmpty();
      tokens = Normalize(tokens).Reverse();
      return tokens;
    }

    private IEnumerable<string> Normalize(IEnumerable<string> tokens)
    {
      int skip = 0;
      foreach (var token in tokens.Reverse())
      {
        if (token == ".")
        {
          continue;
        }
        if (token == "..")
        {
          skip++;
          continue;
        }
        if (skip > 0)
        {
          skip--;
          continue;
        }
        yield return token;
      }
    }

    private IEnumerable<Node<TValue>> DoFindMatches(string path)
    {
      var tokens = Tokenize(path);
      var trail = new Stack<Node<TValue>>();
      IEnumerable<Node<TValue>> nodes = entries.AsSingle();
      foreach (var token in tokens)
      {
        nodes = ExploreChildren(token, nodes);
        nodes.Where(x => x.Value != null).ForEach(trail.Push);
      }
      return trail;
    }

    private IEnumerable<Node<TValue>> DoFindExactMatches(string path)
    {
      var tokens = Tokenize(path);
      IEnumerable<Node<TValue>> nodes = entries.AsSingle();
      foreach (var token in tokens)
      {
        nodes = ExploreChildren(token, nodes);
        if (!nodes.Any())
        {
          break;
        }
      }
      return nodes.Where(x => x.Value != null);
    }

    private IEnumerable<Node<TValue>> ExploreChildren(string token, IEnumerable<Node<TValue>> nodes)
    {
      if (token == "*")
      {
        var allChildren = nodes.SelectMany(x => x.Values);
        foreach (var child in allChildren)
        {
          yield return child;
        }
      }
      else
      {
        foreach (var node in nodes)
        {
          var child = node[token] ?? node["*"];
          if (child != null)
          {
            yield return child;
          }
        }
      }
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