using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Sandbox.Lib
{
  public static class NameConventions
  {
    public static string MakeTitle(string title, string name)
    {
      return !string.IsNullOrWhiteSpace(title) ? title : name.ChangeCase(TextCase.ProperCase);
    }
  }
}
