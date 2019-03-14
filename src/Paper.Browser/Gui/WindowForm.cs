using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui.Layouts;
using Paper.Browser.Gui.Widgets;
using Toolset.Reflection;

namespace Paper.Browser.Gui
{
  public partial class WindowForm : Form
  {
    private IWindowLayout _layout;

    public WindowForm()
    {
      InitializeComponent();

      this.WindowLayout = new FixedWindowLayout();

      // this.ActionBar.ItemAdded += (o, e) => ToolBarChanged(this.ActionBar);
      // this.ActionBar.ItemRemoved += (o, e) => ToolBarChanged(this.ActionBar);
      // this.ToolBar.ItemAdded += (o, e) => ToolBarChanged(this.ToolBar);
      // this.ToolBar.ItemRemoved += (o, e) => ToolBarChanged(this.ToolBar);
      // 
      // ToolBarChanged(this.ActionBar);
      // ToolBarChanged(this.ToolBar);
      // 
      // this.StatusBar.Visible = false;
    }

    public IWindowLayout WindowLayout
    {
      get => _layout;
      set
      {
        if (value != _layout)
        {
          _layout = value ?? new FixedWindowLayout();
          _layout.Format(this);
        }
      }
    }

    public string StatusMessage
    {
      get => StatusLabel.Text;
      set => StatusLabel.Text = value;
    }

    public int? ProgressPercent
    {
      get => ProgressBar.Value;
      set
      {
        int percent;
        ProgressBarStyle style;

        if (value == null)
        {
          percent = 0;
          style = ProgressBarStyle.Continuous;
        }
        else if (value >= 0)
        {
          percent = (value > 100) ? 100 : value.Value;
          style = ProgressBarStyle.Continuous;
        }
        else
        {
          percent = 0;
          style = ProgressBarStyle.Marquee;
        }

        ProgressBar.Value = percent;
        ProgressBar.Style = style;
      }
    }

    public WindowForm DisplayStatus(string message, TimeSpan? duration = null)
    {
      this.Call(() =>
      {
        timer.Enabled = false;
        StatusMessage = message;
        if (duration != null)
        {
          timer.Enabled = true;
          timer.Interval = (int)duration.Value.TotalMilliseconds;
        }
      });
      return this;
    }

    public WindowForm ClearStatus()
    {
      this.Call(() =>
      {
        timer.Enabled = false;
        StatusMessage = "";
      });
      return this;
    }

    public WindowForm DisplayProgress(int percent = -1)
    {
      this.Call(() => ProgressPercent = percent);
      return this;
    }

    public WindowForm ClearProgress()
    {
      this.Call(() => ProgressPercent = null);
      return this;
    }

    private void mnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
