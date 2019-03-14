using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Paper.Browser.Lib.Pages
{
  public interface ISelectablePage : IPage
  {
    event EventHandler SelectionChanged;

    bool SelectionEnabled { get; set; }

    ICollection<Entity> GetSelection();
  }
}
