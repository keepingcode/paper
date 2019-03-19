using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Paper.Browser.Gui
{
  public interface IFieldWidget : IWidget
  {
    event EventHandler FieldChanged;
    event EventHandler ValueChanged;

    Entity Entity { get; set; }

    Field Field { get; set; }

    bool HasChanges { get; }

    IEnumerable<string> GetErrors();
  }
}
