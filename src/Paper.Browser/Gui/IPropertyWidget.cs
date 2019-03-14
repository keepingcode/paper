using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media.Design;

namespace Paper.Browser.Gui
{
  public interface IPropertyWidget : IWidget
  {
    IHeaderInfo Header { get; set; }

    string Text { get; set; }

    object Value { get; set; }
  }
}
