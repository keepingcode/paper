using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public class Args : HashMap<Var>
  {
    public static Args ParseQueryArgs(string uri)
    {
      var args = new Args();
      var queryString = new UriString(uri);
      foreach (var name in queryString.GetArgNames())
      {
        var value = queryString.GetArg(name);
        args[name] = value is Var var ? var : new Var(value);
      }
      return args;
    }

    public static Args ParsePathArgs(string path, string pathTemplate)
    {
      var matches = Regex.Matches(pathTemplate, @"\{([^{}]+)\}");
      var keys = matches.Cast<Match>().Select(x => x.Groups[1].Value).ToArray();

      var pathPattern = $"^{Regex.Replace(pathTemplate, @"(\{[^{}]+\})", @"([^/]+)")}";

      var match = Regex.Match(path, pathPattern);
      var values = match.Success
        ? match.Groups.Cast<Group>().Skip(1).Select(x => new Var(x.Value)).ToArray()
        : new Var[0];

      var args = new Args();
      for (int i = 0; i < keys.Length; i++)
      {
        var key = keys[i];
        var value = values.ElementAtOrDefault(i);

        args[key] = value;
      }
      return args;
    }
  }
}
