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
  public partial class BitWidget : UserControl, IWidget
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private bool _value;

    public BitWidget()
    {
      InitializeComponent();
      FeedbackSlider();
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

    public bool Value
    {
      get => _value;
      set
      {
        _value = value;
        FeedbackSlider();
        OnPropertyChanged(nameof(Value));
      }
    }

    object IWidget.Value
    {
      get => Value;
      set => Value = Change.To<bool>(value);
    }

    private void AlternateValue()
    {
      Value = !Value;
    }

    private void FeedbackSlider()
    {
      btSlider.Left = Value ? 20 : 0;
      btSlider.BackColor = Value ? Color.Green : SystemColors.ControlDark;
    }

    public void OnPropertyChanged(string property)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    private void pnSlideRail_Click(object sender, EventArgs e)
    {
      AlternateValue();
    }

    private void btSlider_Click(object sender, EventArgs e)
    {
      AlternateValue();
    }

    private void lbCaption_Click(object sender, EventArgs e)
    {
      btSlider.Select();
    }
  }
}
