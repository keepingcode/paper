using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Media;
using Toolset;

namespace Paper.Browser.Lib
{
  public class Action
  {
    private bool complete;

    public Action(Window window, Entity entity, EntityAction entityAction, ICollection<Entity> selection = null)
    {
      this.Window = window;
      this.Entity = entity;
      this.EntityAction = entityAction;
      this.Selection = selection;

      var form = new ActionForm();
      form.Text = entityAction.Title ?? entityAction.Name?.ChangeCase(TextCase.PascalCase) ?? "Edição";

      form.SubmitButton.Text = entityAction.Title ?? "OK";
      form.SubmitButton.Click += async (o, e) => await SubmitAsync();
      form.ExitButton.Click += async (o, e) => await CancelAsync();
      form.FormClosing += async (o, e) => e.Cancel = !(complete || await CancelAsync());

      this.Host = form;
    }

    public Window Window { get; }

    public Entity Entity { get; }

    public EntityAction EntityAction { get; }

    public ICollection<Entity> Selection { get; }

    public Form Host { get; }

    public async Task<bool> SubmitAsync()
    {
      var form = (ActionForm)Host;
      try
      {
        form.SubmitButton.Enabled = form.ExitButton.Enabled = false;

        await Task.Delay(2000);

        complete = true;
        Host.Close();

        return await Task.FromResult(true);
      }
      finally
      {
        form.SubmitButton.Enabled = form.ExitButton.Enabled = true;
      }
    }

    public async Task<bool> CancelAsync()
    {
      var form = (ActionForm)Host;
      try
      {
        form.SubmitButton.Enabled = form.ExitButton.Enabled = false;

        var hasChanges = await CheckForChangesAsync();
        if (hasChanges)
        {
          var ln = Environment.NewLine;
          var result = MessageBox.Show(
            this.Host,
            $"Existem alterações pendentes.{ln}{ln}Deseja realmente sair e perder estas alterações?",
            this.Host.Text,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2
          );
          if (result == DialogResult.No)
          {
            return await Task.FromResult(false);
          }
        }

        complete = true;
        Host.Close();

        return await Task.FromResult(true);
      }
      finally
      {
        form.SubmitButton.Enabled = form.ExitButton.Enabled = true;
      }
    }

    private async Task<bool> CheckForChangesAsync()
    {
      return await Task.FromResult(!complete);
    }

    private Ret Validate()
    {
      return Ret.OK();
      //  try
      //  {
      //    bool isValid = true;
      //    foreach (var widget in Form.Widgets())
      //    {
      //      if (!widget.ValidateContent())
      //      {
      //        isValid = false;
      //      }
      //    }
      //    return isValid;
      //  }
      //  catch (Exception ex)
      //  {
      //    // TODO: O que fazer com essa exceção?
      //    ex.Trace();
      //    return ex;
      //  }
    }

    private async Task<Ret> SendFormAsync()
    {
      return await Task.FromResult(Ret.OK());
      //  try
      //  {
      //    var action = this.EntityAction;

      //    var payload = new Payload();
      //    foreach (var widget in Form.Widgets())
      //    {
      //      var field = widget.Field;
      //      var value = widget.Content;
      //      payload.SetProperty(field.Name, value);
      //    }

      //    var ret = await Navigator.Current.RequestAsync(action.Href, action.Method, payload);

      //    var entity = ret.Value?.Data as Entity;
      //    var entityIsStatus = (entity != null) && entity.Class.Has(ClassNames.Status);
      //    var entityIsContent = !entityIsStatus && ret.Ok && (entity != null);

      //    if (entityIsContent)
      //    {
      //      var target = Navigator.Current.CreateWindow(TargetNames.Blank);
      //      target.SetContent(ret);
      //      target.SetBusy(false);
      //    }

      //    if (entityIsStatus)
      //    {
      //      var target = Navigator.Current.CreateWindow(TargetNames.Blank, parent: Form);
      //      target.SetContent(ret);
      //      target.SetBusy(false);
      //    }

      //    return ret;
      //  }
      //  catch (Exception ex)
      //  {
      //    // TODO: O que fazer com essa exceção?
      //    ex.Trace();
      //    return ex;
      //  }
    }
  }
}