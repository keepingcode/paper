using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.CSharp;
using Paper.Media;
using Paper.Media.Data;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Collections;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;
using Toolset.Serialization;
using Toolset.Serialization.Csv;
using Toolset.Serialization.Graph;
using Toolset.Serialization.Json;
using Toolset.Serialization.Xml;
using Toolset.Xml;

namespace Sandbox
{
  class Class
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? Date { get; set; }
  }

  class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      try
      {
        //var uri = new UriString("http://localhost.com/?page=10&pageSize=20");
        //var uri = new UriString("http://localhost.com/?limit=10&offset=20");

        var sort = new Sort()
          .AddField<Class>(opt => opt.Id)
          .AddField<Class>(opt => opt.Name)
          .AddField<Class>(opt => opt.Date);

        sort.CopyFrom("http://localhost.com/?sort=id&sort=name:desc");
        Debug.WriteLine(sort);

        sort.AddSortedField("date", Paper.Media.Data.SortOrder.Descending);

        var uri = sort.CreateUri("http://localhost.com/");
        Debug.WriteLine(uri);

        return;

        var entity = new Entity();
        entity.SetTitle("Teste");


        


        entity.ExpandUri("http://localhost/", "/api/1");
        Debug.WriteLine(entity.ToJson());


        //try
        //{
        //  using (var memory = new MemoryStream())
        //  {
        //    var serializer = new DataContractJsonSerializer(typeof(Entity));
        //    serializer.WriteObject(memory, entity);
        //    memory.Position = 0;
        //    var json = new StreamReader(memory).ReadToEnd();
        //    Debug.WriteLine(Json.Beautify(json));
        //  }
        //}
        //catch (Exception ex)
        //{
        //  ex.Trace();
        //}
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}