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

namespace Sandbox.Widgets
{
  public partial class TextWidget : UserControl, IWidget
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public TextWidget()
    {
      InitializeComponent();
      FeedbackPlaceholder();
    }

    public string Placeholder
    {
      get => lbPlaceholder.Text;
      set
      {
        lbPlaceholder.Text = value;
        OnPropertyChanged(nameof(Placeholder));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Bindable(true)]
    public override string Text
    {
      get => lbCaption.Text;
      set
      {
        lbCaption.Text = value;
        OnPropertyChanged(nameof(Text));
      }
    }

    public string Value
    {
      get => txContent.Text;
      set
      {
        if (txContent.Text != value)
        {
          txContent.Text = value;
          OnPropertyChanged(nameof(Value));
        }
      }
    }

    object IWidget.Value
    {
      get => Value;
      set => Value = Change.To<string>(value);
    }

    private void TextWidget_Resize(object sender, EventArgs e)
    {
      if (this.Height != 39)
      {
        this.Height = 39;
      }
    }

    private void FeedbackPlaceholder()
    {
      var hasFocus = this.ContainsFocus && txContent.ContainsFocus;
      lbPlaceholder.Visible = !hasFocus && (txContent.Text.Length == 0);
    }

    public void OnPropertyChanged(string property)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
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
      OnPropertyChanged(nameof(Value));
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
  }
}
