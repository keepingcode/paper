using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  public interface IFactory
  {
    object CreateObject(Type type, params object[] args);
  }
}
