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
    Field Field { get; set; }

    object Value { get; set; }

    bool HasChanges { get; }

    bool Validate();
  }
}
