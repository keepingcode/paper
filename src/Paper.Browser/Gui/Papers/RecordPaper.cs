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
      dgContent.AutoGenerateColumns = false;
      dgContent.Rows.Clear();
      dgContent.Columns.Clear();
      dgContent.ColumnHeadersVisible = false;

      var titleColumn = new DataGridViewTextBoxColumn();
      titleColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      dgContent.Columns.Add(titleColumn);

      var valueColumn = new DataGridViewTextBoxColumn();
      valueColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      valueColumn.MinimumWidth = 100;
      dgContent.Columns.Add(valueColumn);

      var entity = (Entity)Content.Data;
      var bag = (PropertyMap)entity.GetProperty(HeaderDesign.BagName);
      var headers = (PropertyValueCollection)bag[ClassNames.Record];

      var headerNames = headers.Select(x => x.ToString()).ToArray();
      foreach (var headerName in headerNames)
      {
        var value = entity.Properties[headerName];

        var info = (
          from child in entity.Children()
          where child.Class.Has(ClassNames.Header)
          where headerName.EqualsIgnoreCase((string)child.Properties["name"])
          select child
        ).FirstOrDefault();

        var headerTitle =
          info?.Properties?["title"]?.ToString()
          ?? headerName.ChangeCase(TextCase.ProperCase);

        var row = new DataGridViewRow();
        row.CreateCells(dgContent);
        row.Cells[0].Value = headerTitle;

        var dataType = info?.Properties?["dataType"]?.ToString() ?? DataTypeNames.Text;
        switch (dataType)
        {
          case DataTypeNames.Bit:
            {
              var bit = Change.To<bool>(value);
              row.Cells[1].Value = bit ? "x" : "-";
              break;
            }
          case DataTypeNames.Number:
            {
              var number = Change.To<int>(value);
              row.Cells[1].Value = number;
              break;
            }
          case DataTypeNames.Decimal:
            {
              var number = Change.To<decimal>(value);
              row.Cells[1].Value = number.ToString("#,##0.00");
              break;
            }
          default:
            {
              row.Cells[1].Value = value;
              break;
            }
        }

        dgContent.Rows.Add(row);
      }
    }
  }
}
