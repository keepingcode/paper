using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Commons
{
  public static class WindowsFormsExtensions
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
      form.Call(() =>
      {
        var mdiClient = GetMdiClient(form.MdiParent);
        if (mdiClient != null)
        {
          form.Location = Point.Empty;
          form.Size = new Size(mdiClient.Size.Width - 5, mdiClient.Size.Height - 5);
        }
      });
    }

    private static MdiClient GetMdiClient(Form form)
    {
      return form.Controls.OfType<MdiClient>().FirstOrDefault();
    }
  }
}
