using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Paper.Media;
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
  class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      try
      {
        var code = 2;

        #region 1 - Json to Entity
        if (code == 1)
        {
          var text = @"
            {
              ""rows"": [
                {
                  ""@class"": ""Users"",
                  ""total"": 4
                },
                {
                  ""@class"": ""User"",
                  ""id"": 1,
                  ""name"": ""One""
                },
                {
                  ""@class"": ""User"",
                  ""id"": 2,
                  ""name"": ""Two""
                },
                {
                  ""@class"": ""Profile"",
                  ""id"": 1,
                  ""name"": ""1st""
                },
                {
                  ""@class"": ""Profile"",
                  ""id"": 2,
                  ""name"": ""2nd""
                }
              ]
            }
        ";
          var serializer = new MediaSerializer();
          var entity = serializer.Deserialize(text);
          Debug.WriteLine(entity.ToJson());
        }
        #endregion

        #region 2 - Csv To Entity
        if (code == 2)
        {
          var text =
 @"
""@class"", ""total""
""Users"", 4
""@class"", ""id"", ""name""
""User"", 1, ""One""
""User"", 2, ""Two""
""@class"", ""id"", ""name""
""Profile"", 1, ""1st""
""Profile"", 2, ""2nd""
";
          var serializer = new MediaSerializer();
          var entity = serializer.Deserialize(text);
          Debug.WriteLine(entity.ToJson());
        }
        #endregion

        #region 3 - Entity To Json
        if (code == 3)
        {
          var entity = new Entity();
          entity.AddClass(ClassNames.Data, "Users");
          entity.AddProperties(new
          {
            Total = 4
          });
          entity.AddEntity(
            new Entity()
              .AddClass(ClassNames.Data, "User")
              .AddProperties(new
              {
                Id = 1,
                Name = "One"
              })
          );
          entity.AddEntity(
            new Entity()
              .AddClass(ClassNames.Data, "User")
              .AddProperties(new
              {
                Id = 2,
                Name = "Twe"
              })
          );
          entity.AddEntity(
            new Entity()
              .AddClass(ClassNames.Data, "Profile")
              .AddProperties(new
              {
                Id = 1,
                Name = "1st"
              })
          );
          entity.AddEntity(
            new Entity()
              .AddClass(ClassNames.Data, "Profile")
              .AddProperties(new
              {
                Id = 2,
                Name = "2nd"
              })
          );
          var serializer = new MediaSerializer("json");
          var json = serializer.Serialize(entity);
          Debug.WriteLine(Json.Beautify(json));
        }
        #endregion

        #region 4 - Read CSV
        if (code == 4)
        {
          var text =
@"
""@class"", ""total""
""Users"", 4
""@class"", ""id"", ""name""
""User"", 1, ""One""
""User"", 2, ""Two""
""@class"", ""id"", ""name""
""Profile"", 1, ""1st""
""Profile"", 2, ""2nd""
";
          using (var reader = new TransformReader(
            new CsvReader(
              new StringReader(text),
              new CsvSerializationSettings { HasHeaders = false }
            ),
            new CsvRowsTransform())
          )
          //using (var writer = new TraceWriter())
          using (var stringWriter = new StringWriter())
          using (var writer = new JsonWriter(stringWriter))
          {
            reader.CopyTo(writer);
            Debug.WriteLine(Json.Beautify(stringWriter.ToString()));
          }
        }
        #endregion

        #region 5 - EntityForm To Json
        if (code == 5)
        {
          var entity = new Entity();
          entity.AddClass(ClassNames.Form);
          entity.AddProperties(new
          {
            Id = 1,
            Name = "One"
          });
          var serializer = new MediaSerializer("json");
          var json = serializer.Serialize(entity);
          Debug.WriteLine(Json.Beautify(json));
        }
        #endregion

      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}