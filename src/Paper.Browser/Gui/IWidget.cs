using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;

namespace Paper.Browser.Gui
{
  public interface IWidget
  {
    UserControl Host { get; }

    Label Label { get; }

    IContainer Components { get; }

    object Value { get; set; }

    Extent GridExtent { get; }
  }
}
