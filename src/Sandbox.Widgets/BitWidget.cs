using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolset;

namespace Sandbox.Widgets
{
  public partial class BitWidget : UserControl, IWidget
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public BitWidget()
    {
      InitializeComponent();
      this.EnhanceControl();

      ckValue.CheckedChanged += (o, e) => OnPropertyChanged(nameof(Value));
    }

    public Control Control => this;

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Bindable(true)]
    public override string Text
    {
      get => ckValue.Text;
      set
      {
        ckValue.Text = value;
        OnPropertyChanged(nameof(Text));
      }
    }

    public bool ReadOnly
    {
      get => !ckValue.Enabled;
      set => ckValue.Enabled = !value;
    }

    public bool Value
    {
      get => ckValue.Checked;
      set
      {
        ckValue.Checked = value;
        OnPropertyChanged(nameof(Value));
      }
    }

    object IWidget.Value
    {
      get => Value;
      set => Value = Change.To<bool>(value);
    }

    public void OnPropertyChanged(string property)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
  }
}
