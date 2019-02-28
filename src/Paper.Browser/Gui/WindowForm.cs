using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Lib;
using Toolset;

namespace Paper.Browser.Gui
{
  public partial class WindowForm : Form
  {
    public WindowForm(Window window)
    {
      this.Window = window;

      InitializeComponent();
      this.Size = new Size(270, 150);

      StatusLabel.TextChanged += (o, e) => lbStatus.Text = StatusLabel.Text;

      Overlay = true;
    }

    public Window Window { get; }

    public bool Overlay
    {
      get => pnOverlay.Visible;
      set
      {
        pnOverlay.Visible = value;

        foreach (Control control in this.Controls)
        {
          control.Enabled = !pnOverlay.Visible;
        }
        ControlBox = !pnOverlay.Visible;

        pnOverlay.Left = 0;
        pnOverlay.Top = 0;
        pnOverlay.Width = this.ClientSize.Width;
        pnOverlay.Height = this.ClientSize.Height;
        pnOverlay.Enabled = pnOverlay.Visible;
        pnOverlay.BringToFront();
      }
    }

    public void Pack()
    {
      var control = PageContainer.Controls.Cast<Control>().FirstOrDefault();
      if (control == null)
        return;
      
      if (control.AutoSize)
      {
        control.Dock = DockStyle.Fill;
        this.AutoSize = false;
        this.PageContainer.Controls.Add(control);
        this.Expand();
      }
      else
      {
        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.AutoSize = true;
        this.PageContainer.Controls.Add(control);

        var size = this.Size;

        this.SuspendLayout();
        control.Dock = DockStyle.Fill;
        this.AutoSizeMode = AutoSizeMode.GrowOnly;
        this.AutoSize = false;
        this.MinimumSize = size;
        this.Size = size;
        this.FormBorderStyle = FormBorderStyle.Sizable;
        this.ResumeLayout();
      }
    }

    private void FeedbackMinimumSize()
    {
      btReduce.Enabled = (this.MinimumSize != Size.Empty);
    }

    private void btExpand_Click(object sender, EventArgs e)
    {
      this.Expand();
    }

    private void btReduce_Click(object sender, EventArgs e)
    {
      this.Reduce();
    }

    private void WindowForm_MinimumSizeChanged(object sender, EventArgs e)
    {
      FeedbackMinimumSize();
    }

    private void btViewSource_Click(object sender, EventArgs e)
    {
      Window.ViewSource();
    }

    private void mnRefresh_Click(object sender, EventArgs e)
    {
      Window.NavigateAsync(Window.Content.Href).NoAwait();
    }
  }
}
