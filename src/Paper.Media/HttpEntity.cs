using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Toolset;

namespace Paper.Media
{
  public static class HttpEntity
  {
    private static IEnumerable<string> EnumerateCauses(string message, Exception exception)
    {
      if (message != null) yield return message;
      while (exception != null)
      {
        yield return exception.Message;
        exception = exception.InnerException;
      }
    }

    public static Ret<Entity> CreateFromRet(UriString route, Ret ret)
    {
      if (ret.Value is Entity entity)
        return Ret.OK(entity);
      else
        return Create(route, ret.Status.Code, ret.Fault.Message, ret.Fault.Exception);
    }

    public static Ret<Entity> Create(UriString route, HttpStatusCode status, string message, Exception exception)
    {
      var causes = EnumerateCauses(message, exception).Distinct();
      var description =
        causes.Any()
          ? string.Join(Environment.NewLine, causes)
          : null;

      var entity = new Entity();

      entity.Class = ClassNames.Status;
      if ((int)status >= 400)
      {
        entity.Class.Add(ClassNames.Error);
      }

      entity.Properties = new PropertyMap();
      entity.Properties.Add("Code", (int)status);
      entity.Properties.Add("Status", status.ToString());

      if (description != null)
        entity.Properties.Add("Description", description);

      if (exception != null)
        entity.Properties.Add("StackTrace", exception.GetStackTrace());

      entity.Links = new LinkCollection();
      entity.Links.Add(new Link
      {
        Href = route.ToString(),
        Rel = RelNames.Self
      });

      return Ret.Create(entity, status);
    }

    public static Ret<Entity> Create(UriString route, HttpStatusCode status, string message)
    {
      return Create(route, status, message, null);
    }

    public static Ret<Entity> Create(UriString route, HttpStatusCode status, Exception exception)
    {
      return Create(route, status, null, exception);
    }

    public static Ret<Entity> Create(UriString route, HttpStatusCode status)
    {
      return Create(route, status, null, null);
    }

    public static Ret<Entity> Create(UriString route, int status, string message, Exception exception)
    {
      return Create(route, (HttpStatusCode)status, message, exception);
    }

    public static Ret<Entity> Create(UriString route, int status, string message)
    {
      return Create(route, (HttpStatusCode)status, message, null);
    }

    public static Ret<Entity> Create(UriString route, int status, Exception exception)
    {
      return Create(route, (HttpStatusCode)status, null, exception);
    }

    public static Ret<Entity> Create(UriString route, int status)
    {
      return Create(route, (HttpStatusCode)status, null, null);
    }

    public static Ret<Entity> Create(UriString route, string message, Exception exception)
    {
      var status =
        exception is NotImplementedException
          ? HttpStatusCode.NotImplemented
          : HttpStatusCode.InternalServerError;
      return Create(route, status, message, exception);
    }

    public static Ret<Entity> Create(UriString route, string message)
    {
      return Create(route, HttpStatusCode.InternalServerError, message, null);
    }

    public static Ret<Entity> Create(UriString route, Exception exception)
    {
      var status =
        exception is NotImplementedException
          ? HttpStatusCode.NotImplemented
          : HttpStatusCode.InternalServerError;
      return Create(route, status, null, exception);
    }

    public static Ret<Entity> Create(UriString route)
    {
      return Create(route, HttpStatusCode.InternalServerError, null, null);
    }
  }
}
