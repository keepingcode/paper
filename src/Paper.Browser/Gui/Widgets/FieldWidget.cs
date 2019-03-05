using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui.Papers;

namespace Paper.Browser.Gui.Widgets
{
  public partial class FieldWidget : UserControl, IWidget
  {
    private object _value;
    private Size _gridExtent;

    public FieldWidget()
    {
      InitializeComponent();
      this.GridExtent = new Size(6, 1);
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

    public Size GridExtent
    {
      get => _gridExtent;
      set
      {
        _gridExtent = value;
        this.Size = GridLayout.Measure(_gridExtent);
      }
    }
  }
}
