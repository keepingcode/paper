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
  public partial class ButtonWidget : UserControl, IWidget
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public ButtonWidget()
    {
      InitializeComponent();
      this.EnhanceControl();

      btAction.Click += (o, e) => this.OnClick(e);
    }

    public Control Control => this;

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Bindable(true)]
    public override string Text
    {
      get => btAction.Text;
      set
      {
        btAction.Text = value;
        OnPropertyChanged(nameof(Text));
      }
    }

    object IWidget.Value
    {
      get;
      set;
    }

    public bool ReadOnly
    {
      get => !btAction.Enabled;
      set => btAction.Enabled = !value;
    }

    public void OnPropertyChanged(string property)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
  }
}
