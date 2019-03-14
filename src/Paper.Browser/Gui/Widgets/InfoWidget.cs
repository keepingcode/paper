using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Helpers;

namespace Paper.Browser.Gui.Widgets
{
  public partial class InfoWidget : UserControl, IWidget
  {
    private object _value;

    public InfoWidget()
    {
      InitializeComponent();
    }

    public InfoWidget(string text, object value)
      : this()
    {
      this.Text = text;
      this.Value = value;
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

    public object Value
    {
      get => _value;
      set
      {
        txValue.Text = Formatter.Format(_value = value);
        UpdateDimensions();
      }
    }

    public Extent GridExtent
    {
      get
      {
        if (Value is bool
         || Value is int
         || Value is double
         || Value is decimal
         || Value is float)
        {
          return new Extent(2, 1);
        }

        if (Value is DateTime)
        {
          return new Extent(3, 1);
        }

        return new Extent(6, 1);
      }
    }

    private void UpdateDimensions()
    {
      this.Size = GridExtent.ToSize(WidgetGridLayout.Metrics);
    }
  }
}