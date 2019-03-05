using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public static class PathArgs // : IArgs
  {
    public static HashMap<Var> ParseArgs(string path, string pathTemplate)
    {
      var matches = Regex.Matches(pathTemplate, @"\{([^{}]+)\}");
      var keys = matches.Cast<Match>().Select(x => x.Groups[1].Value).ToArray();

      var pathPattern = $"^{Regex.Replace(pathTemplate, @"(\{[^{}]+\})", @"([^/]+)")}";

      var match = Regex.Match(path, pathPattern);
      var values = match.Success
        ? match.Groups.Cast<Group>().Skip(1).Select(x => new Var(x.Value)).ToArray()
        : new Var[0];

      var map = new HashMap<Var>();
      for (int i = 0; i < keys.Length; i++)
      {
        var key = keys[i];
        var value = values.ElementAtOrDefault(i);

        map[key] = value;
      }
      return map;
    }

    //private readonly string pathTemplate;
    //private readonly HashMap<Var> args;

    //public PathArgs(string path, string pathTemplate)
    //{
    //  this.pathTemplate = pathTemplate;
    //  this.args = ParsePath(path, pathTemplate);
    //}

    //private HashMap<Var> ParsePath(string path, string pathTemplate)
    //{
    //  var matches = Regex.Matches(pathTemplate, @"\{([^{}]+)\}");
    //  var keys = matches.Cast<Match>().Select(x => x.Groups[1].Value).ToArray();

    //  var pathPattern = $"^{Regex.Replace(pathTemplate, @"(\{[^{}]+\})", @"([^/]+)")}";

    //  var match = Regex.Match(path, pathPattern);
    //  var values = match.Success
    //    ? match.Groups.Cast<Group>().Skip(1).Select(x => new Var(x.Value)).ToArray()
    //    : new Var[0];

    //  var map = new HashMap<Var>();
    //  for (int i = 0; i < keys.Length; i++)
    //  {
    //    var key = keys[i];
    //    var value = values.ElementAtOrDefault(i);

    //    map[key] = value;
    //  }
    //  return map;
    //}

    //public ICollection<string> Keys => args.Keys;

    //public ICollection<Var> Values => args.Values;

    //public Var this[string key]
    //{
    //  get => args[key];
    //  set => args[key] = value;
    //}

    //public override string ToString()
    //{
    //  try
    //  {
    //    var text = pathTemplate;
    //    foreach (var key in Keys)
    //    {
    //      var value = this[key]?.ToString() ?? "*";
    //      var param = $"{{{key}}}";
    //      text = text.Replace(param, value);
    //    }
    //    return text;
    //  }
    //  catch
    //  {
    //    return base.ToString();
    //  }
    //}

    //public IEnumerator<KeyValuePair<string, Var>> GetEnumerator()
    //{
    //  return args.GetEnumerator();
    //}

    //IEnumerator IEnumerable.GetEnumerator()
    //{
    //  return args.GetEnumerator();
    //}
  }
}
