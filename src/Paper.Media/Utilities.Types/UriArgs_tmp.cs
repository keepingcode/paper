using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;
using Toolset.Data;
using Toolset.Reflection;

namespace Paper.Media.Utilities.Types
{
  public class UriArgs_tmp : IEnumerable<KeyValuePair<string, object>>
  {
    private readonly HashMap<object> items;
    private readonly List<string> keys;

    public UriArgs_tmp()
    {
      this.items = new HashMap<object>();
      this.keys = new List<string>();
    }

    public UriArgs_tmp(UriArgs_tmp items)
    {
      this.items = new HashMap<object>(items);
      this.keys = new List<string>(items.keys);
    }

    /// <summary>
    /// Quantidade de argumentos definidos.
    /// </summary>
    public int Count => keys.Count;

    /// <summary>
    /// Nomes dos argumentos definidos.
    /// </summary>
    public ICollection<string> Keys => keys;

    /// <summary>
    /// Obtém o argumento da posição.
    /// </summary>
    /// <param name="index">A posição do argumento.</param>
    /// <returns>O valor do argumento.</returns>
    public object this[int index]
    {
      get => Get(keys.ElementAt(index));
      set
      {
        var key = keys.ElementAt(index);
        if (key == null)
          throw new IndexOutOfRangeException($"Índice: {index}");

        Set(key, value);
      }
    }

    /// <summary>
    /// Obtém o valor do argumento.
    /// </summary>
    /// <param name="key">Nome do argumento.</param>
    /// <returns>O valor do argumento.</returns>
    public object this[string key]
    {
      get => Get(key);
      set => Set(key, value);
    }

    private object Get(string key)
    {
      if (key == null)
        return null;

      object value = this;
      foreach (var token in key.Split('.'))
      {
        if (value == null)
        {
          value = null;
          break;
        }

        while (value is IVar var)
        {
          value = var.Value;
        }

        if (value is UriArgs_tmp map)
        {
          value = map.items[token];
        }
        else
        {
          value = value?._Get(token);
        }
      }
      return value;
    }

    private void Set(string key, object value)
    {
      ParseArg(key, value, this);
    }

    public string ToUri(UriTemplate template = null)
    {
      throw new NotImplementedException();
    }

    private static void ParseArg(string key, object value, UriArgs_tmp target)
    {
      var isList = key.EndsWith("[]");
      if (isList)
      {
        key = key.Substring(0, key.Length - 2);
      }

      var isMin = key.EndsWithIgnoreCase(".min");
      var isMax = key.EndsWithIgnoreCase(".max");
      var isRange = isMin || isMax;
      if (isRange)
      {
        key = key.Substring(0, key.Length - 4);
      }

      var tokens = key.Split('.');
      var parents = tokens.Take(tokens.Length - 1);
      var current = tokens.Last();

      foreach (var parent in parents)
      {
        if (!(target.items[parent] is UriArgs_tmp))
        {
          target.items[parent] = new UriArgs_tmp();
        }
        target = (UriArgs_tmp)target.items[parent];
      }

      if (isRange)
      {
        var range = (Range)target.items[current];
        var min = isMin ? value : range.Min;
        var max = isMax ? value : range.Max;
        target.items[current] = new Range(min, max);
      }
      else if (isList)
      {
        var list = target.items[current] as List<object>;
        if (list == null)
        {
          target.items[current] = (list = new List<object>());
        }
        if (value != null)
        {
          list.Add(value);
        }
      }
      else
      {
        target.items[current] = value;
      }
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return items.GetEnumerator();
    }

    public static UriArgs_tmp ParseUri(string uri, string uriTemplate = null)
    {
      var tokens = (uri ?? "").Split('?');
      var path = tokens.First();
      var queryString = string.Join("?", tokens.Skip(1));

      if (!string.IsNullOrEmpty(uriTemplate))
      {
      }

      if (!string.IsNullOrEmpty(queryString))
      {
      }

      throw new NotImplementedException();
    }
  }
}