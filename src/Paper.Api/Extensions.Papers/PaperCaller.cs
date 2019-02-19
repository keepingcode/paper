using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Api.Rendering;
using Paper.Media;
using Toolset;
using Toolset.Collections;
using Toolset.Net;
using Toolset.Reflection;
using static Toolset.Ret;

namespace Paper.Api.Extensions.Papers
{
  internal class PaperCaller
  {
    private readonly IObjectFactory objectFactory;

    public PaperCaller(IObjectFactory objectFactory)
    {
      this.objectFactory = objectFactory;
    }

    public async Task<Ret<Result>> CallAsync(PaperContext context)
    {
      var req = context.Request;
      var res = context.Response;
      var path = context.Path;
      var args = context.Args.Values.ToArray();
      var paper = context.Paper;

      Ret<Result> ret = CallPaperMethod(path, paper, req, res, args);
      if (ret.Status.CodeClass != HttpStatusClass.Success)
      {
        return await Task.FromResult(ret);
      }

      Result result = ret.Value;

      var noResult = result.ValueType == typeof(void);
      if (noResult)
      {
        ret = Ret.Create(HttpStatusCode.Found);
        ret.Status.Data[HeaderNames.Location] = req.RequestUri;
        return await Task.FromResult(ret);
      }

      var isUri = typeof(string).IsAssignableFrom(result.ValueType)
               || typeof(Uri).IsAssignableFrom(result.ValueType)
               || typeof(Href).IsAssignableFrom(result.ValueType)
               || typeof(UriString).IsAssignableFrom(result.ValueType);
      if (isUri)
      {
        var href = (result.Value as Href)?.ToString() ?? result.Value?.ToString();
        ret = Ret.Create(HttpStatusCode.Found);
        ret.Status.Data[HeaderNames.Location] = href;
        return await Task.FromResult(ret);
      }

      return await Task.FromResult(ret);
    }

    private Ret<Result> CallPaperMethod(string path, PaperDescriptor paper, Request req, Response res, object[] args)
    {
      object result = null;
      try
      {
        // TODO escolher entre Index e outros metodos
        var method = paper.IndexMethod;

        result = objectFactory.Invoke(paper.Paper, method, args);
      }
      catch (Exception ex)
      {
        result = Ret.Fail(ex);
      }

      var resultType = paper.IndexMethod.ReturnType;
      if (Is.Ret(resultType))
      {
        resultType = TypeOf.Ret(resultType);
      }

      Ret<Result> ret;
      if (result == null)
      {
        ret = Ret.Create(HttpStatusCode.NotFound);
      }
      else if (Is.Ret(result))
      {
        ret = new Ret<Result>();
        ret.Status = (RetStatus)result._Get(nameof(ret.Status));
        ret.Fault = (RetFault)result._Get(nameof(ret.Fault));
        ret.Value = new Result
        {
          Value = result._Get(nameof(ret.Value)),
          ValueType = resultType
        };
      }
      else
      {
        ret = Ret.OK(new Result
        {
          Value = result,
          ValueType = resultType
        });
      }

      return ret;
    }

  }
}