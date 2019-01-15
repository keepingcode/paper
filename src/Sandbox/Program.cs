using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sandbox.Lib;
using Toolset;

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
        var url = "http://host.com/path/:verb?arg=val";
        Debug.WriteLine(url);

        var uri = (UriString)url;
        Debug.WriteLine(uri);

        //uri = uri.SetProtocol("protocol");
        //Debug.WriteLine(uri);

        //uri = uri.SetHost("host.com");
        //Debug.WriteLine(uri);

        //uri = uri.SetPath("/path");
        //Debug.WriteLine(uri);

        uri = uri.SetArgs("?id=10&id=20?id=40");
        Debug.WriteLine(uri);

        Debug.WriteLine(uri.GetArg<string>("arg"));
        Debug.WriteLine(uri.GetArg<List<int>>("id"));
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}
