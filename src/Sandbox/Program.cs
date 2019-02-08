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
  
  class Program
  {
    private static IEnumerable<string> Tokenize(string path)
    {
      var tokens = path.ToLower().Split('/').NonNullOrEmpty();
      tokens = Normalize(tokens).Reverse();
      return tokens;
    }

    private static IEnumerable<string> Normalize(IEnumerable<string> tokens)
    {
      int skip = 0;
      foreach (var token in tokens.Reverse())
      {
        if (token == ".")
        {
          continue;
        }
        if (token == "..")
        {
          skip++;
          continue;
        }
        if (skip > 0)
        {
          skip--;
          continue;
        }
        yield return token;
      }
    }

    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      try
      {
        var tokens = Tokenize("../a");
        Debug.WriteLine(string.Join("/", tokens));
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}