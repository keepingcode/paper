using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui.Layouts;
using Paper.Browser.Lib;
using Paper.Media;

namespace Paper.Browser.Lib.Pages
{
  public interface IPage
  {
    Control Host { get; }

    Window Window { get; set; }

    Entity Entity { get; set; }
  }
}
