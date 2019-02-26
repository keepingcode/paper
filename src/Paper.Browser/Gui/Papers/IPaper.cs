using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Lib;

namespace Paper.Browser.Gui.Papers
{
  public interface IPaper
  {
    Control Control { get; }

    Window Window { get; }

    Content Content { get; }
  }
}
