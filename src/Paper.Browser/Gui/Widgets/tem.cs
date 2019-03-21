using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Paper.Browser.Lib;
using Paper.Media;

namespace Paper.Browser.Gui.Widgets
{
  public interface tem
  {
    IContainer Components { get; }
    Entity Entity { get; set; }
    Field Field { get; set; }
    Extent GridExtent { get; set; }
    bool HasChanges { get; }
    UserControl Host { get; }
    Href Href { get; set; }
    NameCollection Keys { get; set; }
    Label Label { get; }
    bool Multiple { get; }
    bool Required { get; }
    object Value { get; set; }
    Window Window { get; set; }

    event EventHandler FieldChanged;
    event EventHandler ValueChanged;

    IEnumerable<string> GetErrors();
  }
}