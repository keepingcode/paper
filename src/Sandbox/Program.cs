using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;
using Toolset.Serialization;
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
        var entity = new Entity
        {
          Title = "My Title",
          Class = new[] { ClassNames.Data, ClassNames.Rows, "Users" },
          //Class = new[] { ClassNames.Rows, "Users" },
          Rel = new[] { "3rd", "4th" },
          Properties = new PropertyCollection
          {
            new Property("MyEntity", "Talz"),
            new Property("Info", new
            {
              Graph = 14002.01
            })
          }
        };

        foreach (var i in Enumerable.Range(1, 100))
        {
          entity.AddEntity(new Entity
          {
            Title = "My Title",
            Class = new[] { ClassNames.Row, "User" },
            Rel = new[] { RelNames.Row },
            Properties = new PropertyCollection
            {
              new Property("Id", i*150),
              new Property("Graph", new
              {
                Dez = i * 10.01,
                DhEmi = DateTime.Now
              })
            }
          });
        }

        //{
        //  var writer = new StringWriter();

        //  var serializer = new DocumentSerializer(DocumentSerializer.Json, indent: true);
        //  serializer.Serialize(entity, writer);

        //  Debug.WriteLine(writer);
        //}

        //{
        //  var writer = new StringWriter();

        //  var serializer = new DocumentSerializer(DocumentSerializer.Xml, indent: true);
        //  serializer.Serialize(entity, writer);

        //  Debug.WriteLine(writer);
        //}

        //{
        //  var writer = new StringWriter();

        //  var serializer = new DocumentSerializer(DocumentSerializer.Csv);
        //  serializer.Serialize(entity, writer);

        //  Debug.WriteLine(writer);
        //}

        {
          using (var stream = File.OpenWrite(@"c:\temp\file.xlsx"))
          {
            var serializer = new DocumentSerializer(DocumentSerializer.Excel);
            serializer.Serialize(entity, stream);
            stream.Flush();
          }
          Debug.WriteLine(@"Saved c:\temp\file.xlsx");
        }
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}