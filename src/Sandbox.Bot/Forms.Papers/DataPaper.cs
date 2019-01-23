using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sandbox.Bot.Forms.Papers
{
  public partial class DataPaper : UserControl, IPaper
  {
    private Entity entity;

    public DataPaper(Entity entity)
    {
      this.entity = entity;
      InitializeComponent();
      InitializeBehaviour();
    }

    public Control Control => this;

    public Icon Icon { get; set; }

    private void InitializeBehaviour()
    {
      Icon = Resources.FormData.ToIcon();
      Text = NameConventions.MakeTitle(entity.Title, "Formulário");

      var properties =
        from property in entity.Properties
        where !property.Name.StartsWith("_")
           && !(property.Value is PropertyCollection)
        select property;

      foreach (Property property in properties)
      {
        var widget = new InfoWidget();
        widget.Text = property.Name.ChangeCase(TextCase.ProperCase);
        widget.Value = property.Value?.ToString();
        widget.Width = 200;
        pnContent.Controls.Add(widget);
      }
    }
  }
}
