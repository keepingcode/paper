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

namespace Sandbox.Bot.Forms
{
  public partial class SettingsDialog : Form
  {
    public SettingsDialog()
    {
      InitializeComponent();
      tvSections.ExpandAll();
    }

    private bool CheckForChanges()
    {
      bool hasChanges;
      try
      {
        var port = Change.To<int>(txPort.Text);
        hasChanges = txHost.Text != Settings.Host || port != Settings.Port;
      }
      catch
      {
        hasChanges = true;
      }
      btSave.Enabled = hasChanges;

      return hasChanges;
    }

    private void LoadSettings()
    {
      txHost.Text = Settings.Host;
      txPort.Text = Settings.Port.ToString();
      CheckForChanges();
    }

    private bool SaveChanges()
    {
      Settings.Host = txHost.Text;

      try
      {
        Settings.Port = Change.To<int>(txPort.Text);
      }
      catch (Exception ex)
      {
        var ln = Environment.NewLine;
        var causes = string.Join(ln, ex.GetCauseMessages().Select(x => $"• {x}"));
        var message = $"O valor indicado para a porta não é um número de porta válido.{ln}{ln}Causa: {ln}{causes}";
        using (var dialog = new FaultDialog(Ret.Fail(new Exception(message, ex))))
        {
          dialog.ShowDialog(this);
        }
        return false;
      }

      var hasChanges = CheckForChanges();
      return !hasChanges;
    }

    private void tvSections_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
    {
      e.Cancel = true;
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
          var ok = SaveChanges();
          e.Cancel = !ok;
        }
        else
        {
          e.Cancel = (result == DialogResult.Cancel);
        }
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

    private void txEndpoint_TextChanged(object sender, EventArgs e)
    {
      CheckForChanges();
    }

    private void txPort_TextChanged(object sender, EventArgs e)
    {
      CheckForChanges();
    }
  }
}
