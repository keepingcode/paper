using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolset;

namespace Sandbox.Widgets
{
  public static class WidgetFactory
  {
    public static IWidget CreateWidget(string widgetName)
    {
      var ns = typeof(WidgetFactory).Namespace;
      var name = widgetName.ChangeCase(TextCase.PascalCase);
      var typeName = $"{ns}.{name}Widget";
      var type = Type.GetType(typeName) ?? typeof(TextWidget);
      var instance = Activator.CreateInstance(type);
      return (IWidget)instance;
    }
  }
}
