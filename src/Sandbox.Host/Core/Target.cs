using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paper.Media;

namespace Sandbox.Host.Core
{
  public class Target
  {
    private Target(Entity entity)
    {
      this.Value = entity;
    }

    private Target(Uri uri)
    {
      this.Value = uri;
    }

    public object Value { get; set; }

    public static implicit operator Target(Entity target)
    {
      return new Target(target);
    }

    public static implicit operator Uri(Target target)
    {
      return target?.Value as Uri;
    }

    public static implicit operator Target(Uri target)
    {
      return new Target(target);
    }
  }
}
