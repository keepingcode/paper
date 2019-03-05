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

    public override string ToString()
    {
      var fault = Fault.ToString();
      return (fault.Length > 0) ? $"{Status} - {fault}" : Status.ToString();
    }

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
      private HashMap<string> _headers;

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

      public HashMap<string> Headers
      {
        get => _headers ?? (_headers = new HashMap<string>());
        set => _headers = value;
      }

      public override string ToString()
      {
        return $"{(int)Code} - {Code}";
      }
    }

    public struct RetFault
    {
      public string Message { get; set; }

      public Exception Exception { get; set; }

      public override string ToString()
      {
        return Message ?? Exception?.Message;
      }
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

    public static Ret NotFound()
    {
      return new Ret
      {
        Status = new RetStatus
        {
          Code = HttpStatusCode.NotFound
        }
      };
    }

    public static Ret<T> NotFound<T>(T value)
    {
      return new Ret<T>
      {
        Value = value,
        Status = new RetStatus
        {
          Code = HttpStatusCode.NotFound
        }
      };
    }

    public static Ret<T> Create<T>(HttpStatusCode status, T value)
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

    public static Ret Fail(HttpStatusCode status, string faultMessage)
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

    public static Ret Fail(HttpStatusCode status, Exception exception)
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

    public static Ret Fail(HttpStatusCode status, string faultMessage, Exception exception)
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