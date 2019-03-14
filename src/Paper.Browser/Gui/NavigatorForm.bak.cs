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
  public partial class NavigatorForm : Form
  {
    public NavigatorForm(Navigator navigator)
    {
      Navigator = navigator;
      InitializeComponent();
      InitializeEvents();
      SetBounds();
      this.WindowState = FormWindowState.Maximized;
    }

    private void InitializeEvents()
    {
    }

    public Navigator Navigator { get; }

    private async Task NavigateAsync(string location = null)
    {
      if (location == null)
      {
        location = txLocation.Text.Trim();

        //TODO: Desativado apenas para os testes
        //txLocation.Text = null;
      }

      if (string.IsNullOrWhiteSpace(location))
      {
        return;
      }

      var window = Navigator.CreateWindow(TargetNames.Blank);
      await Navigator.NavigateAsync(location, window);
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

    private void txLocation_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter)
      {
        NavigateAsync().NoAwait();
      }
    }
  }
}