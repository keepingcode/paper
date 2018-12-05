using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Paper.Media;
using Paper.Media.Routing;
using Paper.Media.Serialization;
using Toolset;

namespace Sandbox
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        var entity = new Entity();
        Trace.WriteLine(new MediaSerializer().SerializeToJson(entity));

        entity.Class.Add("one,two");
        entity.Title = "MyTitle";
        entity.Rel.Add("one,two");

        entity.Properties.Add("this", "that");

        Trace.WriteLine(Json.Beautify(new MediaSerializer().SerializeToJson(entity)));
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}