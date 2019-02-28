using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Paper.Browser.Gui.Widgets
{
  public interface IInputWidget : IWidget
  {
    Field Field { get; set; }

    bool Required { get; set; }

    bool Changed { get; }

    void CommitChanges();

    void RevertChanges();

    bool ValidateContent();
  }
}
