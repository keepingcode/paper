using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui
{
  public partial class WidgetGrid : FlowLayoutPanel
  {
    public WidgetGrid()
    {
      InitializeComponent();
      this.ControlAdded += (o, e) => Pack();
      this.ControlRemoved += (o, e) => Pack();
    }

    public void Pack()
    {
      this.Size = WidgetGridLayout.Measure(Controls.Cast<IWidget>());
    }
  }
}
