﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Widgets;
using Paper.Media;
using Paper.Media.Design;
using Toolset;

namespace Paper.Browser.Lib
{
  public class WindowAction
  {
    private bool complete;

    public WindowAction(Window window, Entity entity, EntityAction entityAction, ICollection<Entity> selection = null)
    {
      this.Window = window;
      this.Entity = entity;
      this.EntityAction = entityAction;
      this.Selection = selection;
      this.Host = CreateForm(entity, entityAction);
    }

    public Window Window { get; }

    public Entity Entity { get; }

    public EntityAction EntityAction { get; }

    public ICollection<Entity> Selection { get; }

    public Form Host { get; }

    public async Task<Ret> SubmitAsync()
    {
      var form = (ActionForm)Host;
      try
      {
        form.SubmitButton.Enabled = form.ExitButton.Enabled = false;

        var hasErrors = CheckForErrors();
        if (hasErrors)
        {
          MessageBox.Show(
            form,
            "Não é possível prosseguir porque existem erros no formulário.",
            form.Text,
            MessageBoxButtons.OK
          );
          return Ret.Fail("Não é possível prosseguir porque existem erros no formulário.");
        }

        var ret = await SendFormAsync();
        if (ret.Ok)
        {
          complete = true;
          Host.Close();
        }
        return ret;
      }
      catch (Exception ex)
      {
        ex.Trace();
        return ex;
      }
      finally
      {
        form.SubmitButton.Enabled = form.ExitButton.Enabled = true;
      }
    }

    private async Task<Ret> SendFormAsync()
    {
      try
      {
        var action = this.EntityAction;

        var form = (ActionForm)this.Host;
        var widgets = form.WidgetGrid.Controls.OfType<IFieldWidget>();

        var payload = new Payload();
        foreach (var widget in widgets)
        {
          var field = widget.Field;
          var value = widget.Value;
          payload.SetProperty(field.Name, value);
        }

        var pack = payload.ToEntity();

        var ret = await Window.RequestAsync(action.Href, action.Method, SelectTarget, pack);

        return ret;
      }
      catch (Exception ex)
      {
        // TODO: O que fazer com essa exceção?
        ex.Trace();
        return ex;
      }
    }

    private string SelectTarget(Entity entity)
    {
      var otherHref = entity.GetSelfHref();
      if (Window.Href != null && otherHref != null)
      {
        var sourceHref = Window.Href.Split('?').First();
        var targetHref = otherHref.Value.Split('?').First();
        if (sourceHref == targetHref)
        {
          return TargetNames.Self;
        }
      }
      return TargetNames.Blank;
    }

    public async Task<Ret> CancelAsync()
    {
      var form = (ActionForm)Host;
      try
      {
        form.SubmitButton.Enabled = form.ExitButton.Enabled = false;

        var hasChanges = CheckForChanges();
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

        return true;
      }
      catch (Exception ex)
      {
        return ex;
      }
      finally
      {
        form.SubmitButton.Enabled = form.ExitButton.Enabled = true;
      }
    }

    private bool CheckForErrors()
    {
      var form = (ActionForm)this.Host;
      var widgets = form.WidgetGrid.Controls.OfType<IFieldWidget>();

      var hasErrors = widgets.SelectMany(x => x.GetErrors()).Any();
      return hasErrors;
    }

    private bool CheckForChanges()
    {
      if (complete)
        return false;

      var form = (ActionForm)this.Host;
      var widgets = form.WidgetGrid.Controls.OfType<IFieldWidget>();

      var hasChanges = widgets.Any(x => x.HasChanges);
      return hasChanges;
    }

    private ActionForm CreateForm(Entity entity, EntityAction action)
    {
      var form = new ActionForm();

      form.Text = action.Title ?? action.Name?.ChangeCase(TextCase.PascalCase) ?? "Edição";
      form.SubmitButton.Text = action.Title ?? "OK";
      form.SubmitButton.Click += async (o, e) => await SubmitAsync();
      form.ExitButton.Click += async (o, e) => await CancelAsync();
      form.FormClosing += async (o, e) => e.Cancel = !(complete || await CancelAsync());

      var hasSelection = (Selection?.Any() == true);

      foreach (var field in action.Fields)
      {
        IFieldWidget widget;

        var hasSelfProvider = (field.Provider?.Rel.Has(RelNames.Self) == true);
        if (hasSelfProvider && hasSelection)
        {
          var selectedItems = Selection.Select(x => x.Properties);

          widget = new SelectRecordFieldWidget { ReadOnly = true };
          widget.Window = Window;
          widget.Entity = entity;
          widget.Field = field;
          widget.Value = selectedItems;
        }
        else
        {
          var fieldType = (field.Type ?? FieldTypeNames.Text).Replace("__", "");
          var prefix = fieldType.ChangeCase(TextCase.PascalCase);
          var typeName = $"Paper.Browser.Gui.Widgets.{prefix}FieldWidget";
          var type = Type.GetType(typeName) ?? typeof(TextFieldWidget);
          widget = (IFieldWidget)Activator.CreateInstance(type);
          widget.Window = Window;
          widget.Entity = entity;
          widget.Field = field;
        }

        form.WidgetGrid.Controls.Add(widget.Host);
      }

      return form;
    }
  }
}