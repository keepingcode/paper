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

namespace Sandbox.Bot.Forms
{
  public partial class ActionDialog : Form
  {
    private Entity entity;
    private EntityAction action;

    public ActionDialog(Entity entity, EntityAction action)
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

        IWidget widget;

        switch (field.Type)
        {
          case "hidden":
            {
              widget = new HiddenWidget();
              widget.Control.Click += (o, e) => Submit();
              break;
            }

          case "submit":
            {
              widget = new ButtonWidget();
              widget.Control.Click += (o, e) => Submit();
              break;
            }

          case "checkbox":
            {
              widget = new BitWidget();
              widget.Value = true.Equals(field.Value)
                          || 1.Equals(field.Value)
                          || "1".Equals(field.Value);
              break;
            }

          default:
            {
              widget = new TextWidget();
              widget.Value = field.Value?.ToString();
              ((TextWidget)widget).Placeholder = field.Placeholder;
              break;
            }
        }

        widget.ReadOnly = field.ReadOnly == true;
        widget.Control.TabStop = field.ReadOnly != true;
        widget.Control.Width = 200;
        widget.Control.Tag = field;
        widget.Text = title;
        pnContent.Controls.Add(widget.Control);
      }
    }

    private void Submit()
    {
      MessageBox.Show("Submit");
    }
  }
}