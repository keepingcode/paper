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

    public async Task<Window> NavigateAsync(string uri, string target, Window reference = null)
    {
      var window = CreateWindow(target, reference);
      window.Invalidate();

      var http = new HttpClient();
      var ret = await http.RequestAsync(uri, MethodNames.Get);

      window.SetContent(ret);
      window.Validate();

      return window;
    }

    public Window CreateWindow(string target, Window reference = null)
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
        window.Form.Show(Form);
      }

      window.Form.Select();

      return window;
    }

    private string MakeFrameName()
    {
      return Guid.NewGuid().ToString("B");
    }
  }
}