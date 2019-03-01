using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Toolset;
using Toolset.Net;

namespace Paper.Browser.Lib
{
  public static class NavigatorExtensions
  {
    public static Window CreateWindow(this Navigator navigator, Target target, Window reference = null, IWin32Window parent = null)
    {
      return navigator.CreateWindow(target.GetName(), reference, parent);
    }

    public static async Task<Ret> NavigateAsync(this Navigator navigator, string uri, Window window)
    {
      try
      {
        window.SetBusy(true);
        var ret = await navigator.RequestAsync(uri, MethodNames.Get, null);
        window.SetContent(ret);
        window.SetBusy(false);
        return true;
      }
      catch (Exception ex)
      {
        return ex;
      }
    }

    public static async Task<Ret<Content>> RequestAsync(this Navigator navigator, string uri, Method method, Entity data)
    {
      return await navigator.RequestAsync(uri, method.GetName(), data);
    }

    public static async Task<Ret<Content>> RequestAsync(this Navigator navigator, string uri, Method method, Payload data)
    {
      var entity = data.ToEntity();
      return await navigator.RequestAsync(uri, method.GetName(), entity);
    }

    public static async Task<Ret<Content>> RequestAsync(this Navigator navigator, string uri, string method, Payload data)
    {
      var entity = data.ToEntity();
      return await navigator.RequestAsync(uri, method, entity);
    }
  }
}