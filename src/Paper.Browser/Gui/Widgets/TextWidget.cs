using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;

namespace Paper.Browser.Gui.Widgets
{
  public partial class TextWidget : UserControl, IWidget
  {
    public TextWidget(Field field)
    {
      InitializeComponent();
    }

    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      get => lbText.Text;
      set => lbText.Text = value;
    }

    public object Value { get; set; }

    public Size GridExtent { get; set; }
  }
}
