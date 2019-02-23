using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Lib
{
  public class TargetNames
  {
    /// <summary>
    /// Abre o documento lincado em uma nova guia.
    /// </summary>
    public const string Blank = "_blank";

    /// <summary>
    /// Abre o documento lincado na mesma guia do documento de partida.
    /// Esta é o alvo padrão.
    /// </summary>
    public const string Self = "_self";

    /// <summary>
    /// Abre o documento lincado na guia pai quando um documento é aninhado.
    /// </summary>
    public const string Parent = "_parent";

    /// <summary>
    /// Abre o documento lincado na guia inicial dentro de uma cadeia quando um documento é aninhado.
    /// </summary>
    public const string Top = "_top";
  }
}
