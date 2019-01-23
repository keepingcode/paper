using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox.Widgets
{
  public partial class HiddenWidget : UserControl, IWidget
  {
    public HiddenWidget()
    {
      InitializeComponent();
      this.Visible = false;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public Control Control => this;

    public object Value { get; set; }

    public bool ReadOnly { get; set; }

    public void OnPropertyChanged(string property)
    {
      // nada a fazer
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (this.Visible)
      {
        this.Visible = false;
      }
    }
  }
}
