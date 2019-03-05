using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media.Data
{
  public class Sort
  {
    private readonly List<string> _fields = new List<string>();
    private readonly List<SortedField> _sortedFields = new List<SortedField>();

    public Sort()
    {
    }

    public Sort(params string[] fields)
    {
      _fields.AddRange(fields);
    }

    public Sort(IEnumerable<string> fields)
    {
      _fields.AddRange(fields);
    }

    #region Operacoes com Fields

    public IEnumerable<string> FieldNames => _fields;

    public bool Contains(string fieldName)
    {
      return _fields.Contains(fieldName);
    }

    public void Add(string field)
    {
      if (!_fields.Contains(field))
      {
        _fields.Add(field);
      }
    }

    public void AddRange(IEnumerable<string> fields)
    {
      _fields.AddRange(fields.Except(_fields));
    }

    public void Remove(string fieldName)
    {
      _fields.Remove(fieldName);
    }

    public void Clear()
    {
      _fields.Clear();
      _sortedFields.Clear();
    }

    #endregion

    #region Operacoes com SortedFields

    public IEnumerable<string> SortedFieldNames => _sortedFields.Select(x => x.Name);

    public IEnumerable<SortedField> SortedFields => _sortedFields;

    public SortedField? GetSortedField(string fieldName)
    {
      var field =
        _sortedFields
          .Where(x => x.Name.EqualsIgnoreCase(fieldName))
          .Select(x => (SortedField?)x)
          .FirstOrDefault();
      return field;
    }

    public SortOrder GetSortOrder(string fieldName)
    {
      var field = GetSortedField(fieldName);
      return (field != null) ? field.Value.Order : SortOrder.Unordered;
    }

    public bool ContainsSortedField(string fieldName)
    {
      return _sortedFields.Any(x => x.Name.EqualsIgnoreCase(fieldName));
    }

    public void AddSortedField(string field, SortOrder order)
    {
      AddSortedField(new SortedField(field, order));
    }

    public void AddSortedField(SortedField field)
    {
      var isValid = !_fields.Any() || field.Name.EqualsAnyIgnoreCase(_fields);
      if (isValid)
      {
        _sortedFields.Add(field);
      }
#if DEBUG
      else
      {
        Trace.TraceWarning("O campo não está disponível para ser ordenado: " + field.Name);
      }
#endif
    }

    public void RemoveSortedField(string fieldName)
    {
      _sortedFields.RemoveAll(x => x.Name.EqualsIgnoreCase(fieldName));
    }

    public void ClearSortedFields()
    {
      _sortedFields.Clear();
    }

    #endregion

    public void CopyFrom(UriString uri)
    {
      CopyFromArgValue(uri.GetArg("sort"));
    }

    public void CopyFrom(IDictionary args)
    {
      CopyFromArgValue(args["sort"]);
    }

    private void CopyFromArgValue(object argValue)
    {
      // Formato correto:
      // -  sort[]=field1&sort[]=field2:desc
      // Formato suportado:
      // -  sort=field1:desc

      ClearSortedFields();

      if (argValue == null)
        return;

      string[] fields = null;

      if (argValue is Var var)
      {
        if (var.IsArray)
        {
          fields = Change.To<string[]>(var.Array);
        }
        else if (var.IsValue)
        {
          fields = new[] { Change.To<string>(var.Value) };
        }
      }
      else if (argValue is IEnumerable list)
      {
        fields = Change.To<string[]>(list);
      }
      else if (argValue != null)
      {
        fields = new[] { Change.To<string>(argValue) };
      }

      if (fields == null)
        return;

      foreach (var field in fields)
      {
        var tokens = field.Split(':');
        var name = tokens.First();
        var sort = tokens.Skip(1).FirstOrDefault();

        var order = sort.EqualsAnyIgnoreCase("desc", "descending")
          ? SortOrder.Descending : SortOrder.Ascending;

        AddSortedField(name, order);
      }
    }

    public UriString CreateUri(UriString baseUri)
    {
      var uri = baseUri;
      if (SortedFields.Any())
      {
        var terms = (
          from field in SortedFields
          let name = field.Name.ChangeCase(TextCase.CamelCase)
          let order = (field.Order == SortOrder.Descending) ? ":desc" : ""
          select $"{name}{order}"
        ).ToArray();
        uri = uri.SetArg("sort", terms);
      }
      return uri;
    }

    public void CopyTo(HashMap args)
    {
      if (SortedFields.Any())
      {
        var terms = (
          from field in SortedFields
          let name = field.Name.ChangeCase(TextCase.CamelCase)
          let order = (field.Order == SortOrder.Descending) ? ":desc" : ""
          select $"{name}{order}"
        ).ToArray();
        args["sort"] = terms;
      }
    }

    public override string ToString()
    {
      var terms =
        from field in SortedFields
        let name = field.Name.ChangeCase(TextCase.CamelCase)
        let order = (field.Order == SortOrder.Descending) ? ":desc" : ""
        select $"sort[]={name}{order}";
      return string.Join("&", terms);
    }
  }
}