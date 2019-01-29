using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Toolset;
using Toolset.Collections;
using Toolset.Data;
using Toolset.Reflection;

namespace Toolset
{
  public class UriString
  {
    private class Parts
    {
      public string Protocol { get; set; }
      public string Host { get; set; }
      public string Path { get; set; }
      public HashMap Args { get; set; }
    }

    private readonly Parts parts;

    private UriString(Parts parts)
    {
      this.parts = parts;
    }

    public UriString(string uri, params object[] args)
    {
      this.parts = ParseParts(uri);
      ParseArgs(args)?.CopyTo(this.parts.Args);
    }

    public UriString(Uri uri, params object[] args)
    {
      this.parts = ParseParts(uri.ToString());
      ParseArgs(args)?.CopyTo(this.parts.Args);
    }

    public string Protocol => parts.Protocol;
    public string Host => parts.Host;
    public string Path => parts.Path;
    public string Args => Stringify(parts.Args);

    private Parts ParseParts(string uri)
    {
      Match match;

      var parts = new Parts();

      match = Regex.Match(uri, @"^(?:([^:]+://)([^/]+)?)?(.*)$");
      if (match.Success)
      {
        parts.Protocol = match.Groups[1].Value.NullIfEmpty();
        parts.Host = match.Groups[2].Value.NullIfEmpty();
        parts.Path = match.Groups[3].Value.NullIfEmpty();
      }
      else
      {
        parts.Path = uri.NullIfEmpty();
      }

      if (parts.Path != null)
      {
        var tokens = parts.Path.Split('?');

        parts.Path = tokens.First().NullIfEmpty();
        parts.Args = ParseArgs(tokens.Skip(1).FirstOrDefault());

        if (parts.Path?.EndsWith("/") == true)
        {
          parts.Path = parts.Path.Substring(0, parts.Path.Length - 1).NullIfEmpty();
        }
      }

      return parts;
    }

    private HashMap ParseArgs(params object[] args)
    {
      var map = new HashMap();
      foreach (var arg in args)
      {
        if (arg is string text)
        {
          ExpandArgs(text, map);
        }
        else
        {
          ExpandArgs(arg, map);
        }
      }
      return map;
    }

    private void ExpandArgs(string args, HashMap map)
    {
      if (string.IsNullOrWhiteSpace(args))
        return;

      if (args.Contains('?'))
      {
        args = string.Join("&", args.Split('?').Skip(1));
      }

      var cache = new HashMap();

      args = args.Split('?').Last();
      var tokens = args.Split('&');
      foreach (var token in tokens)
      {
        var parts = token.Split('=');
        var key = parts.First();
        var value = string.Join("=", parts.Skip(1));
        if (value == "")
        {
          value = "1";
        }

        var isRange = key.EndsWith(".min") || key.EndsWith(".max");
        var isArray = key.Contains("[]") || cache.ContainsKey(key);

        if (isRange)
        {
          var isMin = key.EndsWith(".min");
          key = Regex.Replace(key, "(.min|.max)$", "");

          var range = cache[key] as Range;
          if (range == null)
          {
            cache[key] = range = new Range();
          }

          range.Min = isMin ? value : range.Min;
          range.Max = isMin ? range.Max : value;
        }
        else if (isArray)
        {
          key = key.Replace("[]", "");

          List<object> items;

          if (cache.ContainsKey(key))
          {
            var current = cache[key];
            if (current is List<object> list)
            {
              items = list;
            }
            else
            {
              cache[key] = items = new List<object> { current };
            }
          }
          else
          {
            cache[key] = items = new List<object>();
          }

          items.Add(value);
        }
        else
        {
          cache[key] = value;
        }
      }

      foreach (var key in cache.Keys)
      {
        var value = cache[key];
        SetCompatibleMapValue(map, key, value);
      }
    }

    private void ExpandArgs(object graph, HashMap map)
    {
      if (graph == null)
        return;

      foreach (var key in graph._GetPropertyNames())
      {
        var value = graph._Get(key);
        SetCompatibleMapValue(map, key, value);
      }
    }

    private void SetCompatibleMapValue(HashMap map, string key, object value)
    {
      if (value == null)
      {
        map.Remove(key);
      }
      else
      {
        if (Range.IsRangeCompatible(value))
        {
          value = Range.CreateCompatibleRange(value);
        }
        else if (!(value is string) && value is IEnumerable items)
        {
          value = items.Cast<object>().ToList();
        }
        map[key] = value;
      }
    }

    private string Stringify(HashMap args)
    {
      if (args?.Any() != true)
        return "";

      var terms = new List<string>();
      foreach (var key in args.Keys)
      {
        var value = args[key];
        if (value == null)
          continue;

        if (value is Var var)
        {
          value = var.RawValue;
        }

        if (value is Range range)
        {
          var min = Change.To<string>(range.Min);
          var max = Change.To<string>(range.Max);
          if (!string.IsNullOrEmpty(min))
          {
            terms.Add($"{key}.min={min}");
          }
          if (!string.IsNullOrEmpty(max))
          {
            terms.Add($"{key}.max={max}");
          }
        }
        else if (!(value is string) && value is IEnumerable items)
        {
          foreach (var item in items.Cast<object>())
          {
            terms.Add($"{key}[]={Change.To<string>(item)}");
          }
        }
        else
        {
          terms.Add($"{key}={Change.To<string>(value)}");
        }
      }
      return $"?{string.Join("&", terms)}";
    }

    private UriString CloneWithModifications(params Parts[] modifications)
    {
      var target = new Parts();
      foreach (var part in this.parts.AsSingle().Concat(modifications))
      {
        target.Protocol = Coalesce(part.Protocol, target.Protocol);
        target.Host = Coalesce(part.Host, target.Host);
        if (!string.IsNullOrEmpty(part.Path))
        {
          if (target.Path == null || part.Path.StartsWith("/"))
          {
            target.Path = part.Path;
          }
          else
          {
            target.Path = $"{target.Path}/{part.Path}";
          }
        }
        part.Args?.CopyTo(target.Args ?? (target.Args = new HashMap()));
      }
      return new UriString(target);
    }

    public bool HasArg(string key)
    {
      return this.parts.Args?.Keys.Any(x => x.EqualsIgnoreCase(key)) == true;
    }

    public T GetArg<T>(string key, T defaultValue = default(T))
    {
      return Change.To<T>(GetArg(key) ?? defaultValue);
    }

    public object GetArg(string key, object defaultValue = null)
    {
      key = this.parts.Args?.Keys.FirstOrDefault(x => x.EqualsIgnoreCase(key));
      if (key == null)
        return defaultValue;

      var value = this.parts.Args[key];
      return value;
    }

    public ICollection<string> GetArgNames()
    {
      return this.parts.Args?.Keys ?? new string[0];
    }

    public UriString SetProtocol(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return this;

      if (!Regex.IsMatch(value, @"^[\w\d]+(://)?$"))
        throw new FormatException(@"Formato inválido para um protocolo. Exemplo de formato suportado: ""ftp"" e ""http://""");

      if (!value.EndsWith("://"))
      {
        value += "://";
      }

      var part = new Parts { Protocol = value };
      var clone = CloneWithModifications(part);
      return clone;
    }

    public UriString SetHost(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return this;

      if (!Regex.IsMatch(value, @"^[\w\d-._]+(:\d+)?$"))
        throw new FormatException(@"Formato inválido para um nome de host. Exemplo de formato suportado: ""host.com"" ""host.com:8080""");

      var part = new Parts { Host = value };
      var clone = CloneWithModifications(part);
      return clone;
    }

    public UriString SetPath(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return this;

      if (!Regex.IsMatch(value, @"^$|^[^?]+"))
        throw new FormatException(@"Formato inválido para um caminho de URI. Exemplo de formato suportado: ""usuarios/123""  ""/usuarios/123""");

      var part = new Parts();

      while (value?.EndsWith("/") == true)
      {
        value = value.Substring(0, value.Length - 1);
      }

      part.Path = value ?? "";

      var clone = CloneWithModifications(part);
      return clone;
    }

    public UriString SetArg(string key, object value)
    {
      var part = new Parts { Args = new HashMap() };
      SetCompatibleMapValue(part.Args, key, value);
      var clone = CloneWithModifications(part);
      return clone;
    }

    public UriString SetArgs(params string[] args)
    {
      var part = new Parts { Args = ParseArgs(args) };
      var clone = CloneWithModifications(part);
      return clone;
    }

    public UriString SetArgs(params object[] args)
    {
      var part = new Parts { Args = ParseArgs(args) };
      var clone = CloneWithModifications(part);
      return clone;
    }

    public UriString UnsetArgs()
    {
      var clone = CloneWithModifications();
      clone.parts.Args?.Clear();
      return clone;
    }

    public UriString UnsetArgs(params string[] argNames)
    {
      var clone = CloneWithModifications();
      var map = clone.parts.Args;
      if (map != null)
      {
        foreach (var argName in argNames)
        {
          map.Remove(argName);
        }
      }
      return clone;
    }

    public UriString UnsetArgsExcept(params string[] exceptArgNames)
    {
      var clone = CloneWithModifications();
      var map = clone.parts.Args;
      if (map != null)
      {
        var argNames = map.Keys.Where(x => !x.EqualsAnyIgnoreCase(exceptArgNames)).ToArray();
        foreach (var argName in argNames)
        {
          map.Remove(argName);
        }
      }
      return clone;
    }

    public UriString Combine(string uri)
    {
      var parts = ParseParts(uri);
      var clone = CloneWithModifications(parts);
      return clone;
    }

    public UriString Combine(string uriPart, string otherUriPart, params string[] otherUriParts)
    {
      var terms = uriPart.AsSingle().Append(otherUriPart).Concat(otherUriParts).ToArray();
      var parts = new Parts[terms.Length];
      for (int i = 0; i < parts.Length; i++)
      {
        var term = terms[i];
        if (i > 0 && term.StartsWith("/"))
        {
          term = term.Substring(1);
        }
        var part = ParseParts(term);
        parts[i] = part;
      }
      var clone = CloneWithModifications(parts);
      return clone;
    }

    public UriString Combine(UriString uri)
    {
      var clone = CloneWithModifications(uri.parts);
      return clone;
    }

    private string Coalesce(params string[] terms)
    {
      return terms.FirstOrDefault(x => !string.IsNullOrEmpty(x));
    }

    [Obsolete("No futuro UriString irá substituir Toolset.Route.")]
    public Toolset.Route ToRoute()
    {
      return new Toolset.Route(ToString());
    }

    public Uri ToUri()
    {
      return new Uri(ToString(), UriKind.RelativeOrAbsolute);
    }

    public override string ToString()
    {
      var args = Stringify(parts.Args);
      var uri = $"{parts.Protocol}{parts.Host}{parts.Path}{args}";
      return uri;
    }

    public static implicit operator UriString(string uri)
    {
      return new UriString(uri);
    }

    public static implicit operator UriString(Uri uri)
    {
      return new UriString(uri);
    }

    [Obsolete("No futuro UriString irá substituir Toolset.Route.")]
    public static implicit operator UriString(Toolset.Route uri)
    {
      return new UriString(uri.ToString());
    }

    public static implicit operator string(UriString uri)
    {
      return uri.ToString();
    }

    public static implicit operator Uri(UriString uri)
    {
      return new Uri(uri.ToString(), UriKind.RelativeOrAbsolute);
    }

    [Obsolete("No futuro UriString irá substituir Toolset.Route.")]
    public static implicit operator Toolset.Route(UriString uri)
    {
      return new Toolset.Route(uri.ToString());
    }
  }
}