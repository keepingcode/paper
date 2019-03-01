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
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.CSharp;
using Paper.Api.Extensions.Papers;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Data;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Collections;
using Toolset.Data;
using Toolset.Net;
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
  public static class Program
  {
    [STAThread]
    public static void Main(string[] args)
    {
      try
      {
        var payload = new Payload();
        payload.SetProperty("form.edit.field1", "one");
        payload.SetProperty("form.edit.field2", "two");
        payload.SetProperty("record.@class", "MyClass");
        payload.SetProperty("record.id", "1");
        payload.SetProperty("record.name", "one");
        payload.SetProperty("records[0].@class", "MyClass");
        payload.SetProperty("records[0].id", "1");
        payload.SetProperty("records[0].name", "one");
        payload.SetProperty("records[2].@class", "MyClass");
        payload.SetProperty("records[2].id", "2");
        payload.SetProperty("records[2].name", "two");

        Debug.WriteLine("---");
        Debug.WriteLine(payload.GetProperty("form.edit.field1"));
        Debug.WriteLine(payload.GetProperty("form.edit.field2"));
        Debug.WriteLine(payload.GetProperty("record.@class"));
        Debug.WriteLine(payload.GetProperty("record.id"));
        Debug.WriteLine(payload.GetProperty("record.name"));
        Debug.WriteLine(payload.GetProperty("records[0].@class"));
        Debug.WriteLine(payload.GetProperty("records[0].id"));
        Debug.WriteLine(payload.GetProperty("records[0].name"));
        Debug.WriteLine(payload.GetProperty("records[2].@class"));
        Debug.WriteLine(payload.GetProperty("records[2].id"));
        Debug.WriteLine(payload.GetProperty("records[2].name"));
        Debug.WriteLine("---");

        //Debug.WriteLine(payload.ToEntity().ToJson());


        // var entity = new Entity();
        // entity.AddHeader("id", Class.Record);
        // entity.AddHeader("name", Class.Record);
        // entity.AddHeader("date", Class.Record);
        // 
        // var json = entity.ToJson();
        // var result = new MediaSerializer(MimeType.Siren).Deserialize(json);
        // Debug.WriteLine(result.ToJson());
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}