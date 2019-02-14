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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.CSharp;
using Paper.Api.Rendering;
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
  class Program
  {
    [STAThread]
    static void Main()
    {
      System.Windows.Forms.Application.EnableVisualStyles();
      System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
      try
      {
        var siteDescriptor = new SiteDescriptor();
        var paperRenderer = new PaperRenderer(siteDescriptor, typeof(MyPaper));

        siteDescriptor.MapRoute("/Users", opt => opt
          .On(Method.Get, paperRenderer.GetAsync)
          //.On(Method.Post, paperRenderer.PostAsync)
        );

        // siteDescriptor.MapRoute("/Users/{userId}", opt => opt
        //   .On(Method.Get, paperRenderer.GetAsync)
        //   .On(Method.Post, paperRenderer.PostAsync)
        // );

        // siteDescriptor.Linkage(
        //   "/Users/{userId}", opt => opt
        // );
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }

  public delegate Task Linkage();

  

  class PaperRenderer
  {
    public PaperRenderer(SiteDescriptor siteDescriptor, Type type)
    {
    }

    public async Task GetAsync(Request request, Response response, NextAsync next)
    {
      await Task.Yield();
    }

    public async Task PostAsync(Request request, Response response, NextAsync next)
    {
      await Task.Yield();
    }
  }

  class SiteDescriptor
  {
    public SiteDescriptor MapRoute(string route, Action<Options> options)
    {
      return this;
    }

    public class Options
    {
      public Options On(Method get, Renderer render)
      {
        return this;
      }
    }
  }

  class MyPaper
  {
  }
}