using System;
using System.Collections.Generic;
using System.Text;

namespace Paper.Media.Rendering
{
  public static class FactoryExtensions
  {
    public static T CreateObject<T>(this IFactory factory, params object[] args)
    {
      return (T)factory.CreateObject(typeof(T), args);
    }
  }
}
