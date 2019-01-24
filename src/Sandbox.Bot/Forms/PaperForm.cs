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
using Sandbox.Bot.Forms.Papers;
using Sandbox.Bot.Properties;
using Toolset;

namespace Sandbox.Bot.Forms
{
  public partial class PaperForm : Form
  {
    private List<Form> ownedForms = new List<Form>();

    public PaperForm(Entity entity)
    {
      this.Entity = entity;
      InitializeComponent();
      InitializeBehaviour();
    }

    public Entity Entity { get; }

    private void InitializeBehaviour()
    {
      AddPaperControl();
      MapActions();
      MapLinks();
    }

    private void AddPaperControl()
    {
      IPaper paper = new DataPaper(Entity);

      paper.Control.Dock = DockStyle.Fill;
      tsContainer.ContentPanel.Controls.Add(paper.Control);

      this.Icon = paper.Icon;
      this.Text = paper.Text;
    }

    private void MapActions()
    {
      if (Entity.Actions == null)
        return;

      var sections =
        from action in Entity.Actions
        let priority =
          action.Rel.Has(RelNames.PrimaryLink) ? 1
          : action.Rel.Has(RelNames.SecondaryLink) ? 2
          : 3
        orderby priority
        group action by priority into g
        select g.ToArray();

      foreach (var section in sections)
      {
        foreach (var action in section)
        {
          var item = new ToolStripButton();
          item.Text = action.Title ?? (action.Name.ChangeCase(TextCase.ProperCase));
          item.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
          item.Image = Resources.Action;
          item.ImageAlign = ContentAlignment.MiddleLeft;
          item.Click += (o, e) => ExecuteAction(action);
          tbActions.Items.Add(item);
        }
        tbActions.Items.Add(new ToolStripSeparator());
      }

      var last = tbActions.Items.Cast<ToolStripItem>().LastOrDefault();
      if (last is ToolStripSeparator)
      {
        tbActions.Items.Remove(last);
      }
    }

    private void MapLinks()
    {
      if (Entity.Links == null)
        return;

      var linkFont = new Font(this.Font, FontStyle.Underline);

      var sections =
        from link in Entity.Links
        let priority =
          link.Rel.Has(RelNames.PrimaryLink) ? 1
          : link.Rel.Has(RelNames.SecondaryLink) ? 2
          : 3
        orderby priority
        group link by priority into g
        select g.ToArray();

      foreach (var section in sections)
      {
        foreach (var link in section)
        {
          if (string.IsNullOrWhiteSpace(link.Title))
            continue;

          var item = new ToolStripButton();
          item.Text = link.Title;
          item.Font = linkFont;
          item.ForeColor = Color.Blue;
          item.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
          item.Image = Resources.Link;
          item.ImageAlign = ContentAlignment.MiddleLeft;
          tbLinks.Items.Add(item);
        }
        tbLinks.Items.Add(new ToolStripSeparator());
      }

      var last = tbLinks.Items.Cast<ToolStripItem>().LastOrDefault();
      if (last is ToolStripSeparator)
      {
        tbLinks.Items.Remove(last);
      }
    }

    private void ExecuteAction(EntityAction action)
    {
      tbActions.Enabled = false;

      var dialog = new ActionForm(Entity, action);

      dialog.MdiParent = this.MdiParent;
      dialog.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
      
      dialog.FormClosed += (o, e) =>
      {
        tbActions.Enabled = true;
        ownedForms.Remove(dialog);
      };
      ownedForms.Add(dialog);

      dialog.Show();
    }

    private void ActivateOwnedForms()
    {
      foreach (var form in ownedForms)
      {
        form.Activate();
      }
    }

    private void PaperForm_Activated(object sender, EventArgs e)
    {
      ActivateOwnedForms();
    }
  }
}