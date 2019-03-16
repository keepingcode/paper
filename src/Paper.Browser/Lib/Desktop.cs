using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Toolset;

namespace Paper.Browser.Lib
{
  public class Desktop
  {
    public Desktop()
    {
      this.Host = new DesktopForm();
      this.Host.SearchBox.KeyUp += (o, e) =>
      {
        if (e.Modifiers != Keys.None || e.KeyCode != Keys.Enter)
          return;

        var url = this.Host.SearchBox.Text;
        if (string.IsNullOrWhiteSpace(url))
          return;

        this.CreateWindow().RequestAsync(url).NoAwait();
      };
    }

    public DesktopForm Host { get; }

    public List<Window> Windows = new List<Window>();

    public Window CreateWindow(Window current, string target)
    {
      var name = (target == TargetNames.Self) ? current?.Name : null;
      if (name == null)
      {
        name = Guid.NewGuid().ToString("B");
      }

      var window = Windows.FirstOrDefault(x => x.Name.EqualsIgnoreCase(name));
      if (window == null)
      {
        window = new Window(name, this);
      }

      return window;
    }
  }
}