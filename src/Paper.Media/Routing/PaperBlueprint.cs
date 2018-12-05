using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media.Design;

namespace Paper.Media.Routing
{
  /// <summary>
  /// Parâmetros de renderização do Paper.
  /// </summary>
  public class PaperBlueprint
  {
    //public PaperBlueprint(IPaper paper, string uriTemplate)
    public PaperBlueprint(Type paperType, string uriTemplate)
    {
      this.PaperType = paperType;
      this.UriTemplate = UriTemplate;
    }

    /// <summary>
    /// Instância da especificação do Paper.
    /// </summary>
    public Type PaperType { get; }

    /// <summary>
    /// Template de URI para extração dos parâmetros do Paper.
    /// </summary>
    public string UriTemplate { get; }
  }
}