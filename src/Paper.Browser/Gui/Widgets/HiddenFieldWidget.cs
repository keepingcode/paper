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
  public partial class HiddenFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private Field _field;
    private object _value;

    public HiddenFieldWidget()
    {
      InitializeComponent();
    }

    public UserControl Host => this;

    public Label Label => null;

    public IContainer Components => components ?? (components = new Container());

    public Field Field
    {
      get => _field;
      set
      {
        if (_field != value)
        {
          _field = value;
          _value = _field.Value;
          FieldChanged?.Invoke(this, EventArgs.Empty);
        }
      }
    }

    public bool HasChanges => false;

    public object Value
    {
      get => _value;
      set
      {
        if (_value != value)
        {
          _value = value;
          ValueChanged?.Invoke(this, EventArgs.Empty);
        }
      }
    }

    public Extent GridExtent { get; }

    public IEnumerable<string> GetErrors()
    {
      yield break;
    }
  }
}