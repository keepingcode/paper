using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox.Widgets
{
  public static class ControlExtensions
  {
    public static void EnhanceControl(this Control control)
    {
      foreach (var child in control.Controls.Cast<Control>())
      {
        if (child is TextBox textBox)
        {
          EnhanceTextBox(textBox);
        }
        else
        {
          EnhanceControl(child);
        }
      }
    }

    private static void EnhanceTextBox(this TextBox control)
    {
      control.Enter += (o, e) => control.SelectAll();
      control.KeyDown += (o, e) =>
      {
        if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
        {
          control.SelectAll();
          e.Handled = true;
          e.SuppressKeyPress = true;
        }
      };
    }
  }
}