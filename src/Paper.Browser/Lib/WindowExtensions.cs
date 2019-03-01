using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Lib
{
  public static class WindowExtensions
  {
    public static async Task NavigateAsync(this Window window, string uri, Target target)
    {
      await window.NavigateAsync(uri, target.GetName());
    }
  }
}
