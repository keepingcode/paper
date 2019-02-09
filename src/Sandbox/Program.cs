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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
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
  class Record
  {
    public int Id { get; set; }
    public string Name { get; set; }
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
        var entity = new Entity();

        var record1 = new Record { Id = 1, Name = "One" };
        var record2 = new Record { Id = 2, Name = "Two" };
        var records = new[]
        {
          new Record { Id = 3, Name = "Three" },
          new Record { Id = 3, Name = "Four" }
        };

        // entity.SetRecord(record1, select: new[] { "Id" });
        // entity.AddHeaders<Record>(new[] { "record", "list", "table" }, select: new[] { "Id" });

        entity.SetRecordAndHeaders(record1, select: new[] { "Id" });

        Debug.WriteLine(entity.ToJson());
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}