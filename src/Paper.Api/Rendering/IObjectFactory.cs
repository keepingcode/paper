using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Api.Rendering
{
  public interface IObjectFactory
  {
    object CreateObject(Type type, params object[] args);
  }
}
