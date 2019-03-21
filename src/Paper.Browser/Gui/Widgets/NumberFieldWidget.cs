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
using Paper.Browser.Lib;

namespace Paper.Browser.Gui.Widgets
{
  public partial class NumberFieldWidget : UserControl, IFieldWidget
  {
    public event EventHandler FieldChanged;
    public event EventHandler ValueChanged;

    private string sourceValue;

    private Field _field;
    private Extent _gridExtent;

    public NumberFieldWidget()
    {
      InitializeComponent();
      UpdateLayout();
      this.Enhance();
      this.EnhanceFieldWidget();
      txValue.TextChanged += (o, e) => ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    public Window Window { get; set; }

    public UserControl Host => this;

    public Label Label => lbText;

    public IContainer Components => components ?? (components = new Container());

    public bool HasChanges => txValue.Text != sourceValue;

    public Entity Entity { get; set; }

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
        var enText = FromPtToEn(txValue.Text);

        if (enText == "")
        {
          return null;
        }
        else if (TryCast(enText, out object number))
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

        string enText;
        if (value == null)
        {
          enText = "";
        }
        else if (TryCast(value, out object number))
        {
          enText = Change.To<string>(number);
        }
        else
        {
          enText = "";
        }

        txValue.Text = FromEnToPt(enText);
        sourceValue = txValue.Text;
      }
    }

    private string FromEnToPt(string enText)
    {
      var ptText = Regex.Replace(enText, @"\.|,", m => m.Value == "." ? "," : ".");
      return ptText;
    }

    private string FromPtToEn(string ptText)
    {
      var enText = Regex.Replace(ptText, @"\.|,", m => m.Value == "." ? "," : ".");
      return enText;
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

      if (Field.DataType == DataTypeNames.Integer)
      {
        if (!Regex.IsMatch(txValue.Text, @"^-?(([0-9]{1,2}\.)?([0-9]{3}\.)*[0-9]{3}|[0-9]+)$"))
          yield return $"O número deve ser um inteiro válido sem casas decimais.";
      }
      else
      {
        if (!Regex.IsMatch(txValue.Text, @"^-?(([0-9]{1,2}\.)?([0-9]{3}\.)*[0-9]{3}|[0-9]+)(,([0-9]{3}(\.[0-9]{3})*(\.[0-9]{1,2})?|[0-9]+))?$"))
          yield return $"O número deve ser um valor decimal válido, com casas decimais separadas por vírgula e, opcionalmente, ponto como separador de milhar";
      }
    }

    private bool TryCast(object value, out object number)
    {
      if (Field.DataType == DataTypeNames.Integer)
      {
        var ok = Change.Try(value, out int output);
        number = output;
        return ok;
      }
      else
      {
        var ok = Change.Try(value, out decimal output);
        number = output;
        return ok;
      }
    }

    private void UpdateLayout()
    {
      if (Field?.DataType == DataTypeNames.Decimal)
      {
        GridExtent = new Extent(3, 1);
      }
      else
      {
        GridExtent = new Extent(2, 1); 
      }
    }
  }
}
