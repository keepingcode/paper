using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Commons;

namespace Paper.Browser.Base.Forms
{
  public partial class WindowForm : Form
  {
    public WindowForm()
    {
      InitializeComponent();
      Overlay = true;
      StatusLabel.TextChanged += (o, e) => lbStatus.Text = StatusLabel.Text;
    }

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
        pnOverlay.BringToFront();
        pnOverlay.Enabled = pnOverlay.Visible;
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
  }
}
