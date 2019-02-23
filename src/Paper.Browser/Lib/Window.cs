using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;

namespace Paper.Browser.Lib
{
  public class Window
  {
    private readonly object synclock = new object();

    public Window(string name)
    {
      this.Name = name;
      this.Form = new WindowForm(this);
    }

    public WindowForm Form { get; }

    public string Name { get; }

    public async Task<Window> NavigateAsync(string uri, string target = TargetNames.Self)
    {
      return await Navigator.Current.NavigateAsync(uri, target, this);
    }
  }
}