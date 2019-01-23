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
using Sandbox.Bot.Api;
using Sandbox.Bot.Forms.Papers;
using Sandbox.Bot.Helpers;

namespace Sandbox.Bot.Forms
{
  public partial class MainForm : Form
  {
    private readonly Blueprint blueprint;
    private readonly Entity blueprintEntity;

    public static MainForm Current
      => Application.OpenForms.Cast<Form>().OfType<MainForm>().FirstOrDefault();

    public MainForm(Blueprint blueprint, Entity blueprintEntity)
    {
      this.blueprint = blueprint;
      this.blueprintEntity = blueprintEntity;

      InitializeComponent();

      LoadFavicon();
      LoadProperties();
      LoadMenu();
    }

    private void LoadMenu()
    {
      mnMenu.Enabled = true;

      if (blueprintEntity.Links?.Any() == true)
      {
        foreach (var link in blueprintEntity.Links)
        {
          var item = new ToolStripMenuItem();
          if (!string.IsNullOrEmpty(link.Title))
          {
            item.Text = link.Title;
            item.Click += async (o, e) => await Router.OpenPaperAsync(link.Href);
            mnMenu.DropDownItems.Add(item);
          }
        }
      }
    }

    private void LoadFavicon()
    {
      var favicon = Favicon.Load();
      if (favicon != null)
      {
        this.Icon = favicon;
      }
    }

    private void LoadProperties()
    {
      Text = blueprint.Info?.Title ?? "PaperBot";
      mnAbout.Text = $"&Sobre {Text}";
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
