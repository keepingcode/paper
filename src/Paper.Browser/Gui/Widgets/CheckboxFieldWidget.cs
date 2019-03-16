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
using System.Text.RegularExpressions;
using Toolset;

namespace Paper.Browser.Gui.Widgets
{
  public partial class CheckboxFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private bool? sourceValue;

    private Field _field;
    private Extent _gridExtent;

    public CheckboxFieldWidget()
    {
      InitializeComponent();
      this.Enhance();
      this.EnhanceFieldWidget();
      GridExtent = new Extent(2, 1);
      ckValue.CheckedChanged += (o, e) => ValueChanged?.Invoke(this, EventArgs.Empty);
      ckValue.CheckedChanged += (o, e) => ckValue.Text = ckValue.Checked ? "Ativado" : "Desativado";
    }

    public UserControl Host => this;

    public Label Label => lbText;

    public IContainer Components => components ?? (components = new Container());

    public bool HasChanges => ckValue.Checked != sourceValue;

    public Field Field
    {
      get => _field;
      set
      {
        _field = value;
        Value = value?.Value;
        FieldChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    private bool Required => Field?.Required == true;

    public object Value
    {
      get => Required ? ckValue.Checked : (ckValue.Checked ? true : (object)null);
      set
      {
        if (Field == null)
          throw new InvalidOperationException("O campo \"Field\" deve ser indicado antes da indicação do valor do campo.");

        if (Required)
        {
          sourceValue = Change.Try<bool>(value, defaultValue: false);
        }
        else
        {
          sourceValue = Change.Try<bool?>(value);
        }

        ckValue.Checked = (sourceValue == true);
      }
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
      if (Field == null)
        yield break;

      if (Field.Required == true && Value == null)
        yield return "Campo requerido";
    }
  }
}
