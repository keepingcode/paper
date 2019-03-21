using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Reflection;
using static Paper.Media.Payload;

namespace Paper.Media
{
  public static class PayloadExtensions
  {
    public static PropertyMap WithForm(this Payload payload)
    {
      return payload.Form ?? (payload.Form = new PropertyMap());
    }

    public static PropertyMap WithRecord(this Payload payload)
    {
      return payload.Record ?? (payload.Record = new PropertyMap());
    }

    public static RecordCollection WithRecords(this Payload payload)
    {
      return payload.Records ?? (payload.Records = new RecordCollection());
    }

    public static void SetProperty(this Payload payload, string name, object value)
    {
      var tokens = name.Split('.');
      var property = tokens.First();
      var key = tokens.Skip(1).Select(x => x.StartsWith("@") ? x : x.ChangeCase(TextCase.PascalCase));

      if (property.EqualsAnyIgnoreCase(nameof(payload.Form)))
      {
        payload.WithForm().SetProperty(key, value);
        return;
      }

      if (property.EqualsAnyIgnoreCase(nameof(payload.Record)))
      {
        payload.WithRecord().SetProperty(key, value);
        return;
      }

      if (property.EqualsAnyIgnoreCase(nameof(payload.Records)))
      {
        var list = (IEnumerable)value;
        foreach (var item in list)
        {
          var map = item as PropertyMap;
          if (map == null)
          {
            foreach (var propertyName in item._GetPropertyNames())
            {
              var propertyValue = item._Get(propertyName);
              map.SetProperty(propertyName, propertyValue);
            }
          }
          payload.WithRecords().Add(map);
        }
        return;
      }

      var match = Regex.Match(property, @"^Records\[(\d+)\]$", RegexOptions.IgnoreCase);
      if (match.Success)
      {
        var index = int.Parse(match.Groups[1].Value);

        var records = payload.WithRecords();
        while (index >= records.Count)
        {
          records.Add(new PropertyMap());
        }

        records[index].SetProperty(key, value);

        return;
      }

      throw new NotSupportedException("Propriedade não suportada: " + property);
    }

    public static object GetProperty(this Payload payload, string name)
    {
      var tokens = name.Split('.');
      var property = tokens.First();
      var key = tokens.Skip(1).Select(x => x.StartsWith("@") ? x : x.ChangeCase(TextCase.PascalCase));

      if (property.EqualsAnyIgnoreCase(nameof(payload.Form)))
      {
        return payload.Form?.GetProperty(key);
      }

      if (property.EqualsAnyIgnoreCase(nameof(payload.Record)))
      {
        return payload.Record?.GetProperty(key);
      }

      var match = Regex.Match(property, @"^Records\[(\d+)\]$", RegexOptions.IgnoreCase);
      if (match.Success)
      {
        var index = int.Parse(match.Groups[1].Value);
        return payload.Records?.ElementAtOrDefault(index)?.GetProperty(key);
      }

      throw new NotSupportedException("Propriedade não suportada: " + property);
    }

    public static string ToJson(this Payload payload, bool indent = true)
    {
      var entity = payload.ToEntity();
      var serializer = new MediaSerializer(MediaSerializer.Json);
      var json = serializer.Serialize(entity);
      return indent ? Json.Beautify(json) : json;
    }
  }
}