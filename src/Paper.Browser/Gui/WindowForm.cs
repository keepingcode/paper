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
  public partial class WindowForm : Form, IWindowLayout
  {
    private IWindowLayout _layout;

    public WindowForm()
    {
      InitializeComponent();

      this.Layout = new StartWindowLayout();

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

    public Control Host => this;

    public Panel ContentPane => Layout.ContentPane;
    public ToolStrip ToolBar => Layout.ToolBar;
    public ToolStrip ActionBar => Layout.ActionBar;
    public StatusStrip StatusBar => Layout.StatusBar;

    public IWindowLayout Layout
    {
      get => _layout;
      set
      {
        _layout = value ?? new FlexWindowLayout();

        this.Controls.Clear();
        this.Controls.Add(_layout.Host);

        if (_layout is FlexWindowLayout)
        {
          _layout.Host.Dock = DockStyle.Fill;
          this.AutoSize = false;
          this.FormBorderStyle = FormBorderStyle.Sizable;
          this.Expand();
        }
        else
        {
          this.AutoSize = true;
          this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
          this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
      }
    }

    public IMessageSupport Messager => Layout as IMessageSupport;

    public string StatusMessage
    {
      get => Messager?.StatusText?._Get<string>("Text");
      set => Messager?.StatusText?._Set("Text", value);
    }

    public int? ProgressPercent
    {
      get => Messager?.ProgressBar?._Get<int>("Value");
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
          percent = value.Value;
          style = ProgressBarStyle.Continuous;
        }
        else
        {
          percent = 0;
          style = ProgressBarStyle.Marquee;
        }

        Messager?.ProgressBar?._Set("Value", percent);
        Messager?.ProgressBar?._Set("Style", style);
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
