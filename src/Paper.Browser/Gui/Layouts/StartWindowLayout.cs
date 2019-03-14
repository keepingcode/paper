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
  public partial class StartWindowLayout : UserControl, IWindowLayout, IMessageSupport
  {
    private int? _progressPercent;

    public StartWindowLayout()
    {
      InitializeComponent();
    }

    public Control Host => this;

    public Panel ContentPane { get; }
    public ToolStrip ToolBar { get; }
    public ToolStrip ActionBar { get; }
    public StatusStrip StatusBar { get; }

    public Component StatusText => _statusText;
    public Component ProgressBar => _progressBar;
  }
}
