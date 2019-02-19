using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Toolset
{
  public struct Ret<T>
  {
    public bool Ok => (int)Status.Code < 400;

    public T Value { get; set; }

    public Ret.RetStatus Status { get; set; }

    public Ret.RetFault Fault { get; set; }

    public static implicit operator Ret<T>(T value)
    {
      return new Ret<T>
      {
        Value = value,
        Status = new Ret.RetStatus
        {
          Code = (((object)value) != null) ? HttpStatusCode.OK : HttpStatusCode.NotFound
        }
      };
    }

    public static implicit operator T(Ret<T> ret)
    {
      return ret.Value;
    }

    public static implicit operator Ret<T>(Exception exception)
    {
      return new Ret<T>
      {
        Status = new Ret.RetStatus
        {
          Code = HttpStatusCode.InternalServerError
        },
        Fault = new Ret.RetFault
        {
          Message = exception.GetCauseMessage(),
          Exception = exception
        }
      };
    }

    public static implicit operator Ret<T>(Ret ret)
    {
      return new Ret<T>
      {
        Value = (ret.Value == null) ? default(T) : (T)ret.Value,
        Status = ret.Status,
        Fault = ret.Fault
      };
    }

    public static implicit operator Ret(Ret<T> ret)
    {
      return new Ret
      {
        Value = ret.Value,
        Status = ret.Status,
        Fault = ret.Fault
      };
    }
  }
}