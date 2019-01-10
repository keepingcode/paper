using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Spec
{
  class TextAttribute : DisplayNameAttribute
  {
    public TextAttribute(string name)
      : base(name)
    {
    }
  }
}