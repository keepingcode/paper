using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui.Widgets;
using Paper.Media;

namespace Paper.Browser.Gui
{
  public partial class ActionForm : Form
  {
    public ActionForm(Lib.Action action)
    {
      this.Action = action;
      InitializeComponent();
      InitializeData();

      this.Text = action.EntityAction.Title;
      this.SubmitButton.Text = action.EntityAction.Title;
    }

    public Lib.Action Action { get; }

    public IEnumerable<IInputWidget> Widgets()
    {
      return ContentPanel.Controls.Cast<Control>().OfType<IInputWidget>();
    }

    private void InitializeData()
    {
      ContentPanel.Controls.Clear();

      var entity = Action.Entity;

      var rowCount = 1;
      var colCount = 0;

      var fields = Action.EntityAction?.Fields ?? Enumerable.Empty<Field>();
      foreach (var field in fields)
      {
        IInputWidget widget;

        int colExtent;

        switch (field.DataType)
        {
          //case DataTypeNames.Bit:
          //case DataTypeNames.Decimal:
          //case DataTypeNames.Date:
          //case DataTypeNames.Time:
          //case DataTypeNames.Datetime:

          case DataTypeNames.Number:
            {
              colExtent = 1;
              widget = new NumberWidget();
              break;
            }

          case DataTypeNames.Label:
          case DataTypeNames.Text:
          default:
            {
              colExtent = 3;
              widget = new TextWidget();
              break;
            }
        }

        widget.Field = field;
        widget.Text = field.Title;
        widget.Content = field.Value;
        widget.Required = (field.Required == true);
        widget.Host.Width = (88 * colExtent) - 6;

        ContentPanel.Controls.Add(widget.Host);

        if (colCount + colExtent <= 3)
        {
          colCount += colExtent;
        }
        else
        {
          rowCount++;
          colCount = colExtent;
        }
      }

      // c = ⌈x / 10⌉
      // r = ⌈x / c⌉
      var margin = this.Padding.All;
      var cols = Math.Ceiling(rowCount / 10M);
      var rows = Math.Ceiling(rowCount / cols);
      var w = ((3 * 88) * cols) + (margin * 2);
      var h = (42 * rows) + (margin * 2) + pnActions.Height;
      //this.MinimumSize = new Size((int)w, (int)h);
      this.ClientSize = new Size((int)w, (int)h);
    }

    private void btSubmit_Click(object sender, EventArgs e)
    {
      var ok = Action.Submit();
      if (ok)
      {
        Close();
      }
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void ActionForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      var hasChanges = Widgets().Any(x => x.Changed);
      if (hasChanges)
      {
        var result = MessageBox.Show(this
          , "Existem alterações pendentes.\nDeseja sair mesmo assim?"
          , this.Text
          , MessageBoxButtons.YesNo
          , MessageBoxIcon.Warning
          , MessageBoxDefaultButton.Button2
        );
        e.Cancel = (result == DialogResult.No);
      }
    }

    private void ActionForm_Click(object sender, EventArgs e)
    {

      MessageBox.Show($"{this.Size.Width}; {this.Size.Height}");
    }
  }
}