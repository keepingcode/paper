using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Paper.Api.Commons;
using Paper.Api.Rendering;
using Paper.Media;
using Paper.Media.Data;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;
using Toolset.Net;
using Toolset.Reflection;
using static Toolset.Ret;

namespace Paper.Api.Extensions.Papers
{
  [Expose]
  public class PaperPipeline : IPipeline
  {
    private readonly IPaperCatalog paperCatalog;
    private readonly IObjectFactory objectFactory;

    public string Route { get; } = "";

    public PaperPipeline(IObjectFactory objectFactory, IPaperCatalog paperCatalog)
    {
      this.objectFactory = objectFactory;
      this.paperCatalog = paperCatalog;
    }

    public async Task RenderAsync(Request req, Response res, NextAsync next)
    {
      var path = req.Path.Substring(Route.Length);
      var paper = paperCatalog.FindExact(path).FirstOrDefault();
      if (paper == null)
      {
        await next.Invoke();
        return;
      }

      var context = new PaperContext();
      context.Paper = paper;
      context.Path = path;
      context.Args = CreateArgs(path, paper, req, res);
      context.Request = req;
      context.Response = res;

      var caller = objectFactory.CreateObject<PaperCaller>();
      var renderer = objectFactory.CreateObject<PaperRenderer>();

      Ret<Result> result = await caller.CallAsync(context);
      if (result.Status.CodeClass != HttpStatusClass.Success)
      {
        var entity = HttpEntity.CreateFromRet(req.RequestUri, result);
        await SendAsync(res, result, entity);
        return;
      }
      
      Ret<Entity> media = await renderer.RenderAsync(context, result.Value);
      if (!media.Ok)
      {
        var entity = HttpEntity.CreateFromRet(req.RequestUri, result);
        await SendAsync(res, media, entity);
        return;
      }

      await SendAsync(res, media, media.Value);
    }

    private async Task SendAsync(Response res, Ret ret, Entity entity)
    {
      res.Status = ret.Status.Code;
      foreach (var entry in ret.Status.Headers)
      {
        res.Headers[entry.Key] = entry.Value;
      }
      await res.WriteEntityAsync(entity);
    }

    private HashMap CreateArgs(string path, PaperDescriptor paper, Request req, Response res)
    {
      var map = new HashMap();

      var pathArgs = new PathArgs(path, paper.PathTemplate);
      foreach (var parameter in paper.PaperParameters)
      {
        object value;
        var type = parameter.ParameterType;
        if (typeof(Sort).IsAssignableFrom(type))
        {
          value = null;
        }
        else if (typeof(Page).IsAssignableFrom(type))
        {
          var page = new Page();
          page.CopyFrom(req.RequestUri);
          value = page;
        }
        else if (typeof(IFilter).IsAssignableFrom(type))
        {
          value = null;
        }
        else
        {
          object rawValue = pathArgs[parameter.Name];
          if (rawValue is Var var)
          {
            rawValue = var.RawValue;
          }
          value = Change.To(rawValue, type);
        }
        map[parameter.Name] = value;
      }

      return map;
    }
  }
}