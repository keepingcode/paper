using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Widgets;
using Paper.Browser.Lib;
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
//using Paper.Browser.Lib;

namespace Paper.Browser
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>  
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      var desktop = new Desktop();
      desktop.Host.Load += async (o, e) =>
        {
          var data = new
          {
            Id = 1,
            Name = "The First",
            Date = DateTime.Now
          };

          var entity = new Entity();
          entity.AddClass(ClassNames.Record);
          entity.SetTitle("My First Page");
          entity.AddProperties(data);
          entity.AddHeaders(data, ClassNames.Record);

          desktop.CreateWindow().SetContent(entity);

          var task1 = desktop.CreateWindow().RequestAsync(
            "http://localhost:8080/Api/1/Paper/Api/Extensions/Papers/SamplePaper/TaskPaper/1?f=json+siren"
          );

          var task2 = desktop.CreateWindow().RequestAsync(
            "http://localhost:8080/Api/1/Paper/Api/Extensions/Papers/SamplePaper/TasksPaper?f=json+siren"
          );

          await task1;
          await task2;
        };

      Application.Run(desktop.Host);
    }
  }
}
