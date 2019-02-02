using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Paper.Media.Design;
using Toolset.Collections;

namespace Paper.Media
{
  [DataContract(Namespace = Namespaces.Default, Name = "Payload")]
  public class Payload
  {
    [DataMember]
    public PropertyCollection Data { get; set; }

    [DataMember]
    public RowCollection Rows { get; set; }

    public Entity ToEntity()
    {
      var entity = new Entity();

      if (Data != null)
      {
        var @class = Data["@class"];
        var properties = Data.Except(@class);
        entity.AddClass(new[] { ClassNames.Data, @class?.Value?.ToString() }.NonNull());
        entity.Properties = new PropertyCollection(properties);
      }

      if (Rows != null)
      {
        foreach (var row in Rows)
        {
          var @class = row["@class"];
          var properties = row.Except(@class);

          var child = new Entity();
          child.AddClass(new[] { ClassNames.Data, @class?.Value?.ToString() }.NonNull());
          child.Properties = new PropertyCollection(properties);

          entity.AddEntity(child);
        }
      }

      return entity;
    }

    public static Payload FromEntity(Entity entity)
    {
      var payload = new Payload();

      var hasData = entity.Class?.Contains(ClassNames.Data) == true;
      if (hasData && entity.Properties is PropertyCollection properties)
      {
        payload.Data = new PropertyCollection();

        var @class = entity.Class?.FirstOrDefault(x => char.IsUpper(x.First())) ?? "data";
        if (@class != null)
        {
          payload.Data.Add(new Property("@class", @class));
        }

        foreach (var property in properties)
        {
          if (!property.Name.StartsWith("_"))
          {
            payload.Data.Add(new Property(property.Name, property.Value));
          }
        }
      }

      var hasRows = entity.Entities?.Any(e => e.Class.Contains(ClassNames.Data)) == true;
      if (hasRows)
      {
        payload.Rows = new RowCollection();

        var children = entity.Entities.Where(e => e.Class.Contains(ClassNames.Data) && e.Properties != null);
        foreach (var child in children)
        {
          var row = new PropertyCollection(); 

          var @class = child.Class?.FirstOrDefault(x => char.IsUpper(x.First())) ?? "row";
          row.Add(new Property("@class", @class));
          
          foreach (var property in child.Properties)
          {
            if (!property.Name.StartsWith("_"))
            {
              row.Add(new Property(property.Name, property.Value));
            }
          }

          payload.Rows.Add(row);
        }
      }

      return payload;
    }

    [CollectionDataContract(Namespace = Namespaces.Default, Name = "Rows", ItemName = "Row")]
    public class RowCollection : List<PropertyCollection>
    {
    }
  }
}
