using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Paper.Media.Design
{
  public class HeaderDesign : PropertyDesign
  {
    public const string BagName = "__headers";

    public HeaderDesign(Entity headerEntity)
      : base(headerEntity)
    {
      this.HeaderEntity = headerEntity;
    }

    public Entity HeaderEntity { get; set; }

    public string Name
    {
      get => Get<string>();
    }

    public string Title
    {
      get => Get(HeaderEntity.Title);
      set => Set(HeaderEntity.Title = value);
    }

    public string DataType
    {
      get => Get<string>();
      set => Set(value);
    }

    public bool Hidden
    {
      get => Get(false);
      set
      {
        if (value)
        {
          Set(true);
        }
        else
        {
          Remove();
        }
      }
    }

    public SortOrder? Order
    {
      get => Get<SortOrder?>();
      set => Set(value);
    }
  }
}