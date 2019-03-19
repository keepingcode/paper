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

namespace Paper.Browser.Gui.Widgets
{
  public partial class SelectRecordFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private object sourceValue;

    private Field _field;
    private Extent _gridExtent;

    public SelectRecordFieldWidget()
    {
      InitializeComponent();
      GridExtent = new Extent(6, 1);
    }

    public UserControl Host => this;

    public Label Label => lbText;

    public IContainer Components => components ?? (components = new Container());

    public Entity Entity { get; set; }

    public Field Field
    {
      get => _field;
      set
      {
        _field = value;
        if (_field?.Provider != null)
        {
          var isSelfProvider = _field.Provider.Rel.Has(RelNames.Self);
          this.Href = isSelfProvider ? Entity.GetSelfHref() : _field?.Provider.Href;
          this.Keys = _field.Provider.Keys;
        }
        else
        {
          this.Href = null;
          this.Keys = null;
        }
      }
    }

    public Href Href { get; set; }

    public NameCollection Keys { get; set; }

    public bool HasChanges => false;

    public object Value
    {
      get;
      set;
    }

    public Extent GridExtent
    {
      get => _gridExtent;
      set
      {
        _gridExtent = value;
        this.Size = _gridExtent.ToSize(WidgetGridLayout.Metrics);
      }
    }

    public IEnumerable<string> GetErrors()
    {
      yield break;
    }
  }
}
