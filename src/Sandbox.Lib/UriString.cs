using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Toolset.Collections;
using Toolset.Reflection;

namespace Sandbox.Lib
{
  public class UriString
  {
    private string _protocol;
    private string _host;
    private string _path;
    private string _args;
    private string _verb;

    private HashMap argMap;

    public UriString()
    {
    }

    public UriString(string uri)
    {
      Parse(uri);
    }

    public string Protocol
    {
      get => _protocol;
      set
      {
        if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, @":/*$"))
          throw new FormatException(@"Formato inválido para um protocolo. Exemplo de formato suportado: ""about:"" e ""http://""");

        _protocol = value ?? "";
      }
    }

    public string Host
    {
      get => _host;
      set
      {
        if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, @"^[\w\d-._]+(:\d+)?$"))
          throw new FormatException(@"Formato inválido para um nome de host. Exemplo de formato suportado: ""host.com"" ""host.com:8080""");

        _host = value ?? "";
      }
    }

    public string Path
    {
      get => _path;
      set
      {
        if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, @"^$|^/[^?]+"))
          throw new FormatException(@"Formato inválido para um caminho de URI. Exemplo de formato suportado: ""/api/usarios/123""");

        value = value ?? "";

        var match = Regex.Match(value, @"(.*)(/:[\w\d-._]+)$");
        if (match.Success)
        {
          _path = match.Groups[1].Value;
          _verb = match.Groups[2].Value;
        }
        else
        {
          _path = value;
        }
      }
    }

    public Uri ToUri()
    {
      return new Uri(this.ToString(), UriKind.RelativeOrAbsolute);
    }

    public string Args
    {
      get => _args ?? (_args = WrapArgs());
      set
      {
        if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, @"^$|^/[^?]+"))
          throw new FormatException(@"Formato inválido para argumentos de URI. Exemplo de formato suportado: ""?id=10&nome=dez""");

        argMap = new HashMap();
        UnwrapArgs(value ?? "");
      }
    }

    public string Verb
    {
      get => _verb;
      set
      {
        if (!string.IsNullOrEmpty(value) && !value.StartsWith(@"^(/?:)?[\w\d-._]+$"))
          throw new FormatException(@"Formato inválido para verbo de URI. Exemplo de formato suportado: ""editar"", "":editar"" e ""/:editar""");

        _verb = value;
      }
    }

    public UriString SetArgs(string args)
    {
      UnwrapArgs(args);
      return this;
    }

    public UriString SetArgs(object graph)
    {
      if (graph is string args)
      {
        UnwrapArgs(args);
      }
      else
      {
        foreach (var key in graph._GetPropertyNames())
        {
          var value = graph._Get(key);
          if (value != null)
          {
            if (!(value is string) && value is IEnumerable items)
            {
              value = items.Cast<object>().ToList();
            }
            this.argMap[key] = value;
          }
        }
      }
      return this;
    }

    public UriString Combine(UriString uri)
    {
      foreach (var property in uri._GetPropertyNames())
      {
        var value = uri._Get<string>(property);
        if (!string.IsNullOrEmpty(value))
        {
          this._Set(property, value);
        }
      }
      return this;
    }

    public UriString Clone()
    {
      return new UriString(this);
    }

    private void UnwrapArgs(string args)
    {
      if (string.IsNullOrWhiteSpace(args))
        return;

      var map = new HashMap();

      args = args.Split('?').Last();
      var tokens = args.Split('&');
      foreach (var token in tokens)
      {
        var parts = token.Split('=');
        var key = parts.First();
        var value = parts.Skip(1).LastOrDefault() ?? "1";

        var isArray = key.Contains("[]") || map.ContainsKey(key);
        if (isArray)
        {
          key = key.Replace("[]", "");

          List<object> items;

          if (map.ContainsKey(key))
          {
            var current = map[key];
            if (current is List<object> list)
            {
              items = list;
            }
            else
            {
              map[key] = items = new List<object> { current };
            }
          }
          else
          {
            map[key] = items = new List<object>();
          }

          items.Add(value);
        }
        else
        {
          map[key] = value;
        }
      }

      if (map.Count > 0)
      {
        this._args = null;
        foreach (var key in map.Keys)
        {
          this.argMap[key] = map[key];
        }
      }
    }

    private string WrapArgs()
    {
      var terms = new List<string>();
      foreach (var key in argMap.Keys)
      {
        var value = argMap[key];
        if (value == null)
          continue;

        if (!(value is string) && value is IEnumerable items)
        {
          foreach (var item in items.Cast<object>())
          {
            terms.Add($"{key}[]={GetString(item)}");
          }
        }
        else
        {
          terms.Add($"{key}={GetString(value)}");
        }
      }

      return terms.Count > 0 ? $"?{string.Join("&", terms)}" : "";
    }

    private string GetString(object value)
    {
      if (value is DateTime date)
      {
        return date.ToString("yyyy-MM-ddTHH:mm:ss");
      }
      else
      {
        return value?.ToString();
      }
    }

    private void Parse(string uri)
    {
      Match match;

      var protocol = "";
      var host = "";
      var path = "";
      var args = "";
      var verb = "";

      match = Regex.Match(uri, @"(?:(.*://)([^/]+))?(.*)");
      if (match.Success)
      {
        protocol = match.Groups[1].Value;
        host = match.Groups[2].Value;
        path = match.Groups[3].Value;
      }
      else
      {
        path = uri;
      }

      var tokens = path.Split('?');

      path = tokens.First();
      args = $"?{string.Join("&", tokens.Skip(1))}";

      if (args == "?")
      {
        args = "";
      }

      if (path.EndsWith("/"))
      {
        path = path.Substring(0, path.Length - 1);
      }

      match = Regex.Match(path, @"(.*)(/:[\w\d-._]+)$");
      if (match.Success)
      {
        path = match.Groups[1].Value;
        verb = match.Groups[2].Value;
      }

      this.Protocol = protocol;
      this.Host = host;
      this.Path = path;
      this.Args = args;
      this.Verb = verb;
    }

    public override string ToString()
    {
      return $"{Protocol}{Host}{Path}{Verb}{Args}";
    }

    public static implicit operator string(UriString uri)
    {
      return uri.ToString();
    }

    public static implicit operator UriString(string uri)
    {
      return new UriString(uri);
    }

    public static implicit operator Uri(UriString uri)
    {
      return uri.ToUri();
    }

    public static implicit operator UriString(Uri uri)
    {
      return new UriString(uri.ToString());
    }
  }
}
