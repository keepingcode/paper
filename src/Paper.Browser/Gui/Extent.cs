using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Gui
{
  public struct Extent
  {
    public Extent(int w, int h)
    {
      this.W = w;
      this.H = h;
    }

    public int W { get; set; }
    public int H { get; set; }
  }
}
