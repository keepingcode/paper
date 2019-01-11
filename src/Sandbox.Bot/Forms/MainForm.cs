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
using Sandbox.Bot.Forms.Papers;
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
            item.Click += (o, e) => Router.OpenPaper(link.Href);
            mnMenu.DropDownItems.Add(item);
          }
        }
      }

      var sandbox = new ToolStripMenuItem();
      sandbox.Text = "Sandbox";
      sandbox.Click += (o, e) =>
      {
        var paperForm = (
          from form in Application.OpenForms.OfType<PaperForm>()
          where form.PaperControl is SandboxPaper
          select form
        ).FirstOrDefault();

        if (paperForm == null)
        {
          paperForm = new PaperForm(null);
          paperForm.MdiParent = this;
        }

        paperForm.Focus();
        paperForm.Show();
      };
      mnMenu.DropDownItems.Add(sandbox);
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
