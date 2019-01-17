using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolset;
using Toolset.Sequel;

namespace Sandbox.Lib.Domain
{
  public class Db : SequelScope
  {
    public Db()
      : base("server=172.27.0.102;database=DBdirector_trigovita_sf_29;uid=sl;pwd=123;")
    {
    }
  }
}