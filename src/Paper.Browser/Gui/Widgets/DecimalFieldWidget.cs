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
  public partial class DecimalFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private string sourceValue;

    private Field _field;
    private Extent _gridExtent;

    public DecimalFieldWidget()
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
      get
      {
        if (txValue.Text == "")
        {
          return null;
        }
        else if (Change.Try(FormatFromPtToEn(txValue.Text), out decimal number))
        {
          return number;
        }
        else
        {
          return null;
        }
      }
      set
      {
        if (Field == null)
          throw new InvalidOperationException("O campo \"Field\" deve ser indicado antes da indicação do valor do campo.");

        if (value == null)
        {
          txValue.Text = "";
        }
        else if (Change.Try(value, out decimal number))
        {
          txValue.Text = FormatFromEnToPt(number.ToString());
        }
        else
        {
          txValue.Text = "";
        }
        sourceValue = txValue.Text;
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

      if (Field.Required == true && string.IsNullOrEmpty(txValue.Text))
        yield return "Campo requerido";

      if (txValue.Text == "")
        yield break;

      if (Field.Pattern != null && Regex.IsMatch(txValue.Text, Field.Pattern))
        yield return $"Não corresponde ao padrão de preenchimento: {Field.Pattern}";

      if (!Regex.IsMatch(txValue.Text, @"^-?(([0-9]{1,2}\.)?([0-9]{3}\.)*[0-9]{3}|[0-9]+)(,([0-9]{3}(\.[0-9]{3})*(\.[0-9]{1,2})?|[0-9]+))?$"))
        yield return $"O número deve ser um valor decimal válido, com casas decimais separadas por vírgula e, opcionalmente, ponto como separador de milhar";
    }

    private string FormatFromPtToEn(string ptText)
    {
      return ptText.Replace(".", ",").Replace(",", "");
    }

    private string FormatFromEnToPt(string enText)
    {
      return enText.Replace(",", "").Replace(".", ",");
    }

    private void UpdateLayout()
    {
      GridExtent = new Extent(3, 1);
    }
  }
}
