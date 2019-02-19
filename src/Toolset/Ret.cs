using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Toolset.Collections;
using Toolset.Net;
using Toolset.Xml;

namespace Toolset
{
  public struct Ret
  {
    public bool Ok => (int)Status.Code < 400;

    public object Value { get; set; }

    public RetStatus Status { get; set; }

    public RetFault Fault { get; set; }

    public static implicit operator Ret(bool value)
    {
      return new Ret
      {
        Status = new RetStatus
        {
          Code = value ? HttpStatusCode.OK : HttpStatusCode.NotFound
        }
      };
    }

    public static implicit operator Ret(Exception exception)
    {
      return new Ret

      {
        Status = new RetStatus
        {
          Code = HttpStatusCode.InternalServerError
        },
        Fault = new RetFault
        {
          Message = exception.GetCauseMessage(),
          Exception = exception
        }
      };
    }

    #region Estruturas

    public struct RetStatus
    {
      private HashMap<string> _data;

      public HttpStatusCode Code { get; set; }

      public int CodeValue
      {
        get => (int)Code;
        set => Code = (HttpStatusCode)value;
      }

      public HttpStatusClass CodeClass
      {
        get => (HttpStatusClass)(CodeValue / 100);
      }

      public HashMap<string> Data
      {
        get => _data ?? (_data = new HashMap<string>());
        set => _data = value;
      }
    }

    public struct RetFault
    {
      public string Message { get; set; }

      public Exception Exception { get; set; }
    }

    #endregion

    #region Extensões

    public static Ret OK()
    {
      return new Ret
      {
        Status = new RetStatus
        {
          Code = HttpStatusCode.OK
        }
      };
    }

    public static Ret<T> OK<T>(T value)
    {
      return new Ret<T>
      {
        Value = value,
        Status = new RetStatus
        {
          Code = HttpStatusCode.OK
        }
      };
    }

    public static Ret<T> Create<T>(T value, HttpStatusCode status)
    {
      return new Ret<T>
      {
        Value = value,
        Status = new RetStatus
        {
          Code = status
        }
      };
    }

    public static Ret Create(HttpStatusCode status)
    {
      return new Ret
      {
        Status = new RetStatus
        {
          Code = status
        }
      };
    }

    public static Ret Create(HttpStatusCode status, string faultMessage)
    {
      return new Ret
      {
        Status = new RetStatus
        {
          Code = status
        },
        Fault = new RetFault
        {
          Message = faultMessage
        }
      };
    }

    public static Ret Create(HttpStatusCode status, Exception exception)
    {
      return new Ret
      {
        Status = new RetStatus
        {
          Code = status
        },
        Fault = new RetFault
        {
          Message = exception?.GetCauseMessage(),
          Exception = exception
        }
      };
    }

    public static Ret Create(HttpStatusCode status, string faultMessage, Exception exception)
    {
      return new Ret
      {
        Status = new RetStatus
        {
          Code = status
        },
        Fault = new RetFault
        {
          Message = faultMessage ?? exception.GetCauseMessage(),
          Exception = exception
        }
      };
    }

    public static Ret Fail(string faultMessage)
    {
      return Fail(faultMessage, null);
    }

    public static Ret Fail(Exception exception)
    {
      return Fail(null, exception);
    }

    public static Ret Fail(string faultMessage, Exception exception)
    {
      return new Ret
      {
        Status = new RetStatus
        {
          Code = HttpStatusCode.InternalServerError
        },
        Fault = new RetFault
        {
          Message = faultMessage ?? exception?.GetCauseMessage(),
          Exception = exception
        }
      };
    }

    #endregion
  }
}