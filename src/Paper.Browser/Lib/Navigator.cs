using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Media;
using Toolset;
using Toolset.Net;

namespace Paper.Browser.Lib
{
  public class Navigator
  {
    private readonly object synclock = new object();

    private Navigator()
    {
      this.Form = new NavigatorForm(this);
    }

    public static Navigator Current { get; } = new Navigator();

    public NavigatorForm Form { get; }

    private string MakeFrameName()
    {
      return Guid.NewGuid().ToString("B");
    }

    public Window CreateWindow(string target, Window reference = null, IWin32Window parent = null)
    {
      string frameName;
      switch (target)
      {
        case TargetNames.Self:
          {
            frameName = reference?.Name ?? MakeFrameName();
            break;
          }
        case TargetNames.Blank:
          {
            frameName = MakeFrameName();
            break;
          }
        default:
          {
            frameName = target;
            break;
          }
      }

      var window = (
        from form in Application.OpenForms.OfType<WindowForm>()
        where form.Name.EqualsIgnoreCase(frameName)
        select form.Window
      ).FirstOrDefault();

      if (window == null)
      {
        window = new Window(frameName);
        window.Form.Show(parent ?? Form);
      }

      window.Form.Select();

      return window;
    }

    public async Task<Ret<Content>> RequestAsync(string uri, string method, Entity data)
    {
      Ret<Content> ret;
      try
      {
        var http = new HttpClient();
        ret = await http.RequestAsync(uri, method, data);
      }
      catch (Exception ex)
      {
        ret = Ret.Fail(uri, ex);
      }
      return ret;
    }

    private void InvokeIgnoringErrors<T>(Action<T> action, T argument)
    {
      try
      {
        action?.Invoke(argument);
      }
      catch (Exception ex)
      {
        // TODO: O que fazer com esse erro?
        ex.Trace();
      }
    }
  }
}