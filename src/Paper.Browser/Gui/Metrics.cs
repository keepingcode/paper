using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Gui
{
  public struct Metrics
  {
    public Metrics(int unit, int margin)
    {
      this.Unit = unit;
      this.Margin = margin;
    }

    public int Unit { get; set; }
    public int Margin { get; set; }
  }
}