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
  public partial class InfoWidget : UserControl, IWidget
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public InfoWidget()
    {
      InitializeComponent();
      this.EnhanceControl();
    }

    public Control Control => this;

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Bindable(true)]
    public override string Text
    {
      get => lbCaption.Text;
      set
      {
        lbCaption.Text = value;
        OnPropertyChanged(nameof(Text));
      }
    }

    public string Value
    {
      get => txContent.Text;
      set
      {
        txContent.Text = value;
        OnPropertyChanged(nameof(Value));
      }
    }

    object IWidget.Value
    {
      get => Value;
      set => Value = Change.To<string>(value);
    }

    public bool ReadOnly { get; set; }

    private void InfoWidget_Resize(object sender, EventArgs e)
    {
      if (this.Height != 39)
      {
        this.Height = 39;
      }
    }

    public void OnPropertyChanged(string property)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
  }
}
