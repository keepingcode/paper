using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;

namespace Sandbox.Bot.Api
{
  public class Result
  {
    private Entity _entity;
    private Uri _uri;

    public Entity Entity
    {
      get => _entity;
      set
      {
        _entity = value;
        _uri = null;
      }
    }

    public Uri Uri
    {
      get => _uri;
      set
      {
        _uri = value;
        _entity = null;
      }
    }

    public static implicit operator Entity(Result result)
    {
      return result.Entity;
    }

    public static implicit operator Result(Entity entity)
    {
      return new Result { Entity = entity };
    }

    public static implicit operator Uri(Result result)
    {
      return result.Uri;
    }

    public static implicit operator Result(Uri uri)
    {
      return new Result { Uri = uri };
    }
  }
}
