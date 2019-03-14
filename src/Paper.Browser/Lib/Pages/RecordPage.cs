using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Browser.Gui;
using Paper.Browser.Gui.Widgets;
using Paper.Media;
using Paper.Media.Design;

namespace Paper.Browser.Lib.Pages
{
  public class RecordPage : IPage
  {
    private Entity _entity;

    public RecordPage()
    {
      this.Host = new WidgetGrid();
    }

    public Control Host { get; }

    public Window Window { get; set; }

    public Entity Entity
    {
      get => _entity;
      set => DisplayEntity(_entity = value);
    }

    private void DisplayEntity(Entity entity)
    {
      Host.Controls.Clear();
      foreach (var header in entity.Headers(ClassNames.Record))
      {
        var widget = new TextWidget();
        widget.Header = header;
        widget.Value = entity.Properties?[header.Name];
        Host.Controls.Add(widget);
      }
    }
  }
}