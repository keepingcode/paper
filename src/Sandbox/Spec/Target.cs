using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Sandbox.Spec
{
  class Target
  {
    public static implicit operator Target(Entity entity)
    {
      return new Target();
    }
  }
}
