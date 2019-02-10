using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;

namespace Paper.Browser.Base.Pages
{
  public partial class RowsPage : UserControl, IPage
  {
    public RowsPage(Window window, Entity entity)
    {
      this.Window = window;
      this.Entity = entity;
      InitializeComponent();
      FillUp(entity);
    }

    public Window Window { get; }
    public Entity Entity { get; }

    private void SetUp(Entity entity)
    {
      var allHeaders = entity.Properties?["headers"] as PropertyMap;
      var rowHeaders = allHeaders?["rows"] as PropertyMap;
      if (rowHeaders != null)
      {
        
      }
    }

    private void FillUp(Entity entity)
    {
      // var records = entity.Entities?.Where(x => x.Class.Has(ClassNames.Data));
      // if (records == null)
      //   return;
      // 
      // foreach (Entity record in records)
      // {
      //   var row = new DataGridViewRow();
      // 
      //   if (record.Properties != null)
      //   {
      //     if (dgRows.ColumnCount < record.Properties.Count)
      //     {
      //       dgRows.ColumnCount = record.Properties.Count;
      //     }
      // 
      //     foreach (var property in record.Properties)
      //     {
      //       var cell = new DataGridViewTextBoxCell();
      //       cell.Value = property.Value;
      // 
      //       row.Cells.Add(cell);
      //     }
      //   }
      // 
      //   dgRows.Rows.Add(row);
      // }
    }
  }
}
