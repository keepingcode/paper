﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Api.Rendering
{
  public class Args : IArgs
  {
    private IArgs[] args;

    public Args(params IArgs[] args)
    {
      this.args = args;
    }

    public ICollection<string> Keys => args.SelectMany(x => x.Keys).Distinct().ToArray();

    public Var this[string key] => args.Select(x => x[key]).NonNull().FirstOrDefault();
  }
}
