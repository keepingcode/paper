using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Lib
{
  public static class TargetExtensions
  {
    public static string GetName(this Target target)
    {
      return $"_{target.ToString().ToLower()}";
    }
  }
}