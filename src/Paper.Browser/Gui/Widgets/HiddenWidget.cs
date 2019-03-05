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
  public partial class HiddenWidget : UserControl, IInputWidget
  {
    public HiddenWidget()
    {
      InitializeComponent();
      Visible = false;
      VisibleChanged += (o, e) => { if (Visible) { Visible = false; } };
    }

    public Control Host => this;

    public object Content { get; set; }

    public Size GridExtent
    {
      get => Size.Empty;
      set { /* não pode ser modificado. */}
    }

    public Field Field { get; set; }

    public bool Required { get; set; }

    public bool Changed => false;

    public void CommitChanges() { }

    public void RevertChanges() { }

    public bool ValidateContent() => true;
  }
}