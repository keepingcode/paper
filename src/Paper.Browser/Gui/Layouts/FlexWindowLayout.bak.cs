using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui.Layouts
{
  public partial class FlexWindowLayout : UserControl, IWindowLayout, IMessageSupport
  {
    public FlexWindowLayout()
    {
      InitializeComponent();
    }

    public Control Host => this;

    public Panel ContentPane => _contentPane;
    public ToolStrip ToolBar => _toolBar;
    public ToolStrip ActionBar => _actionBar;
    public StatusStrip StatusBar => _statusBar;

    public Component StatusText => _statusText;
    public Component ProgressBar => _progressBar;
  }
}
