using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Lib
{
  public static class NavigatorExtensions
  {
    public static async Task<Window> NavigateAsync(this Navigator navigator, string uri, Target target, Window reference = null)
    {
      return await navigator.NavigateAsync(uri, target.GetName(), reference);
    }
  }
}