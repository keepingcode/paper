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

namespace Paper.Browser.Gui.Widgets
{
  public partial class TextFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private string sourceValue;

    private Field _field;
    private Extent _gridExtent;

    public TextFieldWidget()
    {
      InitializeComponent();
      UpdateLayout();
      this.Enhance();
      this.EnhanceFieldWidget();
      txValue.TextChanged += (o, e) => ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    public UserControl Host => this;

    public Label Label => lbText;

    public IContainer Components => components ?? (components = new Container());

    public bool HasChanges => txValue.Text != sourceValue;

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

    public object Value
    {
      get => txValue.Text;
      set
      {
        txValue.Text = value?.ToString();
        sourceValue = txValue.Text;
      }
    }

    public Extent GridExtent
    {
      get => (Field?.Type == FieldTypeNames.Hidden) ? Extent.Empty : _gridExtent;
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

      if (Field.Required == true && string.IsNullOrEmpty(txValue.Text))
        yield return "Campo requerido";

      if (Field.MinLength != null && txValue.Text.Length < Field.MinLength)
        yield return $"Deve ter no mínimo {Field.MinLength} caracteres";

      if (Field.MaxLength != null && txValue.Text.Length > Field.MaxLength)
        yield return $"Deve ter no máximo {Field.MaxLength} caracteres";

      if (Field.Pattern != null && Regex.IsMatch(txValue.Text, Field.Pattern))
        yield return $"Não corresponde ao padrão de preenchimento: {Field.Pattern}";
    }

    private void UpdateLayout()
    {
      GridExtent = (Field?.Multiline == true) ? new Extent(6, 5) : new Extent(6, 1);
      Visible = Field?.Type != FieldTypeNames.Hidden;
    }
  }
}
