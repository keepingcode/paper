using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Toolset;

namespace Paper.Browser.Base.Pages
{
  public static class PageFactory
  {
    private static readonly List<string> Classes = new List<string>
    {
      ClassNames.Table,
      ClassNames.Error
    };

    public static IPage CreatePage(Window window, Entity entity)
    {
      try
      {
        var @class = entity.Class?.OrderByDescending(Classes.IndexOf).FirstOrDefault();
        var typeName = $"{typeof(IPage).Namespace}.{@class.ChangeCase(TextCase.PascalCase)}Page";
        var type = Type.GetType(typeName) ?? typeof(ErrorPage);
        var page = (IPage)Activator.CreateInstance(type, window, entity);
        return page;
      }
      catch (Exception ex)
      {
        string href = entity.Links?.FirstOrDefault(x => x.Rel.Has(RelNames.Self))?.Href;
        entity = HttpEntity.Create(href, ex);
        var page = new ErrorPage(window, entity);
        return page;
      }
    }
  }
}
