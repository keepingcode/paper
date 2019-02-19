using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Api.Extensions.Papers
{
  public class Result
  {
    public Result(IPaper paper)
    {
    }

    public Result(Uri location)
    {
    }

    public Result(string location)
    {
    }

    public static implicit operator Result(IPaper paper)
    {
      return new Result(paper);
    }

    public static implicit operator Result(Uri location)
    {
      return new Result(location);
    }

    public static implicit operator Result(string location)
    {
      return new Result(location);
    }
  }
}
