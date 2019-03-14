using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui.Layouts
{
  public class FixedWindowLayout : IWindowLayout
  {
    public void Format(WindowForm window)
    {
      window.AutoSize = true;
      window.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      window.LayoutPane.Dock = DockStyle.None;
      window.LayoutPane.AutoSize = true;
      window.LayoutPane.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      window.ContentPane.Dock = DockStyle.None;
      window.ContentPane.AutoSize = true;
      window.ContentPane.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }
  }
}