using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Paper.Media.Design.Papers;
using Sandbox.Bot.Helpers;
using Sandbox.Bot.Net;
using Toolset;

namespace Sandbox.Bot.Forms
{
  public partial class LauncherForm : Form
  {
    private CancellationTokenSource cancellationTokenSource;
    private volatile bool suspended;

    public LauncherForm()
    {
      this.cancellationTokenSource = new CancellationTokenSource();
      this.PaperClient = new PaperClient();

      InitializeComponent();
    }

    public PaperClient PaperClient { get; private set; }

    public Entity BlueprintEntity { get; private set; }

    public Blueprint Blueprint { get; private set; }

    private async void ConnectAsync()
    {
      while (!cancellationTokenSource.IsCancellationRequested)
      {
        try
        {
          if (suspended)
          {
            lbMessage.SetText("Aguardando configurações...");
            await Task.Delay(500);
            continue;
          }

          lbMessage.SetText("Localizando o servidor de dados...");

          var entity = await PaperClient.ReadAsync("/blueprint");

          if (cancellationTokenSource.IsCancellationRequested)
            break;
          if (suspended)
            continue;

          this.BlueprintEntity = entity;
          this.Blueprint = EntityParser.ParseEntity<Blueprint>(entity);

          if (cancellationTokenSource.IsCancellationRequested)
            break;
          if (suspended)
            continue;

          await DownloadAssetsAsync();

          this.DialogResult = DialogResult.OK;
          break;
        }
        catch (Exception ex)
        {
          ex.Trace();

          var causes = ex.GetCauseMessages().Select(x => $"• {x}");
          var detail = string.Join(Environment.NewLine, causes);

          lbDetail.SetText(detail);
          lbDetail.SetForeColor(Color.Firebrick);
        }

        if (cancellationTokenSource.IsCancellationRequested)
          break;
        if (suspended)
          continue;

        lbMessage.SetText("Não conectado. Uma nova tentativa será feita em segundos...");
        await Task.Delay(2000);
      }

      if (cancellationTokenSource.IsCancellationRequested)
      {
        DialogResult = DialogResult.Cancel;
      }
      this.Close();
    }

    private async Task DownloadAssetsAsync()
    {
      try
      {
        var bytes = await PaperClient.ReadBytesAsync("/favicon.ico");
        Favicon.Save(bytes);
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }

    private void ShowSettings()
    {
      try
      {
        suspended = true;
        using (var dialog = new SettingsDialog())
        {
          dialog.ShowDialog(this);
          this.PaperClient = new PaperClient();
        }
      }
      finally
      {
        suspended = false;
      }
    }

    private void StartDialog_Shown(object sender, EventArgs e)
    {
      ConnectAsync();
    }

    private void btCancel_Click(object sender, EventArgs e)
    {
      cancellationTokenSource.Cancel();
      lbMessage.Text = "Cancelando...";
      btCancel.Text = "Cancelando...";
      btCancel.Enabled = false;
    }

    private void btSettings_Click(object sender, EventArgs e)
    {
      ShowSettings();
    }
  }
}
