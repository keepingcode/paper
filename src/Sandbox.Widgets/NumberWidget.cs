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
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;

namespace Sandbox.Widgets
{
  public partial class NumberWidget : UserControl, IWidget
  {
    private const int DefaultHeight = 39;

    private object originalValue;

    public NumberWidget()
    {
      InitializeComponent();
      FeedbackPlaceholder();
      this.Value = "";
      this.Placeholder = "0";
      this.EnhanceControl();
    }

    public Control Control => this;

    public bool HasChanged => Value != originalValue;

    private decimal? ConvertToNumber(string text)
    {
      var digits = Regex.Replace(text, "[^0-9,.]", "");
      digits = digits.Replace(",", ".");
      if (digits != "")
      {
        decimal number;
        var ok = decimal.TryParse(digits, out number);
        if (ok)
          return number;
      }
      return null;
    }

    private string ConvertToString(decimal? number)
    {
      return (number != null) ? number.ToString() : null;
    }

    public object Value
    {
      get => ConvertToNumber(txContent.Text);
      set
      {
        decimal? number;
        if (value is decimal)
        {
          number = (decimal)value;
        }
        else if (value is decimal?)
        {
          number = (decimal?)value;
        }
        else if (value is string text)
        {
          number = ConvertToNumber(text);
        }
        else
        {
          try
          {
            number = Change.To<decimal>(value);
          }
          catch
          {
            number = null;
          }
        }
        txContent.Text = ConvertToString(number);
      }
    }

    public string Type { get; set; }

    public string DataType { get; set; }

    public string Category { get; set; }

    public string Title
    {
      get => lbCaption.Text;
      set => lbCaption.Text = value;
    }

    public string Placeholder
    {
      get => lbPlaceholder.Text;
      set => lbPlaceholder.Text = value;
    }

    public bool ReadOnly
    {
      get => txContent.ReadOnly;
      set
      {
        txContent.ReadOnly = value;
        if (txContent.ReadOnly)
        {
          txContent.BorderStyle = BorderStyle.None;
          txContent.Location = new Point(0 + 4, 16 + 4);
          btUp.Enabled = btDown.Enabled = false;
        }
        else
        {
          txContent.BorderStyle = BorderStyle.Fixed3D;
          txContent.Location = new Point(0, 16);
          btUp.Enabled = btDown.Enabled = true;
        }
      }
    }

    public bool Required { get; set; }

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public string Pattern { get; set; }

    public bool Multiline
    {
      get => txContent.Multiline;
      set
      {
        txContent.Multiline = value;
        if (txContent.Multiline)
        {
          txContent.Height = DefaultHeight << 2;
        }
      }
    }

    public bool AllowMany { get; set; }

    public bool AllowRange { get; set; }

    public bool AllowWildcards { get; set; }

    public Ret ValidateChanges()
    {
      if (HasChanged)
      {
        var text = txContent.Text;

        if (!string.IsNullOrWhiteSpace(Pattern))
        {
          var ok = Regex.IsMatch(text, Pattern);
          if (!ok)
            return Ret.Fail("O texto não está no formato esperado.");
        }

        if (text.Length < MinLength)
          return Ret.Fail($"O texto deve ter no mínimo {MinLength} caracteres.");

        if (MaxLength > 0 && text.Length > MaxLength)
          return Ret.Fail($"O texto deve ter no máximo {MaxLength} caracteres.");
      }
      return true;
    }

    private void Up()
    {
      var number = ConvertToNumber(txContent.Text) ?? 0;
      number++;
      txContent.Text = ConvertToString(number);
    }

    private void Down()
    {
      var number = ConvertToNumber(txContent.Text) ?? 0;
      number--;
      txContent.Text = ConvertToString(number);
    }

    private void FeedbackPlaceholder()
    {
      var hasFocus = this.ContainsFocus && txContent.ContainsFocus;
      lbPlaceholder.Visible = !hasFocus && (txContent.Text.Length == 0);
    }

    private void TextWidget_Click(object sender, EventArgs e)
    {
      txContent.Focus();
      FeedbackPlaceholder();
    }

    private void lbCaption_Click(object sender, EventArgs e)
    {
      txContent.Focus();
      FeedbackPlaceholder();
    }

    private void lbPlaceholder_Click(object sender, EventArgs e)
    {
      txContent.Focus();
      FeedbackPlaceholder();
    }

    private void txContent_TextChanged(object sender, EventArgs e)
    {
      FeedbackPlaceholder();
    }

    private void txContent_Enter(object sender, EventArgs e)
    {
      FeedbackPlaceholder();
    }

    private void txContent_Leave(object sender, EventArgs e)
    {
      FeedbackPlaceholder();
    }

    private void TextWidget_Enter(object sender, EventArgs e)
    {
      FeedbackPlaceholder();
    }

    private void TextWidget_Leave(object sender, EventArgs e)
    {
      FeedbackPlaceholder();
    }

    private void btUp_Click(object sender, EventArgs e)
    {
      Up();
    }

    private void btDown_Click(object sender, EventArgs e)
    {
      Down();
    }
  }
}
