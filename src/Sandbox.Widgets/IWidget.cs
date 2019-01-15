using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Widgets
{
  interface IWidget : IComponent, INotifyPropertyChanged
  {
    string Text { get; set; }

    object Value { get; set; }

    void OnPropertyChanged(string property);
  }
}
