using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox.Widgets
{
  public interface IWidget : INotifyPropertyChanged
  {
    Control Control { get; }

    string Text { get; set; }

    object Value { get; set; }

    bool ReadOnly { get; set; }

    void OnPropertyChanged(string property);
  }
}
