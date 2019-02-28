using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Paper.Browser.Gui.Widgets
{
  public struct Value
  {
    private string _dataType;
    private object _data;

    public Value(object data)
    {
      _data = data;
      _dataType = null;
    }


    public Value(object data, string dataType)
    {
      _data = data;
      _dataType = dataType;
    }

    public string DataType
    {
      get => _dataType ?? DataTypeNames.Text;
      set => _dataType = value;
    }

    public object Data
    {
      get => _data;
      set => _data = value;
    }
  }
}