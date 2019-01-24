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
  public partial class SubmitWidget : UserControl, IWidget
  {
    public SubmitWidget()
    {
      InitializeComponent();
      this.EnhanceControl();

      btAction.Click += (o, e) => this.OnClick(e);
    }

    public Control Control => this;

    public bool HasChanged => false;

    public object Value { get; set; }

    public string Type { get; set; }

    public string DataType { get; set; }

    public string Category { get; set; }

    public string Title
    {
      get => btAction.Text;
      set => btAction.Text = value;
    }

    public string Placeholder { get; set; }

    public bool ReadOnly
    {
      get => !btAction.Enabled;
      set => btAction.Enabled = !value;
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
