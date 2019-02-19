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
    }

    public IPaper Paper { get; }

    public Type PaperType { get; }

    public string PathTemplate { get; }

    public ParameterInfo[] PaperParameters { get; }

    public MethodInfo IndexMethod { get; }

    public ICollection<ParameterInfo> IndexArgs => IndexMethod.GetParameters();

    public ICollection<MethodInfo> Formatters { get; }

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
      foreach (var method in PaperType.GetMethods())
      {
        if (typeof(Format).IsAssignableFrom(method.ReturnType)
         || typeof(ICollection<Format>).IsAssignableFrom(method.ReturnType)
         || typeof(IEnumerable<Format>).IsAssignableFrom(method.ReturnType)
         || typeof(PaperLink).IsAssignableFrom(method.ReturnType)
         || typeof(ICollection<PaperLink>).IsAssignableFrom(method.ReturnType)
         || typeof(IEnumerable<PaperLink>).IsAssignableFrom(method.ReturnType))
        {
          yield return method;
        }
      }
    }
  }
}
