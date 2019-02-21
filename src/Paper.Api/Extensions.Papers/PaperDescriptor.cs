using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Paper.Media;
using Toolset.Reflection;
using Paper.Media.Data;
using Toolset;
using Paper.Media.Design;
using Toolset.Collections;

namespace Paper.Api.Extensions.Papers
{
  public class PaperDescriptor
  {
    public PaperDescriptor(IPaper paper)
    {
      this.Paper = paper;
      this.PaperType = paper.GetType();
      this.IndexMethod = this.PaperType._GetMethodInfo("Index");
      this.PaperParameters = MakePaperParameters();
      this.PathTemplate = MakePath();
      this.Formatters = MakeFormatters().ToArray();
      this.Actions = MakeActions().ToArray();
    }

    public IPaper Paper { get; }

    public Type PaperType { get; }

    public string PathTemplate { get; }

    public ParameterInfo[] PaperParameters { get; }

    public MethodInfo IndexMethod { get; }

    public ICollection<ParameterInfo> IndexArgs => IndexMethod.GetParameters();

    public ICollection<MethodInfo> Formatters { get; }

    public ICollection<MethodInfo> Actions { get; }

    private ParameterInfo[] MakePaperParameters()
    {
      return (
         from param in IndexMethod.GetParameters()
         where !typeof(Sort).IsAssignableFrom(param.ParameterType)
            && !typeof(Page).IsAssignableFrom(param.ParameterType)
            && !typeof(IFilter).IsAssignableFrom(param.ParameterType)
         select param
      ).ToArray();
    }

    public string MakePath()
    {
      var namePath = Conventions.MakeName(PaperType).Replace(".", "/");
      var argsPath = string.Join("/", PaperParameters.Select(x => $"{{{x.Name}}}"));
      var path = $"/{string.Join("/", namePath, argsPath)}";
      return path;
    }

    private IEnumerable<MethodInfo> MakeFormatters()
    {
      var methods = PaperType.GetMethods(BindingFlags.Public | BindingFlags.Instance).Except(IndexMethod);
      foreach (var method in methods)
      {
        if (typeof(IFormatter).IsAssignableFrom(method.ReturnType)
         || typeof(ICollection<IFormatter>).IsAssignableFrom(method.ReturnType)
         || typeof(IEnumerable<IFormatter>).IsAssignableFrom(method.ReturnType)
         
         || typeof(Link).IsAssignableFrom(method.ReturnType)
         || typeof(ICollection<Link>).IsAssignableFrom(method.ReturnType)
         || typeof(IEnumerable<Link>).IsAssignableFrom(method.ReturnType)
         
         || typeof(Format).IsAssignableFrom(method.ReturnType)
         || typeof(ICollection<Format>).IsAssignableFrom(method.ReturnType)
         || typeof(IEnumerable<Format>).IsAssignableFrom(method.ReturnType))
        {
          yield return method;
        }
      }
    }

    private IEnumerable<MethodInfo> MakeActions()
    {
      var knownMethods = IndexMethod.AsSingle().Concat(Formatters);
      var methods = PaperType.GetMethods(BindingFlags.Public | BindingFlags.Instance).Except(knownMethods);
      foreach (var method in methods)
      {
        if (method.DeclaringType.Namespace.StartsWith("System"))
          continue;

        if (typeof(void).IsAssignableFrom(method.ReturnType)
         || typeof(Ret).IsAssignableFrom(method.ReturnType)
         
         || typeof(string).IsAssignableFrom(method.ReturnType)
         || typeof(Href).IsAssignableFrom(method.ReturnType)
         || typeof(Uri).IsAssignableFrom(method.ReturnType)
         || typeof(UriString).IsAssignableFrom(method.ReturnType)

         || typeof(Ret<string>).IsAssignableFrom(method.ReturnType)
         || typeof(Ret<Href>).IsAssignableFrom(method.ReturnType)
         || typeof(Ret<Uri>).IsAssignableFrom(method.ReturnType)
         || typeof(Ret<UriString>).IsAssignableFrom(method.ReturnType))
        {
          yield return method;
        }
      }
    }
  }
}
