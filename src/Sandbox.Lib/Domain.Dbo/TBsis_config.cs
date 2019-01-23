using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sandbox.Lib.Domain.SmallApi;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;

namespace Sandbox.Lib.Domain.Dbo
{
  public class TBsis_config : Table<TBsis_config, string, TBsis_config.Filter>
  {
    [Pk(AutoIncrement.No)]
    public string DFchave { get; set; }
    public string DFvalor { get; set; }

    public class Filter : SmallApi.Filter
    {
      public Var<string> DFchave { get; set; }
      public Var<string> DFvalor { get; set; }
    }
  }
}