using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paper.Browser
{
  static class Settings
  {
    public static string Prefix = "/Api/1";

    public static string Host = "localhost";

    public static int Port = 8080;

    public static string Endpoint => $"http://{Host}:{Port}/";
  }
}
