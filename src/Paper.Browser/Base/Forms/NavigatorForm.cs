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
using Toolset;

namespace Paper.Browser.Base.Forms
{
  public partial class NavigatorForm : Form
  {
    public NavigatorForm()
    {
      InitializeComponent();

      SetBounds();
      this.WindowState = FormWindowState.Maximized;
    }

    private void SetBounds()
    {
      try
      {
        var screen = Screen.FromControl(this);
        var area = screen.WorkingArea;
        this.Bounds = new Rectangle(
          area.Left + area.Width - this.Width,
          area.Top,
          this.Width,
          area.Height
        );
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }

    public void OpenSettings()
    {
      new SettingsDialog().ShowDialog(this);
    }

    private void mnClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void mnSettings_Click(object sender, EventArgs e)
    {
      OpenSettings();
    }
  }
}
