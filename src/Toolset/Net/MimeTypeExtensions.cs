using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolset.Net
{
  public static class MimeTypeExtensions
  {
    public static string GetName(this MimeType mimetype)
    {
      var name = mimetype.ToString();
      var property = typeof(MimeTypeNames).GetProperty(name);
      return property?.GetValue(null)?.ToString();
    }
  }
}