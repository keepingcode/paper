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
using Paper.Media.Design;
using Paper.Browser.Helpers;

namespace Paper.Browser.Gui.Widgets
{
  public partial class TextWidget : UserControl, IWidget
  {
    private IHeaderInfo _header;
    private object _value;
    private Extent _gridExtent;

    public TextWidget()
    {
      InitializeComponent();
      this.Enhance();
      this.GridExtent = new Extent(6, 1);
    }

    public UserControl Host => this;

    public Label Label => lbText;

    public IContainer Components => components ?? (components = new Container());

    public IHeaderInfo Header
    {
      get => _header;
      set
      {
        _header = value;
        UpdateLayout();
      }
    }

    public object Value
    {
      get => _value;
      set
      {
        _value = value;
        txValue.Text = Formatter.Format(value);
      }
    }

    public Extent GridExtent
    {
      get => (Header?.Hidden == true) ? Extent.Empty : _gridExtent;
      set
      {
        _gridExtent = value;
        this.Size = _gridExtent.ToSize(WidgetGridLayout.Metrics);
      }
    }

    private void UpdateLayout()
    {
      lbText.Text = Header?.Title ?? lbText.Text;

      switch (Header?.DataType)
      {
        case DataTypeNames.Bit:
        case DataTypeNames.Number:
        case DataTypeNames.Decimal:
        case DataTypeNames.Date:
        case DataTypeNames.Time:
          {
            GridExtent = new Extent(2, 1);
            break;
          }

        case DataTypeNames.Datetime:
          {
            GridExtent = new Extent(3, 1);
            break;
          }

        case DataTypeNames.Text:
        case DataTypeNames.Label:
        case DataTypeNames.ArrayOfRecords:
        default:
          {
            GridExtent = new Extent(6, 1);
            break;
          }
      }
    }
  }
}
