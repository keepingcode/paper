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
using Paper.Media;
using System.Diagnostics;

namespace Paper.Browser.Gui.Widgets
{
  public partial class NumberWidget : UserControl, IInputWidget
  {
    private readonly WidgetBasics<int?> basics;

    public NumberWidget()
    {
      InitializeComponent();
      this.basics = new WidgetBasics<int?>(this);
      txContent.TextChanged += (o, e) => basics.Content = txContent.Text;
    }

    public Control Host
    {
      get => basics.Host;
    }

    public Field Field
    {
      get => basics.Field;
      set => basics.Field = value;
    }

    public object Content
    {
      get => basics.Content;
      set
      {
        basics.Content = value;
        basics.CommitChanges();
        txContent.Text = basics.Content?.ToString();
      }
    }

    [Bindable(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      get => basics.Text;
      set => basics.Text = value;
    }

    public bool Required
    {
      get => basics.Required;
      set => basics.Required = value;
    }

    public bool Changed
    {
      get => basics.Changed;
    }

    public void CommitChanges() => basics.CommitChanges();

    public void RevertChanges() => basics.RevertChanges();

    public bool ValidateContent() => basics.ValidateContent();
  }
}