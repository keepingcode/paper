using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sandbox.Lib;
using Sandbox.Lib.Domain.Dbo;
using Toolset;
using Toolset.Data;

namespace Sandbox
{
  class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      try
      {
        var url = "http://host.com/path/:verb?id.min=10&id.max=20";
        Debug.WriteLine(url);

        var uri = (UriString)url;
        Debug.WriteLine(uri);

        uri = uri.SetArg("x", Range.Above(30));
        Debug.WriteLine(uri);

        // Debug.WriteLine(uri.GetArg<string>("arg"));
        // Debug.WriteLine(uri.GetArg<List<int>>("id"));
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}
