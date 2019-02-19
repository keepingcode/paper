using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Paper.Api.Rendering
{
  public static class ObjectFactoryExtensions
  {
    public static T CreateObject<T>(this IObjectFactory factory, params object[] extraArgs)
    {
      return (T)factory.CreateObject(typeof(T), extraArgs);
    }

    public static T GetInstance<T>(this IObjectFactory factory)
    {
      return (T)factory.GetInstance(typeof(T));
    }

    public static object Invoke(this IObjectFactory factory, Delegate @delegate, params object[] extraArgs)
    {
      var parameters = @delegate.Method.GetParameters();
      var values = new object[parameters.Length];
      for (int i = 0; i < parameters.Length; i++)
      {
        var parameter = parameters[i];
        var type = parameter.ParameterType;
        values[i] = factory.GetInstance(type) ?? SelectArgument(extraArgs, type);
      }
      var result = @delegate.DynamicInvoke(values);
      return result;
    }

    public static object Invoke(this IObjectFactory factory, object host, MethodInfo method, params object[] extraArgs)
    {
      var parameters = method.GetParameters();
      var values = new object[parameters.Length];
      for (int i = 0; i < parameters.Length; i++)
      {
        var parameter = parameters[i];
        var type = parameter.ParameterType;
        values[i] = factory.GetInstance(type) ?? SelectArgument(extraArgs, type);
      }
      var result = method.Invoke(host, values);
      return result;
    }

    private static object SelectArgument(object[] args, Type expectedType)
    {
      var arg = args.FirstOrDefault(x => x?.GetType() == expectedType)
             ?? args.FirstOrDefault(x => expectedType.IsAssignableFrom(x?.GetType()));
      return arg;
    }
  }
}
