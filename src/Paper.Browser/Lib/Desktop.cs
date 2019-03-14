using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Browser.Gui;
using Toolset;

namespace Paper.Browser.Lib
{
  public class Desktop
  {
    public Desktop()
    {
      this.Host = new DesktopForm();
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