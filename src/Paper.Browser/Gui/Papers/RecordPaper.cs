using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Lib;
using Paper.Media;
using Paper.Media.Design;
using Toolset;
using Paper.Browser.Gui.Controls;

namespace Paper.Browser.Gui.Papers
{
  public partial class RecordPaper : UserControl, IPaper
  {
    public RecordPaper(Window window, Content content)
    {
      this.Window = window;
      this.Content = content;
      InitializeComponent();
      InitializeData();
    }

    public Control Control => this;

    public Window Window { get; }

    public Content Content { get; }

    private void InitializeData()
    {
      pnContent.Controls.Clear();

      var entity = (Entity)Content.Data;

      var rowCount = 1;
      var colCount = 0;

      var headers = entity.Headers(ClassNames.Record).Where(x => !x.Hidden);
      foreach (var header in headers)
      {
        var value = header.GetValue(entity);

        var field = new FieldBox();
        field.Text = header.Title;
        field.Value = value;

        int colExtent;

        switch (header.DataType)
        {
          case DataTypeNames.Bit:
          case DataTypeNames.Number:
          case DataTypeNames.Decimal:
          case DataTypeNames.Date:
          case DataTypeNames.Time:
          case DataTypeNames.Datetime:
            colExtent = 1;
            break;

          case DataTypeNames.Label:
          case DataTypeNames.Text:
          default:
            colExtent = 3;
            break;
        }

        // w = 82x + 6(x - 1)
        // w = 88x - 6
        field.Width = (88 * colExtent) - 6;

        pnContent.Controls.Add(field);

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
      var w = (284 * cols) - 6 + (margin * 2);
      var h = (42 * rows) - 6 + (margin * 2);

      this.MinimumSize = new Size((int)w, (int)h);
    }
  }
}
