using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui.Layouts
{
  public class FlexWindowLayout : IWindowLayout
  {
    public void Format(WindowForm window)
    {
      window.AutoSize = false;
      window.LayoutPane.Dock = DockStyle.Fill;
      window.LayoutPane.AutoSize = false;
      window.ContentPane.Dock = DockStyle.Fill;
      window.ContentPane.AutoSize = false;

      var offset = (window.Mode == Lib.Mode.SelectBox) ? new Size(-150, -150) : Size.Empty;
      window.Expand(offset);
    }
  }
}
