using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolset;

namespace Sandbox.Bot.Helpers
{
  static class Favicon
  {
    public static void Save(byte[] icon)
    {
      var filepath = Path.Combine(App.Path, "favicon.ico");
      File.WriteAllBytes(filepath, icon);
    }

    public static Icon Load()
    {
      try
      {
        var filepath = Path.Combine(App.Path, "favicon.ico");
        using (var stream = File.OpenRead(filepath))
        {
          var icon = new Icon(stream);
          return icon;
        }
      }
      catch (Exception ex)
      {
        ex.Trace();
        return null;
      }
    }
  }
}
