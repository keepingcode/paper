using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Gui.Layouts
{
  public static class WindowLayouts
  {
    public static readonly IWindowLayout Fixed = new FixedWindowLayout();
    public static readonly IWindowLayout Flex = new FlexWindowLayout();
  }
}
