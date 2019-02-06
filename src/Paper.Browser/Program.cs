using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Base;
using Paper.Browser.Base.Forms;
using Paper.Browser.Commons;

namespace Paper.Browser
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>  
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      Navigator.Form.Load += (o, e) => Navigator.NewWindow().OpenAsync("~/Index");

      Application.Run(Navigator.Form);
    }
  }
}
