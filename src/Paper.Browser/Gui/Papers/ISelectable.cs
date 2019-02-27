using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Gui.Papers
{
  public interface ISelectable
  {
    event EventHandler SelectionChanged;

    bool SelectionEnabled { get; set; }

    IEnumerable<object> GetSelection();
  }
}
