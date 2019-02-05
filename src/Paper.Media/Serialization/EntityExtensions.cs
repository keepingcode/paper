using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Media.Serialization
{
  public static class EntityExtensions
  {
    public static string ToJson(this Entity entity, bool indent = true)
    {
      var serializer = new MediaSerializer(MediaSerializer.JsonSiren);
      var json = serializer.Serialize(entity);
      return indent ? Json.Beautify(json) : json;
    }
  }
}
