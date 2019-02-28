﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper.Browser.Gui.Widgets
{
  public interface IWidget
  {
    Control Host { get; }

    string Text { get; set; }

    object Content { get; set; }
  }
}
