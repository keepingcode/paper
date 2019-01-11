using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox.Bot
{
  static class FormExtensions
  {
    public static void Do(this Control control, Action action)
    {
      if (control.InvokeRequired)
      {
        control.Invoke(action);
      }
      else
      {
        action.Invoke();
      }
    }

    public static void SetText(this Control control, string text)
    {
      Do(control, () => control.Text = text);
    }

    public static void SetForeColor(this Control control, Color color)
    {
      Do(control, () => control.ForeColor = color);
    }
  }
}