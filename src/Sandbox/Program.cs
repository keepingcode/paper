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
        var actives = 0;
        var onlines = 0;

        var rnd = new Random();

        var users = new EntityCollection();
        foreach (var id in Enumerable.Range(1, 10))
        {
          var active = rnd.Next(2);
          var online = rnd.Next(2);

          actives += active;
          onlines += online;

          users.Add(new Entity
          {
            Title = $"Usuário {id}",
            Class = new[] { ClassNames.Data, ClassNames.Row, "Domain.Db.TBusuario" },
            Rel = new[] { RelNames.Rows },
            Properties = PropertyCollection.Create(
              new
              {
                Id = id,
                Name = $"Usuário {id}",
                Ativo = active == 1,
                Online = online == 1
              }
            )
          });
        }

        var entity = new Entity
        {
          Title = "Usuários no Período",
          Class = new[] { ClassNames.Data, ClassNames.Rows },
          Rel = new[] { "3rd", "4th" },
          Properties = PropertyCollection.Create(
            new {
              Total = users.Count,
              Ativos = actives,
              Online = onlines
            }
          ),
          Entities = users
        };

        

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
            var serializer = new MediaSerializer(MediaSerializer.Excel);
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