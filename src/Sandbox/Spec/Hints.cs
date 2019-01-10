using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Spec
{
  [Flags]
  enum Hints
  {
    Primary   = 0b00000001,
    Secondary = 0b00000010,

    Add       = 0b00000100,
    Save      = 0b00001000,
    Delete    = 0b00010000,
    Check     = 0b00100000,
  }
}
