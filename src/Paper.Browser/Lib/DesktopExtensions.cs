using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Lib
{
  public static class DesktopExtensions
  {
    public static Window CreateWindow(this Desktop desktop, Target target)
    {
      return desktop.CreateWindow((Window)null, target.GetName());
    }

    public static Window CreateWindow(this Desktop desktop, string target = TargetNames.Blank)
    {
      return desktop.CreateWindow((Window)null, target);
    }

    public static Window CreateWindow(this Desktop desktop, Window current, Target target)
    {
      return desktop.CreateWindow(current, target.GetName());
    }
  }
}