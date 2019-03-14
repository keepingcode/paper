using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paper.Media;
using Toolset;

namespace Paper.Browser.Gui.Widgets
{
  public static class WidgetExtensions
  {
    private static Font FlatFont;
    private static Font BoldFont;
    private static Color FlatColor;
    private static Color BoldColor;

    public static void EnhanceFieldWidget(this IFieldWidget widget)
    {
      widget.FieldChanged += (o, e) => UpdateLayout(widget);
      widget.ValueChanged += (o, e) => UpdateLayout(widget);
    }

    private static void UpdateLayout(IFieldWidget widget)
    {
      var field = widget.Field;
      if (field == null)
        return;

      // inicializacao
      //

      if (FlatFont == null)
      {
        FlatFont = widget.Host.Font;
        FlatColor = widget.Host.ForeColor;
        BoldFont = new Font(FlatFont, FontStyle.Bold);
        BoldColor = Color.Firebrick;
      }

      var tipBox = widget.Components.Components.OfType<ToolTip>().FirstOrDefault();
      if (tipBox == null)
      {
        tipBox = new ToolTip(widget.Components);
      }

      // checagem
      //

      string text;
      string tip;
      Font font;
      Color color;

      text = field.Title ?? field.Name.ChangeCase(TextCase.ProperCase);
      if (field.Required == true)
      {
        text += "*";
      }

      font = widget.HasChanges ? BoldFont : FlatFont;

      var errors = widget.GetErrors().ToArray();
      if (errors.Length > 0)
      {
        tip = string.Join(Environment.NewLine, errors.Select(x => $"• {x}"));
        color = BoldColor;
      }
      else
      {
        tip = "";
        color = FlatColor;
      }

      // aplicacao das mudancas
      //

      if (tipBox.GetToolTip(widget.Host) != tip)
      {
        tipBox.SetToolTip(widget.Host, tip);
      }
      if (widget.Label.Text != text)
      {
        widget.Label.Text = text;
      }
      if (widget.Label.Font != font)
      {
        widget.Label.Font = font;
      }
      if (widget.Label.ForeColor != color)
      {
        widget.Label.ForeColor = color;
      }
    }
  }
}
