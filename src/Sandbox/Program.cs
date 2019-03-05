using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
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
  public interface IWidget
  {
    Size Extent { get; }
  }

  public class Widget : IWidget
  {
    public Size Extent { get; set; }
  }

  public static class Program
  {
    public static Size Measure(IEnumerable<IWidget> widgets)
    {
      int cols = 0;
      int rows = 0;

      int maxCols = 0;
      int maxRows = 12;
      do
      {
        maxCols += 6;

        var size = widgets.Select(x => x.Extent).Aggregate(new Size(0, 1), (prev, curr) =>
        {
          var calc = (prev.Width + curr.Width) <= maxCols
            ? new Size(prev.Width + curr.Width, Math.Max(prev.Height, curr.Height))
            : new Size(curr.Width, prev.Height + curr.Height);
          cols = Math.Max(cols, calc.Width);
          return calc;
        });

        rows = size.Height;
      } while (rows > maxRows);

      int unit = 37;
      int margin = 6;

      var extent = new Size(
        width: (cols * unit) + ((cols - 1) * margin),
        height: (rows * unit) + ((rows - 1) * margin)
      );
      return extent;
    }

    [STAThread]
    public static void Main(string[] args)
    {
      try
      {
        var widgets = new Widget[]
        {
          //new Widget{ Extent = new Size(1,1) },
          new Widget{ Extent = new Size(5,6) },
          //new Widget{ Extent = new Size(1,1) },
          //new Widget{ Extent = new Size(1,1) },
          new Widget{ Extent = new Size(3,1) },
          //new Widget{ Extent = new Size(1,1) },
          //new Widget{ Extent = new Size(1,1) },
          //new Widget{ Extent = new Size(3,1) },
          //new Widget{ Extent = new Size(1,1) },

          //new Widget{ Extent = new Size(3,1) },
          //new Widget{ Extent = new Size(1,1) },
          //new Widget{ Extent = new Size(1,1) },
          //new Widget{ Extent = new Size(3,1) },
          //new Widget{ Extent = new Size(1,1) },
          //new Widget{ Extent = new Size(1,1) },
          //new Widget{ Extent = new Size(3,1) },
          //new Widget{ Extent = new Size(1,1) },

          //new Widget{ Extent = new Size(2,1) },
          //new Widget{ Extent = new Size(3,1) },
        };

        Debug.WriteLine(Measure(widgets));
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}