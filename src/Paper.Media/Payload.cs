using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Paper.Media.Design;
using Toolset;
using Toolset.Collections;

namespace Paper.Media
{
  [DataContract(Namespace = Namespaces.Default, Name = "Payload")]
  public class Payload
  {
    [DataMember]
    public PropertyMap Data { get; set; }

    [DataMember]
    public RowCollection Rows { get; set; }

    // TODO: Ainda nao suportado.
    [DataMember]
    public PropertyMap Form { get; set; }

    public Entity ToEntity()
    {
      var entity = new Entity();

      if (Data != null)
      {
        CopyMapToEntity(Data, entity, ClassNames.Record);
      }

      if (Rows != null)
      {
        foreach (var row in Rows)
        {
          var record = new Entity();
          CopyMapToEntity(row, entity, ClassNames.Record);
          entity.AddEntity(record);
        }
      }

      if (Form != null)
      {
        CopyMapToEntity(Data, entity, ClassNames.Form);
      }

      return entity;
    }

    public static Payload FromEntity(Entity entity)
    {
      var payload = new Payload();

      var hasData = entity.Class.Has(ClassNames.Record);
      if (hasData)
      {
        payload.Data = new PropertyMap();
        CopyEntityToMap(entity, payload.Data, ClassNames.Record);
      }

      var hasRows = entity.Entities?.Any(e => e.Class.Has(ClassNames.Record)) == true;
      if (hasRows)
      {
        payload.Rows = new RowCollection();

        var records = entity.Entities.Where(e => e.Class.Has(ClassNames.Record));
        foreach (var record in records)
        {
          var map = new PropertyMap();
          CopyEntityToMap(record, map, ClassNames.Record);
          payload.Rows.Add(map);
        }
      }

      var hasForm = entity.Class.Has(ClassNames.Form);
      if (hasForm)
      {
        payload.Form = new PropertyMap();
        CopyEntityToMap(entity, payload.Form, ClassNames.Form);
      }

      return payload;
    }

    private static void CopyEntityToMap(Entity entity, PropertyMap map, string defaultClass)
    {
      var @class = entity.Class?.FirstOrDefault(x => char.IsUpper(x.First())) ?? defaultClass;
      if (@class != null)
      {
        map.Add("@class", @class);
      }
      if (entity.Properties is PropertyMap properties)
      {
        foreach (var property in properties.Where(x => !x.Key.StartsWith("__")))
        {
          map.Add(property.Key, property.Value);
        }
      }
    }

    private static void CopyMapToEntity(PropertyMap map, Entity entity, string defaultClass)
    {
      var @class = map["@class"];
      var properties = map.Where(x => !x.Key.EqualsAnyIgnoreCase("@class"));
      entity.AddClass(new[] { defaultClass, @class?.ToString() }.NonNull());
      entity.AddProperties(properties);
    }

    [CollectionDataContract(Namespace = Namespaces.Default, Name = "Rows", ItemName = "Row")]
    public class RowCollection : List<PropertyMap>
    {
    }
  }
}