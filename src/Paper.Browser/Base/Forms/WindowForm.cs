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
      FeedbackAutosize();
    }

    private void FeedbackAutosize()
    {
      this.MaximizeBox = !this.AutoSize;
    }

    private void WindowForm_AutoSizeChanged(object sender, EventArgs e)
    {
      FeedbackAutosize();
    }

    private void WindowForm_Resize(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Maximized)
      {
        this.WindowState = FormWindowState.Normal;
        this.Expand();
      }
    }
  }
}
