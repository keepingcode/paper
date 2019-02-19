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

namespace Paper.Api.Extensions.Papers
{
  public class PaperDescriptor
  {
    public PaperDescriptor(IPaper paper)
    {
      this.Paper = paper;
      this.PaperType = paper.GetType();
      this.IndexMethod = this.PaperType._GetMethodInfo("Index");
      this.PathArgs = MakePathArgs();
      this.PathTemplate = MakePath();
      this.Formatters = MakeFormatters().ToArray();
    }

    public IPaper Paper { get; }

    public Type PaperType { get; }

    public string PathTemplate { get; }

    public ParameterInfo[] PathArgs { get; }

    public MethodInfo IndexMethod { get; }

    public ICollection<ParameterInfo> IndexArgs => IndexMethod.GetParameters();

    public ICollection<MethodInfo> Formatters { get; }

    private ParameterInfo[] MakePathArgs()
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
      var name = Regex.Replace(PaperType.Name, "(Paper|Action)s?$", "", RegexOptions.IgnoreCase);
      var args = PathArgs.Select(x => $"{{{x.Name}}}").ToArray();

      var namePath = $"/{name.Replace(".", "/")}";
      var argsPath = (args.Length > 0) ? $"/{string.Join("/", args)}" : null;

      var path = $"{namePath}{argsPath}";
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
