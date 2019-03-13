using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Gui
{
  public struct Extent
  {
    public static readonly Extent Empty = new Extent(0, 0);

    private Point point;

    public Extent(int x, int y)
    {
      this.point = new Point(x, y);
    }

    public bool IsEmpty
    {
      get => point.IsEmpty;
    }

    public int X
    {
      get => point.X;
      set => point.X = value;
    }

    public int Y
    {
      get => point.Y;
      set => point.Y = value;
    }

    public Size ToSize(Metrics metrics)
    {
      var w = (X * metrics.Unit) + ((X - 1) * metrics.Margin);
      var h = (Y * metrics.Unit) + ((Y - 1) * metrics.Margin);
      return new Size(w, h);
    }

    public override int GetHashCode()
    {
      return point.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      return point.Equals(obj);
    }

    public static Extent operator +(Extent a, Extent b)
    {
      return new Extent(a.X + b.X, a.Y + b.Y);
    }

    public static Extent operator -(Extent a, Extent b)
    {
      return new Extent(a.X - b.X, a.Y - b.Y);
    }

    public static bool operator ==(Extent left, Extent right)
    {
      return left.point == right.point;
    }

    public static bool operator !=(Extent left, Extent right)
    {
      return left.point != right.point;
    }
  }
}