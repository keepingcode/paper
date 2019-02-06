using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Base.Forms
{
  public partial class NavigatorForm : Form
  {
    public NavigatorForm()
    {
      InitializeComponent();
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
