﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui.Layouts
{
  public interface IMessageSupport
  {
    Component StatusText { get; }

    Component ProgressBar { get; }
  }
}
