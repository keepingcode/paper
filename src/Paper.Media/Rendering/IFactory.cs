using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Rendering
{
  public interface IFactory
  {
    object CreateObject(Type type, params object[] args);
  }
}
