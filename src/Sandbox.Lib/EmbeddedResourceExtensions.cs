using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.IO;

namespace Sandbox.Lib
{
  public static class EmbeddedResourceExtensions
  {
    public static bool HasResource(this Type type, string resourceName)
    {
      return GetResourceManifestName(type.Assembly, resourceName) != null;
    }

    public static string GetResourceText(this Type type, string resourceName)
    {
      return GetResourceText(type.Assembly, resourceName);
    }

    public static string GetResourceText(this Assembly assembly, string resourceName)
    {
      var manifest = GetResourceManifestName(assembly, resourceName);
      if (manifest == null)
        return null;

      using (var stream = assembly.GetManifestResourceStream(manifest))
      {
        var text = new StreamReader(stream).ReadToEnd();
        return text;
      }
    }

    private static string GetResourceManifestName(this Assembly assembly, string resourceName)
    {
      return (
        from manifest in assembly.GetManifestResourceNames()
        where manifest.EndsWith(resourceName)
        select manifest
      ).FirstOrDefault();
    }
  }
}
