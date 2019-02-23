using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser.Lib
{
  public enum Target
  {
    /// <summary>
    /// Abre o documento lincado em uma nova guia.
    /// </summary>
    Blank,

    /// <summary>
    /// Abre o documento lincado na mesma guia do documento de partida.
    /// Esta é o alvo padrão.
    /// </summary>
    Self,

    // Parent e Top não são suportados porque o PaperBrowser não suporta iFrame
    //
    // /// <summary>
    // /// Abre o documento lincado na guia pai quando um documento é aninhado.
    // /// </summary>
    // Parent,
    // /// <summary>
    // /// Abre o documento lincado na guia inicial dentro de uma cadeia quando um documento é aninhado.
    // /// </summary>
    // Top
  }
}