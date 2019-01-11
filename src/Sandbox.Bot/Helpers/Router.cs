using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Sandbox.Bot.Forms;
using Sandbox.Bot.Net;

namespace Sandbox.Bot.Helpers
{
  static class Router
  {
    public static void OpenPaper(string route, params string[] args)
    {
      // PaperClient client

      //O PAPERCLIENT DEVERIA SER ESTATICO
      //  ASSIM COM ESTE CORE DO BOT EH ESTATICO

    }

    public static void OpenPaper(Entity entity)
    {
      var mainForm = Application.OpenForms.Cast<Form>().OfType<MainForm>().FirstOrDefault();

    }
  }
}
