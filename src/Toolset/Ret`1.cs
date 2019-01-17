using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Toolset
{
  public class Ret<T>
  {
    private Type valueType;

    private T _value;
    private string _faultMessage;

    internal Ret()
    {
    }

    public bool OK => Status < 400;

    public int Status { get; set; }

    public T Value { get; set; }

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

      var value = OK ? $": {Change.To<string>(Value)}" : null;

      return $"{{{Status} {message}{value}}}";
    }

    public static implicit operator T(Ret<T> ret)
    {
      return ret.Value;
    }

    public static implicit operator Ret<T>(T value)
    {
      return Ret.Ok(value);
    }

    public static implicit operator Ret<T>(Exception ex)
    {
      return Ret.Fail(ex);
    }

    #region Conversões implícitas

    public static implicit operator Ret<T>(Ret other)
    {
      var ret = new Ret<T>();
      ret.Status = other.Status;
      ret.Value = Change.To<T>(other.Value);
      ret.Fault = other.Fault;
      ret._faultMessage = other._faultMessage;
      return ret;
    }

    public static implicit operator Ret(Ret<T> other)
    {
      var ret = new Ret();
      ret.Status = other.Status;
      ret.Value = other.Value;
      ret.Fault = other.Fault;
      ret._faultMessage = other._faultMessage;
      return ret;
    }

    #endregion
  }
}