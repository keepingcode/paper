using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui
{
  public static class WidgetGridLayout
  {
    public static readonly Metrics Metrics = new Metrics { Unit = 37, Margin = 6 };

    public static Size Measure(IEnumerable<IWidget> widgets)
    {
      int cols = 0;
      int rows = 0;

      int maxCols = 0;
      int maxRows = 12;
      do
      {
        maxCols += 6;

        var size = widgets.Select(x => x.GridExtent).Aggregate(new Extent(0, 1), (prev, curr) =>
        {
          var calc = (prev.X + curr.X) <= maxCols
            ? new Extent(prev.X + curr.X, Math.Max(prev.Y, curr.Y))
            : new Extent(curr.X, prev.Y + curr.Y);
          cols = Math.Max(cols, calc.X);
          return calc;
        });

        rows = size.Y;
      } while (rows > maxRows);

      var extent = new Size(
        width: (cols * Metrics.Unit) + (cols * Metrics.Margin),
        height: (rows * Metrics.Unit) + (rows * Metrics.Margin)
      );
      return extent;
    }
  }
}