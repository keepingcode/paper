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
  class A { }
  class B { }

  class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      try
      {
        var entity = new Entity();

        entity.AddProperties(new
        {
          graph = new
          {
            id = 10,
            items = new HashMap
            {
              { "one", new { id = 1, name = "one" } },
              { "two", new { id = 2, name = "two" } }
            }
          }
        }
        , select: new[] { "graph.items.one", "graph.items.two" }
        , except: new[] { "graph.items.one.name" }
        );

        // entity.SetTitle("My Title");
        //entity.SetProperty("my.count", 3);
        //entity.WithProperties("my.numbers").SetProperty("ten", 10);
        //entity.WithProperties("my.numbers").SetProperty("two.five", 5);

        //entity.Properties
        //entity.Actions
        //entity.Links

        //{
        //  Title = "Foo Bar",
        //  Properties = new PropertyMap
        //  {
        //    { "Id", 10 },
        //    { "Name", "Foo" },
        //    { "Group",
        //      new PropertyMap {
        //        { "Id", 20 },
        //        { "Name", "Bar" }
        //      }
        //    },
        //    { "Nums", new string[] { "One", "Two", "Three" } },
        //    { "Texs", new List<string> { "One", "Two", "Three" } }
        //  }
        //};

        try
        {
          using (var memory = new MemoryStream())
          {
            var serializer = new DataContractJsonSerializer(typeof(Entity));
            serializer.WriteObject(memory, entity);
            memory.Position = 0;
            var json = new StreamReader(memory).ReadToEnd();
            Debug.WriteLine(Json.Beautify(json));
          }
        }
        catch (Exception ex)
        {
          ex.Trace();
        }

        Debug.WriteLine(entity.ToJson());
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}