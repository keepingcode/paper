using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Layouts;
using Paper.Browser.Gui.Widgets;
using Paper.Browser.Lib.Pages;
using Paper.Media;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;
using Toolset.Net;

namespace Paper.Browser.Lib
{
  public class Window
  {
    public event EventHandler SelectionChanged;

    private readonly HashMap<WindowAction> actions = new HashMap<WindowAction>();

    public Window(string name, Desktop desktop)
    {
      this.Name = name;
      this.Desktop = desktop;

      this.Host = new WindowForm();
      this.Host.FormClosing += async (o, e) => e.Cancel = !await CloseAsync();
      this.Host.KeyUp += Host_KeyUp;

      desktop.Windows.Add(this);
      FeedbackContent();
      FeedbackSelectionChanged();
    }

    public Mode Mode
    {
      get => Host.Mode;
      set
      {
        Host.Mode = value;
        FeedbackContent();
      }
    }

    public string Name { get; }

    public string Href { get; set; }

    public Desktop Desktop { get; }

    public WindowForm Host { get; }

    public Entity Entity { get; private set; }

    public IPage Page { get; private set; }

    public ICollection<Entity> GetSelection()
    {
      return (Page as ISelectablePage)?.GetSelection();
    }

    public void SetContent(Entity entity)
    {
      try
      {
        this.Host.SuspendLayout();
        this.Href = entity.GetSelfLink()?.Href;
        this.Host.Text = entity.Title ?? this.Href ?? "Janela";
        this.Entity = entity;
        this.Page = SetPage(entity);
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
      finally
      {
        this.Host.ResumeLayout();
        FeedbackContent();
      }
    }

    private IPage SetPage(Entity entity)
    {
      var currentPage = Host.ContentPane.Controls.OfType<ISelectablePage>().FirstOrDefault();
      if (currentPage != null)
      {
        currentPage.SelectionChanged -= CurrentPage_SelectionChanged;
      }

      IPage page;
      if (entity.Class.Has(ClassNames.Record))
      {
        page = new RecordPage();
      }
      else if (entity.Class.Has(ClassNames.Table))
      {
        page = new TablePage();
      }
      else
      {
        return null;
      }

      page.Window = this;
      page.Entity = entity;

      Host.WindowLayout = page.Host.AutoSize ? WindowLayouts.Flex : WindowLayouts.Fixed;
      Host.ContentPane.Controls.Clear();
      Host.ContentPane.Controls.Add(page.Host);

      Host.SelectionCountLabel.Text = "";

      if (page is ISelectablePage selectablePage)
      {
        selectablePage.SelectionChanged += CurrentPage_SelectionChanged;
      }

      return page;
    }

    public void PerformAction(Entity entity, IPage page, EntityAction entityAction, bool bindToSelection = false)
    {
      var selection = bindToSelection
        ? (page as ISelectablePage)?.GetSelection().OfType<Entity>().ToArray()
        : null;

      var action = new WindowAction(this, entity, entityAction, selection);

      this.Host.ToolBar.Enabled = this.Host.ActionBar.Enabled = false;
      action.Host.FormClosed += (o, e) => this.Host.ToolBar.Enabled = this.Host.ActionBar.Enabled = true;
      action.Host.Show(this.Host);
    }

    public async Task<Ret> RequestAsync(string uri, string method, Func<Entity, string> target, Entity data)
    {
      try
      {
        Host.DisplayStatus("Carregando...");
        Host.DisplayProgress();
        Application.DoEvents();

        var client = new HttpClient();

        var ret = await client.RequestAsync(uri, method, data);
        if (!ret.Ok)
        {
          // TODO: Emitir uma mensagem de falha
          return ret;
        }

        if (ret.Value?.Data is Entity entity)
        {
          var targetName = target.Invoke(entity);
          var window = Desktop.CreateWindow(this, targetName);
          window.SetContent(entity);
        }

        return ret;
      }
      catch (Exception ex)
      {
        // TODO: O que fazer com essa exceção
        ex.Trace();
        return ex;
      }
      finally
      {
        Host.ClearStatus();
        Host.ClearProgress();
      }
    }

    public async Task ReloadAsync()
    {
      if (string.IsNullOrEmpty(this.Href))
        return;

      await this.RequestAsync(this.Href);
    }

    public async Task<bool> CloseAsync()
    {
      Desktop.Windows.Remove(this);
      return await Task.FromResult(true);
    }

    private void FeedbackContent()
    {
      try
      {
        var fixedItems = new ToolStripItem[] {
          Host.SelectionCountLabel,
          Host.OkButton,
          Host.ExitButton
        };

        Host.ActionBar.Items.Clear();
        var items = Host.ToolBar.Items
          .Cast<ToolStripItem>()
          .Except(fixedItems)
          .ToArray();
        items.ForEach(item => Host.ToolBar.Items.Remove(item));

        if (this.Mode == Mode.SelectBox)
        {
          Host.ToolBar.Visible = true;
          Host.ActionBar.Visible = false;
          
          Host.OkButton.Visible = true;
          Host.ExitButton.Visible = true;

          FeedbackSelectBoxActions();
        }
        else
        {
          Host.ToolBar.Visible = true;
          Host.ActionBar.Visible = true;

          Host.OkButton.Visible = false;
          Host.ExitButton.Visible = false;

          FeedbackNavigationActions();
        }

        FeedbackSelectionChanged();
      }
      catch (Exception ex)
      {
        // TODO: O que fazer com essa exceção?
        ex.Trace();
      }
    }

    private void FeedbackSelectBoxActions()
    {

      if (Page is ISelectablePage selectable)
      {
        selectable.SelectionEnabled = true;
      }
    }

    private void FeedbackNavigationActions()
    {
      var actions = Entity?.Actions;
      if (actions == null)
        return;

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
          selectionButton.Click += (o, e) => PerformAction(Entity, Page, action, bindToSelection);
          selectionButton.Visible = false;
          Host.ToolBar.Items.Add(selectionButton);
        }
        //else
        {
          var button = new ToolStripButton();
          button.Tag = action;
          button.Text = action.Title;
          button.Click += (o, e) => PerformAction(Entity, Page, action);
          button.Padding = new Padding(10, button.Padding.Top, 10, button.Padding.Bottom);
          Host.ActionBar.Items.Add(button);
        }
      }

      if (Page is ISelectablePage selectable)
      {
        selectable.SelectionEnabled = selectionBound;
      }
    }

    private void FeedbackSelectionChanged()
    {
      var page = this.Page as ISelectablePage;
      if (page == null)
        return;

      var selectionCount = page.GetSelection().Count;

      var actionButtons =
        from item in Host.ToolBar.Items.OfType<ToolStripButton>()
        where item.Tag is EntityAction
        select item;
      actionButtons.ForEach(x => x.Visible = (selectionCount > 0));

      switch (selectionCount)
      {
        case 0:
          {
            Host.SelectionCountLabel.Text = (Mode == Mode.SelectBox)
              ? "Marque os itens desejados"
              : "";
            break;
          }
        case 1:
          {
            Host.SelectionCountLabel.Text = "1 selecionado";
            break;
          }
        default:
          {
            Host.SelectionCountLabel.Text = $"{selectionCount} selecionados";
            break;
          }
      }
    }

    private void CurrentPage_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        FeedbackSelectionChanged();
      }
      catch (Exception ex)
      {
        ex.Trace();
      }

      try
      {
        SelectionChanged?.Invoke(this, EventArgs.Empty);
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }

    private void Host_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Modifiers == Keys.None && e.KeyCode == Keys.F5)
      {
        ReloadAsync().RunAsync();
      }
    }
  }
}