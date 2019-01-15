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
    }

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

    public void OnPropertyChanged(string property)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
  }
}
