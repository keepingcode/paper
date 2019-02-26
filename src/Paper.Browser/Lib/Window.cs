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
using static Toolset.Ret;

namespace Paper.Browser.Lib
{
  public class Window
  {
    private readonly object synclock = new object();

    private Ret<Content> contentRet;

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
      this.contentRet = contentRet;

      var entity = contentRet.Value?.Data as Entity;
      if (entity != null)
      {
        this.Form.Text = entity.Title;
      }

      IPaper paper;

      try
      {
        var content = contentRet.Value;
        paper = paperFactory?.Invoke(this, content) ?? PaperFactory.CreatePaper(this, content);
      }
      catch (Exception ex)
      {
        var href = contentRet.Value?.Href;
        var content = new Content
        {
          Href = href,
          Data = HttpEntity.Create(href, ex).Value
        };
        paper = new StatusPaper(this, content);
      }

      Form.Call(() =>
      {
        Form.PageContainer.Controls.Clear();
        Form.PageContainer.Controls.Add(paper.Control);
      });
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
      var window = Navigator.Current.CreateWindow(target);
      window.SetContent(contentRet, (w, content) => new TextPlainPaper(w, contentRet.Value));
      window.Validate();
    }

    public async Task<Window> NavigateAsync(string uri, string target = TargetNames.Self)
    {
      return await Navigator.Current.NavigateAsync(uri, target, this);
    }
  }
}