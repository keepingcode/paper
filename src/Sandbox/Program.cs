using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sandbox.Lib;
using Sandbox.Lib.Domain.Dbo;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Xml;

namespace Sandbox
{
  class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      try
      {
        {
          var ret = DoOk();
          Debug.WriteLine(ret);
        }

        {
          var ret = DoOk(10.01);
          Debug.WriteLine((Ret<int>)(Ret)ret);
        }

        {
          var ret = DoFail();
          Debug.WriteLine(ret);
        }

        {
          var ret = DoFail(10.01);
          Debug.WriteLine(ret);
        }
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }

    static Ret DoOk()
    {
      return true;
    }

    static Ret<T> DoOk<T>(T value)
    {
      return value;
    }

    static Ret DoFail()
    {
      return new Exception("Failure happens!");
    }

    static Ret<T> DoFail<T>(T value)
    {
      return new Exception("Failure happens!");
    }
  }
}