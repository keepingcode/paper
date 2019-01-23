using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox.Bot.Forms.Papers
{
  interface IPaper
  {
    Control Control { get; }

    string Text { get; }

    Icon Icon { get; }
  }
}
