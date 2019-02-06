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
using Toolset;

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
      this.Form.Timer.Tick += (o, e) => SetMessage(null, TimeSpan.Zero);
    }

    private void SetProgress(int percent)
    {
      try
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
      catch (Exception ex)
      {
        ex.Trace();
      }
    }

    public void SetMessage(string status)
    {
      SetMessage(status, TimeSpan.FromSeconds(5));
    }

    public void SetMessage(string status, TimeSpan timeout)
    {
      lock (synclock)
      {
        try
        {
          Form.Call(() =>
          {
            Form.Timer.Enabled = false;
            Form.StatusLabel.Text = status;
            if (!string.IsNullOrEmpty(status))
            {
              Form.Timer.Interval =
                timeout.TotalMilliseconds.CompareTo(int.MaxValue) >= 0
                  ? int.MaxValue
                  : (int)timeout.TotalMilliseconds;
              Form.Timer.Enabled = true;
            }
          });
        }
        catch (Exception ex)
        {
          ex.Trace();
        }
      }
    }

    public async void OpenAsync(string href)
    {
      try
      {
        SetFormState(href);

        var entity = await Http.RequestEntityAsync(href);
        var page = PageFactory.CreatePage(this, entity);

        Pack((UserControl)page);

        SetFormState(href, entity: entity);

        this.Page = page;
      }
      catch (Exception ex)
      {
        ex.Trace();
        SetFormState(href, ex: ex);
      }
    }

    private void SetFormState(string href, Entity entity = null, Exception ex = null)
    {
      try
      {
        Form.SuspendLayout();
        Form.Call(() =>
        {
          if (ex != null)
          {
            Form.Text = "Falha";
            Form.Overlay = false;
            SetMessage("Falha no carregamento.", TimeSpan.MaxValue);
            SetProgress(0);
          }
          else if (entity != null)
          {
            Form.Text = entity.Title ?? href;
            Form.Overlay = false;
            SetMessage("Localizado.");
            SetProgress(0);
          }
          else
          {
            Form.Text = "Carregando...";
            Form.Overlay = true;
            SetMessage($"Localizando {href}...", TimeSpan.MaxValue);
            SetProgress(-1);
          }
        });
      }
      finally
      {
        Form.ResumeLayout();
      }
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