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
using Paper.Media.Design;
using Sandbox.Bot.Api;
using Sandbox.Bot.Properties;
using Sandbox.Lib;
using Sandbox.Widgets;
using Toolset;
using Toolset.Reflection;

namespace Sandbox.Bot.Forms
{
  public partial class ActionForm : Form
  {
    private Entity entity;
    private EntityAction action;

    public ActionForm(Entity entity, EntityAction action)
    {
      this.entity = entity;
      this.action = action;
      InitializeComponent();
      InitializeBehaviour();
    }

    private void InitializeBehaviour()
    {
      var entityTitle = NameConventions.MakeTitle(entity.Title, "Formulário");
      var actionTitle = NameConventions.MakeTitle(action.Title, action.Name);

      Icon = Resources.FormEdit.ToIcon();
      Text = $"{entityTitle} - {actionTitle}";

      foreach (Field field in action.Fields)
      {
        var title = NameConventions.MakeTitle(field.Title, field.Name);
        var value = field.Value;

        var widget = WidgetFactory.CreateWidget(field.Type);

        widget._CopyFrom(field);

        widget.Title = title;
        widget.Control.Tag = field;
        widget.Control.Width = 200;

        if (widget is SubmitWidget button)
        {
          button.Click += async (o, e) =>
          {
            pnContent.Enabled = false;
            try
            {
              await SubmitAsync();
            }
            finally
            {
              pnContent.Enabled = true;
            }
          };
        }
        if (field.ReadOnly == true)
        {
          widget.Control.TabStop = false;
        }

        pnContent.Controls.Add(widget.Control);
      }
    }

    private async Task SubmitAsync()
    {
      try
      {
        var formData = new Entity();
        formData.AddClass(Class.FormData);

        foreach (var widget in pnContent.Controls.OfType<IWidget>())
        {
          if (widget.Value != null)
          {
            var name = widget.Name;
            var text = Change.To<string>(widget.Value);
            formData.AddProperty(name, text);
          }
        }

        var route = action.Href;
        var method = action.Method ?? MethodNames.Post;

        await MediaClient.Current.TransferAsync(route, method, formData);
      }
      catch (Exception ex)
      {
        ex.Trace();
        MessageBox.Show(this, ex.Message, "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}