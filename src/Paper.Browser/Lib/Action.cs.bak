using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Widgets;
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Serialization;
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
      try
      {
        Form.SetBusy(true);

        Ret ret;

        ret = Validate();
        if (!ret.Ok)
          return false;

        ret = SendFormAsync().RunSync();
        if (!ret.Ok)
          return false;

        Form.Widgets().ForEach(x => x.CommitChanges());
        Form.Close();

        Window.NavigateAsync(Window.Content.Href).NoAwait();

        return true;
      }
      finally
      {
        Form.SetBusy(false);
      }
    }

    private Ret Validate()
    {
      try
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
      catch (Exception ex)
      {
        // TODO: O que fazer com essa exceção?
        ex.Trace();
        return ex;
      }
    }

    private async Task<Ret> SendFormAsync()
    {
      try
      {
        var action = this.EntityAction;

        var payload = new Payload();
        foreach (var widget in Form.Widgets())
        {
          var field = widget.Field;
          var value = widget.Content;
          payload.SetProperty(field.Name, value);
        }

        var ret = await Navigator.Current.RequestAsync(action.Href, action.Method, payload);

        var entity = ret.Value?.Data as Entity;
        var entityIsStatus = (entity != null) && entity.Class.Has(ClassNames.Status);
        var entityIsContent = !entityIsStatus && ret.Ok && (entity != null);

        if (entityIsContent)
        {
          var target = Navigator.Current.CreateWindow(TargetNames.Blank);
          target.SetContent(ret);
          target.SetBusy(false);
        }

        if (entityIsStatus)
        {
          var target = Navigator.Current.CreateWindow(TargetNames.Blank, parent: Form);
          target.SetContent(ret);
          target.SetBusy(false);
        }

        return ret;
      }
      catch (Exception ex)
      {
        // TODO: O que fazer com essa exceção?
        ex.Trace();
        return ex;
      }
    }
  }
}