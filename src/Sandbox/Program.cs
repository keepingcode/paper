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
using Toolset.Serialization.Graph;
using Toolset.Serialization.Json;
using Toolset.Xml;

namespace Sandbox
{
  public class Other
  {
    public int Id { get; set; }
  }

  public class Prop
  {
    public string Key { get; set; }
    public object Value { get; set; }
  }

  public class Props : List<Prop>
  {
    public void Add(string key, object value)
    {
      this.Add(new Prop { Key = key, Value = value });
    }
  }

  public class Graph
  {
    public int Id { get; set; }
    public int[] Ids { get; set; }
    public List<Graph> Graphs { get; set; }
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
        //var json = @"{ ""properties"": { ""id"": 10 } }";
        var json = @"
        {
          ""class"": [
            ""data"",
            ""rows"",
            ""Users""
          ],
          ""title"": ""Users"",
          ""properties"": {
            ""count"": 2,
            ""actives"": 1,
            ""onlines"": 0
          },
          ""actions"": [
            {
              ""name"": ""Filter""
            }
          ],
          ""links"": [
            {
              ""rel"": [
                ""self""
              ],
              ""href"": ""http://host.com/Api/1/Users""
            }
          ],
          ""entities"": [
            {
              ""class"": [
                ""data"",
                ""rows"",
                ""Users""
              ],
              ""title"": ""Users"",
              ""properties"": {
                ""count"": 2,
                ""actives"": 1,
                ""onlines"": 0
              },
              ""actions"": [
                {
                  ""name"": ""Filter""
                }
              ],
              ""links"": [
                {
                  ""rel"": [
                    ""self""
                  ],
                  ""href"": ""http://host.com/Api/1/Users""
                }
              ]
            }
          ]
        }

        ";

        //        var json = @"
        //";


        using (var reader1 = new JsonReader(new StringReader(json)))
        using (var reader2 = new JsonReader(new StringReader(json)))
        using (var writer = new GraphWriter(typeof(Entity)))
        //using (var writer = new GraphWriter2(typeof(Graph)))
        {
          reader1.CopyTo(writer);
          reader2.CopyTo(writer);
          foreach (var graph in writer.Graphs)
          {
            Debug.WriteLine(graph.ToXElement());
          }
        }

        //var actives = 0;
        //var onlines = 0;

        //var rnd = new Random();

        //var entity = new Entity
        //{
        //  Title = "Users",
        //  Class = new[] { ClassNames.Data, ClassNames.Rows, "Users" },
        //  Properties = PropertyCollection.Create(
        //    new
        //    {
        //      Count = 2,
        //      Actives = 1,
        //      Onlines = 0
        //    }
        //  ),
        //  Entities = new EntityCollection
        //  {
        //    new Entity {
        //      Title = "Usuário",
        //      Class = new[] { ClassNames.Data, ClassNames.Row, "User" },
        //      Properties = PropertyCollection.Create(new {
        //        Id = 1,
        //        Name = "Fulano"
        //      }),
        //      Links = new LinkCollection { new Link { Rel = RelNames.Self, Href = "http://host.com/Api/1/Users/1" } }
        //    },
        //    new Entity {
        //      Title = "Usuário",
        //      Class = new[] { ClassNames.Data, ClassNames.Row, "User" },
        //      Properties = PropertyCollection.Create(new {
        //        Id = 2,
        //        Name = "Beltrano"
        //      }),
        //      Links = new LinkCollection { new Link { Rel = RelNames.Self, Href = "http://host.com/Api/1/Users/1" } }
        //    }
        //  },
        //  Links = new LinkCollection { new Link { Rel = RelNames.Self, Href = "http://host.com/Api/1/Users" } }
        //};

        //var serializer = new MediaSerializer(MediaSerializer.JsonSiren);
        //var str = serializer.Serialize(entity);
        //Debug.WriteLine(Json.Beautify(str));


        //var e = serializer.Deserialize(str);
        //Debug.WriteLine(e);

      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}