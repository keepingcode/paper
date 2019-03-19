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
  public partial class DatetimeLocalFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private object sourceValue;

    private Field _field;
    private Extent _gridExtent;

    public DatetimeLocalFieldWidget()
    {
      InitializeComponent();
      UpdateLayout();
      this.Enhance();
      this.EnhanceFieldWidget();
      GridExtent = new Extent(4, 1);
      dpValue.TextChanged += (o, e) => ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    public UserControl Host => this;

    public Label Label => lbText;

    public IContainer Components => components ?? (components = new Container());

    public bool HasChanges
    {
      get => (Value == null) != (sourceValue == null) || !Value.Equals(sourceValue);
    }

  public Field Field
    {
      get => _field;
      set
      {
        _field = value;
        Value = value?.Value;
        UpdateLayout();
        FieldChanged?.Invoke(this, EventArgs.Empty);
      }
    }

    private bool Required => Field?.Required == true;

    public object Value
    {
      get => dpValue.Checked ? (object)dpValue.Value : null;
      set
      {
        if (Field == null)
          throw new InvalidOperationException("O campo \"Field\" deve ser indicado antes da indicação do valor do campo.");

        var defaultDate = MakeDefaultDate();
        var date = Required
          ? Change.Try<DateTime>(value, defaultDate)
          : Change.Try<DateTime?>(value);

        dpValue.Checked = Required || value != null;
        dpValue.Value = date ?? defaultDate;

        sourceValue = date;
      }
    }

    private DateTime MakeDefaultDate()
    {
      var now = DateTime.Now;
      var date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
      return date;
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

    private void UpdateLayout()
    {
      dpValue.ShowCheckBox = Field?.Required != true;
    }
  }
}
