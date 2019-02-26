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
using System.Collections;
using Toolset.Collections;
using System.Diagnostics;

namespace Paper.Browser.Gui.Papers
{
  public partial class TablePaper : UserControl, IPaper
  {
    public TablePaper(Window window, Content content)
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

      var menuColumn = new DataGridViewButtonColumn();
      menuColumn.Visible = false;
      dgContent.Columns.Add(menuColumn);

      var checkColumn = new DataGridViewCheckBoxColumn();
      checkColumn.Visible = false;
      dgContent.Columns.Add(checkColumn);

      var entity = (Entity)Content.Data;
      var bag = (PropertyMap)entity.GetProperty(HeaderDesign.BagName);
      var headers = (PropertyValueCollection)(bag[ClassNames.Table] ?? bag[ClassNames.Record]);

      var headerNames = headers.Select(x => x.ToString()).ToArray();
      foreach (var headerName in headerNames)
      {
        var info = (
          from child in entity.Children()
          where child.Class.Has(ClassNames.Header)
          where headerName.EqualsIgnoreCase((string)child.Properties["name"])
          select child
        ).FirstOrDefault();

        var headerTitle =
          info?.Properties?["title"]?.ToString()
          ?? headerName.ChangeCase(TextCase.ProperCase);

        var dataType = info?.Properties?["dataType"]?.ToString() ?? DataTypeNames.Text;

        var col = new DataGridViewTextBoxColumn();
        col.Tag = dataType;

        switch (dataType)
        {
          case DataTypeNames.Bit:
            {
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
            }
          case DataTypeNames.Number:
          case DataTypeNames.Decimal:
            {
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              break;
            }
          default:
            {
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              break;
            }
        }

        col.Name = headerName;
        col.HeaderText = headerTitle;
        col.ReadOnly = true;

        dgContent.Columns.Add(col);
      }

      var lastColumn = dgContent.Columns.Cast<DataGridViewColumn>().Last();
      lastColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

      var records =
        from child in entity.Children()
        where child.Class.Has(ClassNames.Record)
        where child.Rel.Has(RelNames.Item)
        select child;
      if (!records.Any())
      {
        records =
          from child in entity.Children()
          where child.Class.Has(ClassNames.Record)
          select child;
      }

      foreach (var record in records)
      {
        var cellValues = headerNames.Select(name => record.Properties[name]);
        var link = record?.Links.FirstOrDefault(x => x.Rel.Has(RelNames.Self));

        var row = new DataGridViewRow();
        row.Tag = link?.Href;
        row.CreateCells(dgContent);

        var i = 0;

        row.Cells[i++].Value = "Opções";
        row.Cells[i++].Value = false;
        foreach (var cellValue in cellValues)
        {
          var dataType = (string)dgContent.Columns[i].Tag;
          switch (dataType)
          {
            case DataTypeNames.Bit:
              {
                var bit = Change.To<bool>(cellValue);
                row.Cells[i].Value = bit ? "x" : "-";
                break;
              }
            case DataTypeNames.Number:
              {
                var number = Change.To<int>(cellValue);
                row.Cells[i].Value = number;
                break;
              }
            case DataTypeNames.Decimal:
              {
                var number = Change.To<decimal>(cellValue);
                row.Cells[i].Value = number.ToString("#,##0.00");
                break;
              }
            default:
              {
                row.Cells[i].Value = cellValue;
                break;
              }
          }
          i++;
        }

        dgContent.Rows.Add(row);
      }
    }

    private void DetailCell(int colIndex, int rowIndex)
    {
      var row = dgContent.Rows[rowIndex];
      var href = row?.Tag?.ToString();
      if (href != null)
      {
        var target = new UriString(href).Path;
        Window.NavigateAsync(href, target).NoAwait();
      }
    }

    private void dgContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      dgContent.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void dgContent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      DetailCell(e.ColumnIndex, e.RowIndex);
    }
  }
}
