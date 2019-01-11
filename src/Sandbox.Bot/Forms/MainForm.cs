using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Paper.Media.Design.Papers;
using Sandbox.Bot.Helpers;
using Sandbox.Bot.Net;

namespace Sandbox.Bot.Forms
{
  public partial class MainForm : Form
  {
    private readonly PaperClient client;

    private Blueprint blueprint;
    private Entity blueprintEntity;

    public MainForm(PaperClient client, Blueprint blueprint, Entity blueprintEntity)
    {
      this.client = client;
      this.blueprint = blueprint;
      this.blueprintEntity = blueprintEntity;
      
      InitializeComponent();

      var favicon = Favicon.Load();
      if (favicon != null)
      {
        this.Icon = favicon;
      }

      Text = this.blueprint.Info?.Title ?? "PaperBot";

      mnAbout.Text = $"&Sobre {Text}";

      if (blueprintEntity.Links?.Any() == true)
      {
        mnMenu.Enabled = true;

        foreach (var link in blueprintEntity.Links)
        {
          var item = new ToolStripMenuItem();
          if (!string.IsNullOrEmpty(link.Title))
          {
            item.Text = link.Title;
            mnMenu.DropDownItems.Add(item);
          }
        }
      }
    }

    private void ShowAbout()
    {
      using (var dialog = new AboutDialog(this.blueprint))
      {
        dialog.ShowDialog(this);
      }
    }

    private void mnAbout_Click(object sender, EventArgs e)
    {
      ShowAbout();
    }
  }
}
