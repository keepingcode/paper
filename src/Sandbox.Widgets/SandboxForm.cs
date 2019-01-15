using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox.Widgets
{
  public partial class SandboxForm : Form
  {
    public SandboxForm()
    {
      InitializeComponent();

      foreach (var widget in this.Controls.Cast<Control>().OfType<IWidget>())
      {
        widget.PropertyChanged += (o, e) =>
        {
          if (e.PropertyName == "Value")
          {
            widget.Text = $"Value: {widget.Value}";
          }
        };
        widget.Text = $"Value: {widget.Value}";
      }
    }
  }
}
