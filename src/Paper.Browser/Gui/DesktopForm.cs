using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolset;

namespace Paper.Browser.Gui
{
  public partial class DesktopForm : Form
  {
    public DesktopForm()
    {
      InitializeComponent();

      this.Bounds = GetBounds();
      this.WindowState = FormWindowState.Maximized;
    }

    private Rectangle GetBounds()
    {
      try
      {
        var screen = Screen.FromControl(this);
        var area = screen.WorkingArea;
        return new Rectangle(
          area.Left + area.Width - this.Width + 9,
          area.Top + 1,
          this.Width,
          area.Height
        );
      }
      catch (Exception ex)
      {
        ex.Trace();
        return this.Bounds;
      }
    }
  }
}