using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolset.Net
{
  /// <summary>
  /// Coleção dos principais mime types conforme especificados pelo IANA.
  /// 
  /// Referência:
  ///    http://www.iana.org/assignments/media-types/media-types.xhtml
  /// </summary>
  public enum MimeType
  {
    Plain,
    Json,
    Xml,
    Csv,
    OctetStream,
    Siren,
    SirenXml,
    Excel
  }
}