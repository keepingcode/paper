using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Sandbox.Lib;
using Sandbox.Lib.Domain;
using Sandbox.Lib.Domain.Dbo;
using Sandbox.Lib.Domain.SmallApi;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;
using Toolset.Xml;

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
        SequelSettings.TraceQueries = true;

        Table table = new TBusuario();
        Entity targetEntity = new Entity();
        DbEntities.CopyTable(table, targetEntity);
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}