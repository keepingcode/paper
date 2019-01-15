using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Sandbox.Widgets;
using Toolset;

namespace Sandbox.Bot.Forms.Papers
{
  public partial class SinglePaper : UserControl
  {
    private Entity entity;

    public SinglePaper(Entity entity)
    {
      this.entity = entity;
      InitializeComponent();
      LoadEntity();
    }
    
    private void LoadEntity()
    {
      Text = entity.Title;

      int i = 0;
      foreach (Property property in entity.Properties)
      {
        if (property.Value is PropertyCollection)
          continue;

        var widget = new InfoWidget();
        widget.Text = property.Name.ChangeCase(TextCase.ProperCase);
        widget.Value = property.Value?.ToString();
        widget.Width = 200;

        widget.Text = $"{++i} - {widget.Text}";

        pnContent.Controls.Add(widget);
      }
    }
  }
}
