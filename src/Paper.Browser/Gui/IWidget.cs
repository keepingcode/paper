using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Paper.Browser.Gui
{
  public interface IWidget
  {
    string Text { get; set; }

    object Tag { get; set; }

    object Value { get; set; }

    Size GridExtent { get; set; }
  }
}
