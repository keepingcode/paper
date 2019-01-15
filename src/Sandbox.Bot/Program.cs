using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sandbox.Bot.Forms;
using Sandbox.Widgets;

namespace Sandbox.Bot
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

      var launcher = new LauncherForm();
      Application.Run(launcher);

      if (launcher.DialogResult == DialogResult.OK)
      {
        Application.Run(new MainForm(launcher.Blueprint, launcher.BlueprintEntity));
      }
    }
  }
}
