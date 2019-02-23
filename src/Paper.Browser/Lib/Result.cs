using System;
using System.Collections.Generic;
using System.Text;
using Paper.Media;

namespace Paper.Browser.Lib
{
  public class Result
  {
    public Entity Entity { get; private set; }
    public byte[] Data { get; private set; }

    public static implicit operator Result(Entity value)
    {
      return new Result { Entity = value };
    }

    public static implicit operator Entity(Result value)
    {
      return value.Entity;
    }

    public static implicit operator Result(byte[] value)
    {
      return new Result { Data = value };
    }

    public static implicit operator byte[] (Result value)
    {
      return value.Data;
    }
  }
}