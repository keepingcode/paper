using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui
{
  public class WidgetGridLayout
  {
    public const int Unit = 37;
    public const int Margin = 6;

    public Size Measure(Size extent)
    {
      return new Size(
        width: (extent.Width * Unit) + ((extent.Width - 1) * Margin),
        height: (extent.Height * Unit) + ((extent.Height - 1) * Margin)
      );
    }

    public Size Measure(IEnumerable<IWidget> widgets)
    {
      int cols = 0;
      int rows = 0;

      int maxCols = 0;
      int maxRows = 12;
      do
      {
        maxCols += 6;

        var size = widgets.Select(x => x.GridExtent).Aggregate(new Size(0, 1), (prev, curr) =>
        {
          var calc = (prev.Width + curr.Width) <= maxCols
            ? new Size(prev.Width + curr.Width, Math.Max(prev.Height, curr.Height))
            : new Size(curr.Width, prev.Height + curr.Height);
          cols = Math.Max(cols, calc.Width);
          return calc;
        });

        rows = size.Height;
      } while (rows > maxRows);

      var extent = new Size(
        width: (cols * Unit) + ((cols - 1) * Margin),
        height: (rows * Unit) + ((rows - 1) * Margin)
      );
      return extent;
    }
  }
}