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
        var text = "My.FirstAttempt.Of.Doing_it";

        Debug.WriteLine(text.ChangeCase(TextCase.ProperCase));
        Debug.WriteLine(text.ChangeCase(TextCase.PascalCase));
        Debug.WriteLine(text.ChangeCase(TextCase.CamelCase));

        Debug.WriteLine("- - -");
        Debug.WriteLine(text.ChangeCase(TextCase.PreserveSpecialCharacters | TextCase.ProperCase));
        Debug.WriteLine(text.ChangeCase(TextCase.PreserveSpecialCharacters | TextCase.Underscore));
        Debug.WriteLine(text.ChangeCase(TextCase.PreserveSpecialCharacters | TextCase.CamelCase));

      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}