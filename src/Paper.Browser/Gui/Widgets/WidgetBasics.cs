using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Toolset;
using Toolset.Reflection;

namespace Paper.Browser.Gui.Widgets
{
  class WidgetBasics<TValue> : IInputWidget
  {
    private readonly IInputWidget widget;

    private IContainer components;
    private Label lbCaption;
    private Label lbFaultCaption;
    private ToolTip toolTip;

    private object cache;
    private object value;
    private bool valid;

    private string _text;
    private bool _isRequired;

    public WidgetBasics(IInputWidget widget)
    {
      this.widget = widget;
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      var host = (Control)widget;

      host.SuspendLayout();

      components = new Container();
      lbCaption = new Label();
      lbFaultCaption = new Label();
      toolTip = new ToolTip(components);

      // lbCaption
      lbCaption.Name = nameof(lbCaption);
      lbCaption.AutoSize = false;
      lbCaption.Location = new Point(-3, 0);
      lbCaption.Width = host.Width;
      lbCaption.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
      lbCaption.SendToBack();

      // lbFaultCaption
      lbFaultCaption.Name = nameof(lbFaultCaption);
      lbFaultCaption.AutoSize = false;
      lbFaultCaption.Location = new Point(-3, 0);
      lbFaultCaption.Width = host.Width;
      lbFaultCaption.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
      lbFaultCaption.ForeColor = Color.Firebrick;
      lbFaultCaption.Visible = false;
      lbFaultCaption.SendToBack();

      host.Controls.Add(lbCaption);
      host.Controls.Add(lbFaultCaption);
      host.Disposed += (o, e) => components.Dispose();
      host.Resize += (o, e) => { if (host.Height != 36) { host.Height = 36; } };

      host.ResumeLayout();
    }

    public Control Host => (Control)widget;

    public Field Field { get; set; }

    public string Text
    {
      get => _text;
      set
      {
        _text = value;
        FeedbackCaption();
      }
    }

    public object Content
    {
      get => value;
      set => SetValue(value);
    }

    public bool Required
    {
      get => _isRequired;
      set
      {
        _isRequired = value;
        FeedbackCaption();
      }
    }

    public bool Changed
    {
      get
      {
        if (!valid)
          return true;

        if ((cache == null) || (value == null))
          return (cache ?? value) != null;

        return !cache.Equals(value);
      }
    }

    public string Fault
    {
      get => toolTip.GetToolTip(lbFaultCaption).NullIfEmpty();
      set
      {
        toolTip.SetToolTip(lbFaultCaption, value);

        var isFaulty = (value != null);
        lbCaption.Visible = !isFaulty;
        lbFaultCaption.Visible = isFaulty;
      }
    }

    private void SetValue(object sourceValue)
    {
      if (sourceValue == null || (sourceValue is string text && string.IsNullOrWhiteSpace(text)))
      {
        this.valid = true;
        this.value = null;
        this.Fault = null;
      }
      else
      {
        this.valid = Change.Try(sourceValue, typeof(TValue), out this.value);
        this.Fault = this.valid ? null : "O texto digitado não é um número válido.";
      }
      FeedbackChanges();
    }

    public void CommitChanges()
    {
      cache = value;
      FeedbackChanges();
    }

    public void RevertChanges()
    {
      SetValue(cache);
      FeedbackChanges();
    }

    public bool ValidateContent()
    {
      if (Fault != null)
        return false;

      if (Required)
      {
        if ((Content == null) || (Content is string text && string.IsNullOrWhiteSpace(text)))
        {
          Fault = "Este campo é requerido.";
          return false;
        }
      }

      return true;
    }

    private void FeedbackCaption()
    {
      lbFaultCaption.Text = lbCaption.Text = Required ? $"{Text}*" : Text;
    }

    private void FeedbackChanges()
    {
      lbCaption.Font = lbFaultCaption.Font = Changed
        ? new Font(lbCaption.Font, FontStyle.Bold)
        : new Font(lbCaption.Font, FontStyle.Regular);
    }
  }
}