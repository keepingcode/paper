using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox.Bot.Forms
{
  public partial class SettingsDialog : Form
  {
    public SettingsDialog()
    {
      InitializeComponent();
      tvSections.ExpandAll();
    }

    private void LoadSettings()
    {
      txEndpoint.Text = Settings.Endpoint;
      CheckForChanges();
    }

    private void SaveChanges()
    {
      Settings.Endpoint = txEndpoint.Text;
      CheckForChanges();
    }

    private bool CheckForChanges()
    {
      var hasChanges = txEndpoint.Text != Settings.Endpoint;
      btSave.Enabled = hasChanges;
      return hasChanges;
    }

    private void tvSections_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
    {
      e.Cancel = true;
    }

    private void txEndpoint_TextChanged(object sender, EventArgs e)
    {
      CheckForChanges();
    }

    private void SettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      var hasChanges = CheckForChanges();
      if (hasChanges)
      {
        var result = MessageBox.Show(
          $"Existem alterações pendentes.{Environment.NewLine}{Environment.NewLine}Deseja aplicá-las antes de sair?",
          this.Text,
          MessageBoxButtons.YesNoCancel,
          MessageBoxIcon.Exclamation,
          MessageBoxDefaultButton.Button3
        );

        if (result == DialogResult.Yes)
        {
          SaveChanges();
        }

        e.Cancel = (result == DialogResult.Cancel);
      }
    }

    private void btSave_Click(object sender, EventArgs e)
    {
      SaveChanges();
    }

    private void SettingsDialog_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void btClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
