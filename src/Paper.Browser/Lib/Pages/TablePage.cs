using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Panels;
using Paper.Media;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;

namespace Paper.Browser.Lib.Pages
{
  public class TablePage : IPage, ISelectablePage
  {
    public event EventHandler SelectionChanged;

    public const string CheckColumnName = "___check___";
    public const string MenuColumnName = "___menu___";

    private Entity _entity;

    private DataGridView dataGrid;
    private TablePanel tablePanel;

    private DataGridViewCheckBoxColumn checkColumn;
    private DataGridViewButtonColumn menuColumn;

    public TablePage()
    {
      this.Host = this.tablePanel = new TablePanel();
      this.dataGrid = this.tablePanel.DataGrid;
      this.dataGrid.CellContentClick += (o, e) =>
      {
        this.dataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
      };
      this.dataGrid.CellContentClick += (o, e) =>
      {
        if (SelectionEnabled && (e.ColumnIndex == checkColumn.Index))
        {
          SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
      };
      this.dataGrid.ColumnHeaderMouseClick += (o, e) =>
      {
        if (SelectionEnabled && (e.ColumnIndex == checkColumn.Index))
        {
          var count = GetSelection().Count();
          var isAllSelected = (count == this.dataGrid.RowCount);

          this.dataGrid.Rows.Cast<DataGridViewRow>().ForEach(
            x => x.Cells[checkColumn.Index].Value = !isAllSelected
          );

          SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
      };
    }

    public Control Host { get; }

    public Window Window { get; set; }

    public Entity Entity
    {
      get => _entity;
      set => DisplayEntity(_entity = value);
    }

    public bool SelectionEnabled
    {
      get => dataGrid.Columns[CheckColumnName].Visible;
      set => dataGrid.Columns[CheckColumnName].Visible = value;
    }

    public ICollection<Entity> GetSelection()
    {
      if (!SelectionEnabled)
        return new Entity[0];

      var selectedRows =
        from row in dataGrid.Rows.Cast<DataGridViewRow>()
        let check = row.Cells[CheckColumnName] as DataGridViewCheckBoxCell
        where check.Value?.Equals(true) == true
        let entity = row.Tag as Entity
        where entity != null
        select entity;

      return selectedRows.ToArray();
    }

    private void DisplayEntity(Entity entity)
    {
      dataGrid.Columns.Clear();
      dataGrid.Rows.Clear();

      if (checkColumn == null)
      {
        checkColumn = new DataGridViewCheckBoxColumn();
        checkColumn.Name = CheckColumnName;
        checkColumn.HeaderText = "";
        checkColumn.Visible = false;
      }
      dataGrid.Columns.Add(checkColumn);

      if (menuColumn == null)
      {
        menuColumn = new DataGridViewButtonColumn();
        menuColumn.Name = MenuColumnName;
        menuColumn.HeaderText = "";
        menuColumn.Visible = false;
      }
      dataGrid.Columns.Add(menuColumn);

      var headers = entity.Headers(ClassNames.Table).ToArray();
      foreach (var header in headers)
      {
        var col = new DataGridViewTextBoxColumn();
        col.Tag = header;
        col.Name = header.Name;
        col.HeaderText = header.Title ?? header.Name.ChangeCase(TextCase.ProperCase);
        col.Visible = !header.Hidden;
        col.ReadOnly = true;

        switch (header.DataType)
        {
          case DataTypeNames.Boolean:
            {
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
            }
          case DataTypeNames.Integer:
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

        dataGrid.Columns.Add(col);
      }

      var lastColumn = dataGrid.Columns.Cast<DataGridViewColumn>().Last();
      lastColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

      var children =
        from child in entity.Children()
        where child.Class.Has(ClassNames.Record)
        where child.Rel.Has(ClassNames.Item)
        select child;

      foreach (var child in children)
      {
        var row = new DataGridViewRow();
        row.Tag = child;
        row.CreateCells(dataGrid);

        row.Cells[0].Value = false;
        row.Cells[1].Value = "Opções";

        int index = 2;
        foreach (var header in headers)
        {
          var value = child.Properties?[header.Name];
          var cellValue = value;
          switch (header.DataType)
          {
            case DataTypeNames.Boolean:
              {
                var bit = Change.To<bool>(value);
                cellValue = bit ? "x" : "-";
                break;
              }
            case DataTypeNames.Integer:
              {
                var number = Change.To<int>(value);
                cellValue = number;
                break;
              }
            case DataTypeNames.Decimal:
              {
                var number = Change.To<decimal>(value);
                cellValue = number.ToString("#,##0.00");
                break;
              }
          }

          row.Cells[index].Tag = value;
          row.Cells[index].Value = cellValue;
          index++;
        }
        dataGrid.Rows.Add(row);
      }
    }

    private object CreateCompatibleValue(object value, string dataType)
    {
      switch (dataType)
      {
        case DataTypeNames.Boolean:
          return Change.To<bool>(value);

        case DataTypeNames.Integer:
          return Change.To<int>(value);

        case DataTypeNames.Decimal:
          return Change.To<decimal>(value);

        case DataTypeNames.Date:
        case DataTypeNames.Time:
        case DataTypeNames.Datetime:
          return Change.To<DateTime>(value);

        case DataTypeNames.Record:
          throw new MediaException("Tipo não suportado: " + DataTypeNames.Record);

        case DataTypeNames.String:
        default:
          return Change.To<string>(value);
      }
    }
  }
}