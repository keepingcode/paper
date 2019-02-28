using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Papers;
using Paper.Media;
using Toolset;
using Toolset.Collections;
using static Toolset.Ret;

namespace Paper.Browser.Lib
{
  public class Window
  {
    private readonly object synclock = new object();

    private Ret<Content> contentRet;
    private IPaper paper;

    public Window(string name)
    {
      this.Name = name;
      this.Form = new WindowForm(this) { Name = name };
    }

    public WindowForm Form { get; }

    public string Name { get; }

    public Content Content => contentRet.Value;

    public void SetContent(Ret<Content> contentRet, Func<Window, Content, IPaper> paperFactory = null)
    {
      if (this.paper != null)
      {
        DisconnectEvents(paper);
      }

      this.contentRet = contentRet;
      Form.Call(() =>
      {
        this.paper = CreatePaper(contentRet.Value, paperFactory);
        CreateActions(this.paper, contentRet.Value);
        FeedbackSelection();
      });

      if (this.paper != null)
      {
        ConnectEvents(paper);
      }
    }

    private IPaper CreatePaper(Content content, Func<Window, Content, IPaper> paperFactory = null)
    {
      IPaper paper;
      try
      {
        var entity = content.Data as Entity;
        if (entity != null)
        {
          this.Form.Text = entity.Title;
        }
        paper = paperFactory?.Invoke(this, content) ?? PaperFactory.CreatePaper(this, content);
      }
      catch (Exception ex)
      {
        var href = contentRet.Value?.Href;
        content = new Content
        {
          Href = href,
          Data = HttpEntity.Create(href, ex).Value
        };
        paper = new StatusPaper(this, content);
      }

      Form.PageContainer.Controls.Clear();
      Form.PageContainer.Controls.Add(paper.Control);

      return paper;
    }

    private void CreateActions(IPaper paper, Content content)
    {
      var entity = content.Data as Entity;
      if (entity == null)
        return;

      var actions = entity.Actions;
      if (actions == null)
        return;

      var actionButtons = (
        from item in Form.ToolBar.Items.OfType<ToolStripButton>()
        where item.Alignment == ToolStripItemAlignment.Left
        select item
      ).ToArray();

      actionButtons.ForEach(x => Form.ToolBar.Items.Remove(x));
      Form.ActionBar.Items.Clear();

      var selectionBound = false;

      foreach (var action in actions)
      {
        var fields = action.Fields ?? Enumerable.Empty<Field>();

        var selectionField = fields.FirstOrDefault(x => x.Provider?.Rel.Has(RelNames.Self) == true);
        var bindToSelection = (selectionField != null);
        if (bindToSelection)
        {
          selectionBound = true;

          var selectionButton = new ToolStripButton();
          selectionButton.Tag = action;
          selectionButton.Text = action.Title;
          selectionButton.Click += (o, e) => PerformAction(action);
          selectionButton.Visible = false;
          Form.ToolBar.Items.Add(selectionButton);
        }

        var button = new ToolStripButton();
        button.Tag = action;
        button.Text = action.Title;
        button.Click += (o, e) => PerformAction(action);
        button.Padding = new Padding(10, button.Padding.Top, 10, button.Padding.Bottom);
        Form.ActionBar.Items.Add(button);
      }

      if (paper is ISelectable selectable)
      {
        selectable.SelectionEnabled = selectionBound;
      }
    }

    private void ConnectEvents(IPaper paper)
    {
      if (paper is ISelectable selelectable)
      {
        selelectable.SelectionChanged += Paper_SelectionChanged;
      }
    }

    private void DisconnectEvents(IPaper paper)
    {
      if (paper is ISelectable selelectable)
      {
        selelectable.SelectionChanged -= Paper_SelectionChanged;
      }
    }

    private void Paper_SelectionChanged(object sender, EventArgs e)
    {
      FeedbackSelection();
    }

    private void FeedbackSelection()
    {
      Form.Call(() =>
      {
        var selectable = paper as ISelectable;
        if (selectable == null)
          return;

        var count = selectable.GetSelection().Count();

        Form.SelectionLabel.Text =
          (count == 0) ? "" : (count == 1) ? "1 selecionado" : $"{count} selecionados";

        var actionButtons =
          from item in Form.ToolBar.Items.Cast<ToolStripItem>()
          where item.Alignment == ToolStripItemAlignment.Left
          select item;

        actionButtons.ForEach(x => x.Visible = count > 0);
      });
    }

    public void PerformAction(EntityAction entityAction)
    {
      var entity = (Entity)Content.Data;
      var selection = (paper as ISelectable)?.GetSelection().OfType<Entity>().ToArray();
      var action = new Action(this, entity, entityAction, selection);
      action.Form.Show(this.Form);
    }

    public void Invalidate()
    {
      Form.Call(() => Form.Overlay = true);
    }

    public void Validate()
    {
      Form.Call(() =>
      {
        Form.Pack();
        Form.Overlay = false;
      });
    }

    public void ViewSource()
    {
      var target = $"{Name}_source";
      var window = Navigator.Current.CreateWindow(target, parent: this.Form);
      window.SetContent(contentRet, (w, content) => new TextPlainPaper(w, contentRet.Value));
      window.Validate();
    }

    public async Task<Window> NavigateAsync(string uri, string target = TargetNames.Self)
    {
      return await Navigator.Current.NavigateAsync(uri, target, this);
    }
  }
}