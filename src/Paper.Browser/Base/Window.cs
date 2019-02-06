using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Base.Forms;
using Paper.Browser.Base.Pages;
using Paper.Browser.Commons;
using Paper.Media;

namespace Paper.Browser.Base
{
  public class Window
  {
    private readonly object synclock = new object();

    public WindowForm Form { get; }
    public IPage Page { get; private set; }

    internal Window(WindowForm form)
    {
      this.Form = form;
      this.Form.Timer.Tick += (o, e) => ShowMessage(null, TimeSpan.Zero);
    }

    public void SetTitle(string title)
    {
      Form.Call(() => Form.Text = title);
    }

    private void SetProgress(int percent)
    {
      Form.Call(() =>
      {
        if (percent > 0)
        {
          percent = 0;
        }
        Form.ProgressBar.Style = (percent < 0) ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
        Form.ProgressBar.Value = (percent < 0) ? 0 : percent;
      });
    }

    public void ShowMessage(string status)
    {
      ShowMessage(status, TimeSpan.FromSeconds(5));
    }

    public void ShowMessage(string status, TimeSpan delay)
    {
      lock (synclock)
      {
        Form.Call(() =>
        {
          Form.Timer.Enabled = false;
          Form.StatusLabel.Text = status;
          if (!string.IsNullOrEmpty(status))
          {
            Form.Timer.Interval =
              delay.TotalMilliseconds.CompareTo(int.MaxValue) >= 0
                ? int.MaxValue
                : (int)delay.TotalMilliseconds;
            Form.Timer.Enabled = true;
          }
        });
      }
    }

    public async void OpenAsync(string href)
    {
      SetProgress(-1);
      SetTitle("Carregando...");
      ShowMessage($"Localizando {href}...", TimeSpan.MaxValue);

      var entity = await Http.RequestEntityAsync(href);

      SetProgress(0);
      SetTitle(entity.Title ?? href);
      ShowMessage("Localizado.");

      var page = PageFactory.CreatePage(this, entity);
      Pack((UserControl)page);

      this.Page = page;
    }

    private void Pack(Control control)
    {
      if (control.AutoSize)
      {
        control.Dock = DockStyle.Fill;
        Form.AutoSize = false;
        Form.PageContainer.Controls.Add(control);
        Form.Expand();
      }
      else
      {
        Form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Form.AutoSize = true;
        Form.PageContainer.Controls.Add(control);

        var size = Form.Size;

        Form.SuspendLayout();
        control.Dock = DockStyle.Fill;
        Form.AutoSizeMode = AutoSizeMode.GrowOnly;
        Form.AutoSize = false;
        Form.MinimumSize = size;
        Form.Size = size;
        Form.FormBorderStyle = FormBorderStyle.Sizable;
        Form.ResumeLayout();
      }
    }
  }
}