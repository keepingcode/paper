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
using Paper.Media.Serialization;
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

        using (var db = new Db())
        {
          Table table = TBusuario.Find(1);
          Filter filter = new TBusuario.Filter();
          filter.Row = Range.Between(10, 50);
          Entity entity = new Entity();
          DbEntities.CopyTable(table, filter, entity);

          entity.Canonicalize("http://host.com/Api/1?f=json");

          var serializer = new MediaSerializer();
          var json = serializer.SerializeToJson(entity);
          Debug.WriteLine(Json.Beautify(json));
        }
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}