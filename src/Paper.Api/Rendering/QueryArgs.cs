using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public static class QueryArgs // : IArgs
  {
    public static HashMap<Var> ParseArgs(string uri)
    {
      var map = new HashMap<Var>();
      var queryString = new UriString(uri);
      foreach (var name in queryString.GetArgNames())
      {
        var value = queryString.GetArg(name);
        map[name] = value is Var var ? var : new Var(value);
      }
      return map;
    }

    //private HashMap<Var> args = new HashMap<Var>();

    //public QueryArgs(string uri)
    //{
    //  var queryString = new UriString(uri);

    //  foreach (var name in queryString.GetArgNames())
    //  {
    //    var value = queryString.GetArg(name);
    //    args[name] = value is Var var ? var : new Var(value);
    //  }
    //}

    //public ICollection<string> Keys => args.Keys;

    //public ICollection<Var> Values => args.Values;

    //public Var this[string key]
    //{
    //  get => args[key];
    //  set => args[key] = value;
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
