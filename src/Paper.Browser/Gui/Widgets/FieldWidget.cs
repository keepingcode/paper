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
  public partial class FieldWidget : UserControl, IWidget
  {
    private object _value;

    public FieldWidget()
    {
      InitializeComponent();
    }

    public Control Host => this;

    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      get => lbCaption.Text;
      set => lbCaption.Text = value;
    }

    public object Content
    {
      get => _value;
      set
      {
        _value = value;
        txContent.Text = Formatter.Format(Content);
      }
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
