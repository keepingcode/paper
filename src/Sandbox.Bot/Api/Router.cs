using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Sandbox.Bot.Api;
using Sandbox.Bot.Forms;
using Sandbox.Lib;
using Toolset;

namespace Sandbox.Bot.Api
{
  static class Router
  {
    public async static Task OpenPaperAsync(string route, params string[] args)
    {
      route = new UriString(route).SetArgs(args);

      var client = MediaClient.Current;
      var entity = await client.TransferAsync(route);
      if (!entity.OK)
      {
        var dialog = new FaultDialog(entity);
        dialog.ShowDialog(MainForm.Current);
        return;
      }

      await OpenPaperAsync(entity.Value);
    }

    public async static Task OpenPaperAsync(Entity entity)
    {
      var form = new PaperForm(entity);
      form.MdiParent = MainForm.Current;
      form.Show();
    }
  }
}
