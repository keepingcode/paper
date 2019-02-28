using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui.Widgets
{
  public partial class DecimalWidget : UserControl, IWidget
  {
    private object _value;

    public DecimalWidget()
    {
      InitializeComponent();
    }

    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      get => lbCaption.Text;
      set => lbCaption.Text = value;
    }

    public object Value
    {
      get => txContent.Text;
      set => txContent.Text = value?.ToString();
    }

    private void Field_Resize(object sender, EventArgs e)
    {
      if (this.Height != 36)
      {
        this.Height = 36;
      }
    }
  }
}