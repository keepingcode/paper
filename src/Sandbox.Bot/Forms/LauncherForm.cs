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
using Sandbox.Bot.Api;
using Sandbox.Bot.Helpers;
using Toolset;

namespace Sandbox.Bot.Forms
{
  public partial class LauncherForm : Form
  {
    private readonly CancellationTokenSource cancellation;
    private readonly MediaClient client;

    private volatile bool suspended;

    public LauncherForm()
    {
      this.cancellation = new CancellationTokenSource();
      this.client = MediaClient.Current;

      InitializeComponent();
    }

    public Entity BlueprintEntity { get; private set; }

    public Blueprint Blueprint { get; private set; }

    private async void ConnectAsync()
    {
      while (!cancellation.IsCancellationRequested)
      {
        try
        {
          await Task.Delay(500);

          if (suspended)
          {
            lbMessage.SetText("Aguardando configurações...");
            continue;
          }

          lbMessage.SetText("Localizando o servidor de dados...");

          var entity = await client.TransferAsync("Blueprint");
          if (!entity.OK)
            throw entity.Fault ?? new Exception(entity.FaultMessage);

          if (cancellation.IsCancellationRequested)
            break;
          if (suspended)
            continue;

          this.BlueprintEntity = entity.Value;
          this.Blueprint = EntityParser.ParseEntity<Blueprint>(entity.Value);

          if (cancellation.IsCancellationRequested)
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

        if (cancellation.IsCancellationRequested)
          break;
        if (suspended)
          continue;

        lbMessage.SetText("Não conectado. Uma nova tentativa será feita em segundos...");
        await Task.Delay(1500);
      }

      if (cancellation.IsCancellationRequested)
      {
        DialogResult = DialogResult.Cancel;
      }
      this.Close();
    }

    private async Task DownloadAssetsAsync()
    {
      try
      {
        var bytes = await client.TransferBytesAsync("/favicon.ico");
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
      cancellation.Cancel();
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
