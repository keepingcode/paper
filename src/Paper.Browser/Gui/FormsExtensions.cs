using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolset;

namespace Paper.Browser.Gui
{
  public static class FormsExtensions
  {
    public static void Call(this Control host, Action action)
    {
      if (host.InvokeRequired)
      {
        host.Invoke(action);
      }
      else
      {
        action.Invoke();
      }
    }

    public static T Call<T>(this Control host, Func<T> func)
    {
      if (host.InvokeRequired)
      {
        return (T)host.Invoke(func);
      }
      else
      {
        return func.Invoke();
      }
    }

    public static void Expand(this Form form)
    {
      try
      {
        form.Call(() =>
        {
          var screen = Screen.FromControl(form);
          var area = screen.WorkingArea;

          var bounds = new Rectangle(area.Location, area.Size);
          bounds.Inflate(-50, -50);

          form.Bounds = bounds;
          form.WindowState = FormWindowState.Normal;
        });
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }

    public static void Reduce(this Form form)
    {
      try
      {
        form.Call(() =>
        {
          if (form.MinimumSize != Size.Empty)
          {
            form.Size = form.MinimumSize;
            form.WindowState = FormWindowState.Normal;
          }
        });
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}
