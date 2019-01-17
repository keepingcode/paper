using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Toolset.Xml;

namespace Toolset
{
  public class Ret
  {
    private Type valueType;

    internal object _value;
    internal string _faultMessage;

    internal Ret()
    {
    }

    public bool OK => Status < 400;

    public int Status { get; set; }

    public object Value { get; set; }

    public Exception Fault { get; set; }

    public string FaultMessage
    {
      get
      {
        if (OK)
          return null;

        if (_faultMessage != null)
          return _faultMessage;

        if (Fault is Exception ex)
          return string.Join(Environment.NewLine, ex.GetCauseMessages());

        return Fault?.ToString()
            ?? ((HttpStatusCode)Status).ToString().ChangeCase(TextCase.ProperCase);
      }
      set => _faultMessage = value;
    }

    public override string ToString()
    {
      var message =
        FaultMessage
        ?? ((HttpStatusCode)Status).ToString().ChangeCase(TextCase.ProperCase);

      return $"{{{Status} {message}{(OK ? $": {Value}" : null)}}}";
    }

    public static implicit operator Ret(bool ok)
    {
      return ok ? Ret.Ok() : Ret.Fail();
    }

    public static implicit operator Ret(Exception ex)
    {
      return Ret.Fail(ex);
    }

    #region Métodos Ok()

    public static Ret Ok()
    {
      return new Ret
      {
        Status = (int)HttpStatusCode.OK
      };
    }

    public static Ret Ok(int status)
    {
      return new Ret
      {
        Status = status
      };
    }

    public static Ret Ok(HttpStatusCode status)
    {
      return new Ret
      {
        Status = (int)status
      };
    }

    public static Ret<T> Ok<T>(T value)
    {
      return new Ret<T>
      {
        Status = (int)HttpStatusCode.OK,
        Value = value
      };
    }

    public static Ret<T> Ok<T>(T value, int status)
    {
      return new Ret<T>
      {
        Status = status,
        Value = value
      };
    }

    public static Ret<T> Ok<T>(T value, HttpStatusCode status)
    {
      return new Ret<T>
      {
        Status = (int)status,
        Value = value
      };
    }

    #endregion

    #region Métodos Fail()

    //public static RetFault Throw(Ret ret)
    //{
    //  return new RetFault
    //  {
    //    Status = ret.Status,
    //    Fault = ret.Fault
    //  };
    //}

    public static Ret Fail()
    {
      return new Ret
      {
        Status = (int)HttpStatusCode.InternalServerError
      };
    }

    public static Ret Fail(string faultMessage)
    {
      return new Ret
      {
        Status = (int)HttpStatusCode.InternalServerError,
        FaultMessage = faultMessage
      };
    }

    public static Ret Fail(Exception fault)
    {
      return new Ret
      {
        Status = fault is NotImplementedException
          ? (int)HttpStatusCode.NotImplemented
          : (int)HttpStatusCode.InternalServerError,
        Fault = fault
      };
    }

    public static Ret Fail(int status)
    {
      return new Ret
      {
        Status = status
      };
    }

    public static Ret Fail(int status, string faultMessage)
    {
      return new Ret
      {
        Status = status,
        FaultMessage = faultMessage
      };
    }

    public static Ret Fail(int status, Exception fault)
    {
      fault.Debug();
      return new Ret
      {
        Status = status,
        Fault = fault
      };
    }

    public static Ret Fail(int status, string faultMessage, Exception fault)
    {
      fault.Debug();
      return new Ret
      {
        Status = status,
        FaultMessage = faultMessage,
        Fault = fault
      };
    }

    public static Ret Fail(HttpStatusCode status)
    {
      return new Ret
      {
        Status = (int)status
      };
    }

    public static Ret Fail(HttpStatusCode status, string faultMessage)
    {
      return new Ret
      {
        Status = (int)status,
        FaultMessage = faultMessage
      };
    }

    public static Ret Fail(HttpStatusCode status, Exception fault)
    {
      fault.Debug();
      return new Ret
      {
        Status = (int)status,
        Fault = fault
      };
    }

    public static Ret Fail(HttpStatusCode status, string faultMessage, Exception fault)
    {
      fault.Debug();
      return new Ret
      {
        Status = (int)status,
        FaultMessage = faultMessage,
        Fault = fault
      };
    }

    #endregion

  }
}