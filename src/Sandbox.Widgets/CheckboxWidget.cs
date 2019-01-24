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
  public partial class CheckboxWidget : UserControl, IWidget
  {
    private object originalValue;

    public CheckboxWidget()
    {
      InitializeComponent();
      this.EnhanceControl();
    }

    public Control Control => this;

    public bool HasChanged => Value != originalValue;

    public object Value
    {
      get => ckValue.Checked ? 1 : 0;
      set
      {
        ckValue.Checked = Change.To<bool>(value);
        originalValue = ckValue.Checked ? 1 : 0;
      }
    }

    public string Type { get; set; }

    public string DataType { get; set; }

    public string Category { get; set; }

    public string Title
    {
      get => ckValue.Text;
      set => ckValue.Text = value;
    }

    public string Placeholder { get; set; }

    public bool ReadOnly
    {
      get => !ckValue.Enabled;
      set => ckValue.Enabled = !value;
    }

    public bool Required { get; set; }

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public string Pattern { get; set; }

    public bool Multiline { get; set; }

    public bool AllowMany { get; set; }

    public bool AllowRange { get; set; }

    public bool AllowWildcards { get; set; }

    public Ret ValidateChanges() => true;
  }
}
