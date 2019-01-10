using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Toolset.Reflection;

namespace Sandbox.Host.Core
{
  public class UriString
  {
    private string _protocol;
    private string _host;
    private string _path;
    private string _args;
    private string _verb;

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
      get => _args;
      set
      {
        if (!string.IsNullOrEmpty(value) && !Regex.IsMatch(value, @"^$|^/[^?]+"))
          throw new FormatException(@"Formato inválido para argumentos de URI. Exemplo de formato suportado: ""?id=10&nome=dez""");

        _args = value ?? "";
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

    public UriString Combine(UriString uri)
    {
      var clone = new UriString(this);
      foreach (var property in uri._GetPropertyNames())
      {
        var value = uri._Get<string>(property);
        if (!string.IsNullOrEmpty(value))
        {
          clone._Set(property, value);
        }
      }
      return clone;
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
