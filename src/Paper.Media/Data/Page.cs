using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media.Data
{
  public class Page
  {
    private int _offset;
    private int _limit;

    public Page()
    {
      this.Offset = 0;
      this.Limit = 50;
    }

    public Page(int offset, int limit)
    {
      this.Offset = offset;
      this.Limit = limit;
    }

    public int Offset
    {
      get => _offset;
      set => _offset = (value > 0) ? value : 0;
    }

    public int Limit
    {
      get => _limit;
      set => _limit = (value > 0) ? value : 50;
    }

    public void IncreaseLimit(int amount = 1)
    {
      Limit += amount;
    }

    public void DecreaseLimit(int amount = 1)
    {
      var newLimit= Limit -= amount;
      Limit = newLimit > 0 ? newLimit : 1;
    }

    public Page Clone()
    {
      return new Page { Offset = this.Offset, Limit = this.Limit };
    }

    public Page GetFirstPage()
    {
      var clone = this.Clone();
      clone.Offset = 0;
      return clone;
    }

    public Page GetNextPage()
    {
      var newOffset = this.Offset + this.Limit;

      var clone = this.Clone();
      clone.Offset = newOffset;
      return clone;
    }

    public Page GetPreviousPage()
    {
      var newOffset = this.Offset - this.Limit;
      if (newOffset < 0)
      {
        newOffset = 0;
      }

      var clone = this.Clone();
      clone.Offset = newOffset;
      return clone;
    }

    public void CopyFrom(UriString uri)
    {
      if (uri == null)
        return;

      var offset = Change.To<int?>(uri.GetArg("offset"));
      if (offset != null)
      {
        Offset = offset.Value;
      }

      var limit = Change.To<int?>(uri.GetArg("limit"));
      if (limit != null)
      {
        Limit = limit.Value;
      }
    }

    public void CopyFrom(IDictionary args)
    {
      if (args == null)
        return;

      var offset = Change.To<int?>(args["offset"]);
      if (offset != null)
      {
        Offset = offset.Value;
      }

      var limit = Change.To<int?>(args["limit"]);
      if (limit != null)
      {
        Limit = limit.Value;
      }
    }

    public UriString CreateUri(UriString baseUri)
    {
      return baseUri.SetArg("offset", Offset).SetArg("limit", Limit);
    }

    public void CopyTo(HashMap args)
    {
      args["offset"] = Offset;
      args["limit"] = Limit;
    }

    public override string ToString()
    {
      return $"offset={Offset}&limit={Limit}";
    }
  }
}