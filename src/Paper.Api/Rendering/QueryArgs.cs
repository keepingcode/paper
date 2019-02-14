using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public class QueryArgs : IArgs
  {
    private HashMap<Var> args = new HashMap<Var>();

    public QueryArgs(string uri)
    {
      var queryString = new UriString(uri);

      foreach (var name in queryString.GetArgNames())
      {
        var value = queryString.GetArg(name);
        args[name] = value is Var var ? var : new Var(value);
      }
    }

    public ICollection<string> Keys => args.Keys;

    public Var this[string key]
    {
      get => args[key];
      set => args[key] = value;
    }
  }
}
