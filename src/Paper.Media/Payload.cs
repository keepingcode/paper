using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Collections;

namespace Paper.Media
{
  [DataContract(Namespace = Namespaces.Default, Name = "Payload")]
  public class Payload
  {
    [DataMember]
    public PropertyMap Form { get; set; }

    [DataMember]
    public PropertyMap Record { get; set; }

    [DataMember]
    public RecordCollection Records { get; set; }

    public Entity ToEntity()
    {
      var entity = new Entity();

      var hasForm = (Form != null);
      var hasRecord = (Record != null);
      var hasRecords = (Records != null);

      var allRecords = Enumerable.Empty<PropertyMap>();

      if (hasForm)
      {
        entity.AddClass(ClassNames.Form);
        CopyMapToEntity(Form, entity);

        if (hasRecord)
        {
          allRecords = allRecords.Append(Record);
        }
      }
      else if (hasRecord)
      {
        entity.AddClass(ClassNames.Record);
        CopyMapToEntity(Record, entity);
      }
      
      if (hasRecords)
      {
        allRecords = allRecords.Concat(Records);
      }

      foreach (var record in allRecords)
      {
        var child = new Entity();
        child.AddClass(ClassNames.Record);
        if (hasForm)
        {
          child.AddRel(ClassNames.Form);
        }
        CopyMapToEntity(record, child);
        entity.AddEntity(child);
      }

      return entity;
    }

    public static Payload FromEntity(Entity entity)
    {
      var payload = new Payload();
      var children = entity.Children().Where(e => e.Class.Has(ClassNames.Record));

      var hasForm = entity.Class.Has(ClassNames.Form);
      var hasRecord = entity.Class.Has(ClassNames.Record);

      if (hasForm)
      {
        payload.Form = new PropertyMap();
        CopyEntityToMap(entity, payload.Form);

        if (hasRecord)
        {
          children = entity.AsSingle().Concat(children);
        }
      }
      else if (hasRecord)
      {
        payload.Record = new PropertyMap();
        CopyEntityToMap(entity, payload.Record);
      }

      if (children.Any())
      {
        payload.Records = new RecordCollection();
        foreach (var child in children)
        {
          var map = new PropertyMap();
          CopyEntityToMap(child, map);
          payload.Records.Add(map);
        }
      }

      return payload;
    }

    private static void CopyEntityToMap(Entity entity, PropertyMap map)
    {
      var @class = entity.Class?.FirstOrDefault(x => char.IsUpper(x.First()));
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

    private static void CopyMapToEntity(PropertyMap map, Entity entity)
    {
      if (map["@class"] is string @class)
      {
        entity.AddClass(@class);
      }
      var properties = map.Where(x => !x.Key.EqualsAnyIgnoreCase("@class"));
      entity.AddProperties(properties);
    }

    [CollectionDataContract(Namespace = Namespaces.Default, Name = "Records", ItemName = "Record")]
    public class RecordCollection : List<PropertyMap>
    {
    }
  }
}