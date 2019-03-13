using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Toolset.Net;

namespace Paper.Browser.Lib
{
  public static class WindowExtensions
  {
    public static async Task RequestAsync(this Window window, string uri)
    {
      await window.RequestAsync(uri, MethodNames.Get, TargetNames.Self, data: null);
    }

    public static async Task RequestAsync(this Window window, string uri, string target)
    {
      await window.RequestAsync(uri, MethodNames.Get, target, data: null);
    }

    public static async Task RequestAsync(this Window window, string uri, Target target)
    {
      await window.RequestAsync(uri, MethodNames.Get, target.GetName(), data: null);
    }

    public static async Task RequestAsync(this Window window, string uri, Entity data)
    {
      await window.RequestAsync(uri, MethodNames.Post, TargetNames.Blank, data);
    }

    public static async Task RequestAsync(this Window window, string uri, Method method, Target target, Entity data)
    {
      await window.RequestAsync(uri, method.GetName(), target.GetName(), data);
    }
  }
}
