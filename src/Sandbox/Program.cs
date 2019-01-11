using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Lib;

namespace Sandbox
{
  class Program
  {
    static void Main(string[] args)
    {
      Debug.WriteLine(
        new UriString("http://host.com/path")
          .SetArgs("one=1&ten=10")
          .SetArgs("a=b&a=c&a=d")
      );

      Debug.WriteLine(
        new UriString("http://host.com/path")
          .SetArgs(new
          {
            one = 1,
            ten = "10",
            a = new[] { "b", "c", "d" }
          })
      );
    }
  }
}
