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
          button.Click += (o, e) => Submit();
        }
        if (field.ReadOnly == true)
        {
          widget.Control.TabStop = false;
        }

        pnContent.Controls.Add(widget.Control);
      }
    }

    private void Submit()
    {
      MessageBox.Show("Submit");
    }
  }
}