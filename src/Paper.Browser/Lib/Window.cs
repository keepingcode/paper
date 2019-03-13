using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Layouts;
using Paper.Browser.Gui.Widgets;
using Paper.Browser.Lib.Pages;
using Paper.Media;
using Paper.Media.Design;
using Toolset;
using Toolset.Net;

namespace Paper.Browser.Lib
{
  public class Window
  {
    public Window(string name, Desktop desktop)
    {
      this.Name = name;
      this.Desktop = desktop;

      this.Host = new WindowForm();
      this.Host.Show(desktop.Host);

      this.Host.FormClosing += (o, e) => e.Cancel = !Close();
      this.Host.KeyUp += Host_KeyUp;

      desktop.Windows.Add(this);
    }

    public string Name { get; }

    public string Href { get; set; }

    public Desktop Desktop { get; }

    public WindowForm Host { get; }

    public void SetContent(Entity entity)
    {
      try
      {
        this.Host.SuspendLayout();

        this.Href = entity.GetSelfLink()?.Href;
        this.Host.Text = entity.Title ?? this.Href ?? "Janela";

        IPage page;
        if (entity.Class.Has(ClassNames.Record))
        {
          page = new RecordPage();
        }
        else if (entity.Class.Has(ClassNames.Table))
        {
          page = new TablePage();
        }
        else
        {
          return;
        }

        page.Window = this;
        page.Entity = entity;

        var isFlexLayout = page.Host.AutoSize;
        if (isFlexLayout)
        {
          page.Host.Dock = DockStyle.Fill;
          Host.Layout = new FlexWindowLayout();
        }
        else
        {
          Host.Layout = new FixedWindowLayout();
        }
        Host.ContentPane.Controls.Add(page.Host);
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
      finally
      {
        this.Host.ResumeLayout();
      }
    }

    public async Task RequestAsync(string uri, string method, string target, Entity data)
    {
      try
      {
        Host.DisplayStatus("Carregando...");
        Host.DisplayProgress();
        Application.DoEvents();

        var client = new HttpClient();

        var ret = await client.RequestAsync(uri, method, data);
        if (!ret.Ok)
        {
          // TODO: Emitir uma mensagem de falha
          return;
        }

        if (ret.Value?.Data is Entity entity)
        {
          var window = Desktop.CreateWindow(this, target);
          window.SetContent(entity);
        }
      }
      catch (Exception ex)
      {
        // TODO: O que fazer com essa exceção
        ex.Trace();
      }
      finally
      {
        Host.ClearStatus();
        Host.ClearProgress();
      }
    }

    public void Reload()
    {
      if (string.IsNullOrEmpty(this.Href))
        return;

      this.RequestAsync(this.Href).NoAwait();
    }

    public bool Close()
    {
      Desktop.Windows.Remove(this);
      if (Desktop.Windows.Count == 0)
      {
        Desktop.CreateWindow();
      }
      return true;
    }

    private void Host_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Modifiers == Keys.None && e.KeyCode == Keys.F5)
      {
        Reload();
      }
    }
  }
}