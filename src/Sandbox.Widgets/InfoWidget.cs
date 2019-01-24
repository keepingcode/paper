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
    private object _value;

    public InfoWidget()
    {
      InitializeComponent();
      this.EnhanceControl();
    }

    public Control Control => this;

    public bool HasChanged => false;

    public object Value
    {
      get => _value;
      set => txContent.Text = Change.To<string>(_value = value);
    }

    public string Type { get; set; }

    public string DataType { get; set; }

    public string Category { get; set; }

    public string Title
    {
      get => lbCaption.Text;
      set => lbCaption.Text = value;
    }

    public string Placeholder { get; set; }

    public bool ReadOnly { get; set; }

    public bool Required { get; set; }

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public string Pattern { get; set; }

    public bool Multiline { get; set; }

    public bool AllowMany { get; set; }

    public bool AllowRange { get; set; }

    public bool AllowWildcards { get; set; }

    public Ret ValidateChanges() => true;

    private void InfoWidget_Resize(object sender, EventArgs e)
    {
      if (this.Height != 39)
      {
        this.Height = 39;
      }
    }
  }
}
