using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Browser.Lib;
using Paper.Media;
using Paper.Media.Design;

namespace Paper.Browser.Gui.Papers
{
  public static class PaperFactory
  {
    public static IPaper CreatePaper(Window window, Content content)
    {
      if (content.Data is Entity entity)
      {
        if (entity.Class.Has(ClassNames.Table))
        {
          return new TablePaper(window, content);
        }
        else if (entity.Class.Has(ClassNames.Record))
        {
          return new RecordPaper(window, content);
        }
        else if (entity.Class.Has(ClassNames.Status))
        {
          return new StatusPaper(window, content);
        }
      }
      return new TextPlainPaper(window, content);
    }
  }
}
