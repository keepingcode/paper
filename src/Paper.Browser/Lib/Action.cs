using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Widgets;
using Paper.Media;
using Toolset;
using Toolset.Collections;

namespace Paper.Browser.Lib
{
  public class Action
  {
    public Action(Window window, Entity entity, EntityAction entityAction, ICollection<Entity> selection = null)
    {
      this.Window = window;
      this.Entity = entity;
      this.EntityAction = entityAction;
      this.Selection = selection;
      this.Form = new ActionForm(this);
    }

    public ActionForm Form { get; }

    public Window Window { get; }

    public Entity Entity { get; }

    public EntityAction EntityAction { get; }

    public ICollection<Entity> Selection { get; }

    public bool Submit()
    {
      bool ok;

      ok = Validate();
      if (!ok)
        return false;

      ok = SendForm();
      if (!ok)
        return false;

      Window.NavigateAsync(Window.Content.Href).NoAwait();
      return true;
    }

    private bool Validate()
    {
      bool isValid = true;
      foreach (var widget in Form.Widgets())
      {
        if (!widget.ValidateContent())
        {
          isValid = false;
        }
      }
      return isValid;
    }

    private bool SendForm()
    {
      foreach (var widget in Form.Widgets())
      {
        widget.Field.Value = widget.Content;
      }

      // TODO: enviar para o servidor

      Form.Widgets().ForEach(x => x.CommitChanges());
      return true;
    }
  }
}