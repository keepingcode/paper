using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Paper.Media.Attributes;
using Toolset;
using Toolset.Collections;
using Toolset.Reflection;

namespace Paper.Media.Design
{
  /// <summary>
  /// Extensões de desenho de instâncias de Field
  /// </summary>
  public static class FieldExtensions
  {
    #region Set properties

    /// <summary>
    /// Adiciona um texto substitudo para o campo.
    /// Categorias são usadas como agrupadores de campos.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="placeholder">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetPlaceholder(this Field field, string placeholder)
    {
      field.Placeholder = placeholder;
      return field;
    }

    /// <summary>
    /// Adiciona uma categoria ao campo.
    /// Categorias são usadas como agrupadores de campos.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="category">A categoria do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetCategory(this Field field, string category)
    {
      field.Category = category;
      return field;
    }

    /// <summary>
    /// Define o tipo de dado do campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="dataType">A categoria do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetType(this Field field, string fieldType)
    {
      field.Type = fieldType;
      return field;
    }

    /// <summary>
    /// Define o tipo de dado do campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="dataType">A categoria do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetType(this Field field, FieldType fieldType)
    {
      field.Type = fieldType.GetName();
      return field;
    }

    /// <summary>
    /// Define o tipo de dado do campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="dataType">A categoria do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetType(this Field field, Type fieldType)
    {
      var dataType = DataTypeNames.FromType(fieldType);
      field.Type = FieldTypeNames.FromDataType(dataType);
      return field;
    }

    /// <summary>
    /// Define o tipo de dado do campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="dataType">A categoria do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetDataType(this Field field, string dataType)
    {
      field.DataType = dataType;
      return field;
    }

    /// <summary>
    /// Define o tipo de dado do campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="dataType">A categoria do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetDataType(this Field field, DataType dataType)
    {
      field.DataType = dataType.GetName();
      return field;
    }

    /// <summary>
    /// Define o tipo de dado do campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="dataType">A categoria do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetDataType(this Field field, Type dataType)
    {
      field.DataType = Conventions.MakeDataType(dataType);
      return field;
    }

    /// <summary>
    /// Marca ou desmarca o campo como somente leitura.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="readOnly">O valor da propriedade somente leitura do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetReadOnly(this Field field, bool readOnly = true)
    {
      field.ReadOnly = readOnly;
      return field;
    }

    /// <summary>
    /// Marca ou desmarca o campo como requerido.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="required">O valor da propriedade requerida do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetRequired(this Field field, bool required = true)
    {
      field.Required = required;
      return field;
    }

    /// <summary>
    /// Marca ou desmarca o campo como oculto.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="hidden">O valor da propriedade oculta do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetHidden(this Field field, bool hidden = true)
    {
      field.Type = hidden ? FieldTypeNames.Hidden : null;
      return field;
    }

    /// <summary>
    /// Define o valor do campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="value">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetValue(this Field field, object value)
    {
      field.Value = value;
      return field;
    }

    /// <summary>
    /// Habilita ou desabilita o suporte a múltiplos valores para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="allowMany">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetAllowMany(this Field field, bool allowMany = true)
    {
      field.AllowMany = allowMany;
      return field;
    }

    /// <summary>
    /// Habilita ou desabilita o suporte a períodos, início e fim, para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="allowRange">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetAllowRange(this Field field, bool allowRange = true)
    {
      field.AllowRange = allowRange;
      return field;
    }

    /// <summary>
    /// Habilita ou desabilita o suporte a caracteres curingas para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="allowWildcards">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetAllowWildcards(this Field field, bool allowWildcards = true)
    {
      field.AllowWildcard = allowWildcards;
      return field;
    }

    /// <summary>
    /// Habilita ou desabilita o suporte a múltiplas linhas para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="multiline">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetMultiline(this Field field, bool multiline = true)
    {
      field.Multiline = multiline;
      return field;
    }

    /// <summary>
    /// Define o tamanho máximo para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="maxLength">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetMaxLength(this Field field, int maxLength)
    {
      field.MaxLength = maxLength;
      return field;
    }

    /// <summary>
    /// Define o tamanho mínimo para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="minLength">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetMinLength(this Field field, int minLength)
    {
      field.MinLength = minLength;
      return field;
    }

    /// <summary>
    /// Define um padrão de texto para validação do campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="pattern">O valor do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetPattern(this Field field, string pattern)
    {
      field.Pattern = pattern;
      return field;
    }

    /// <summary>
    /// Víncula um provedor de dados para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="href">A rota do provedor de dados.</param>
    /// <param name="keys">Nomes dos campos chaves.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetProvider(this Field field, string href, params string[] keys)
    {
      field.Provider = new FieldProvider();
      field.Provider.Href = href;
      field.Provider.Keys = new NameCollection(keys);
      return field;
    }

    /// <summary>
    /// Víncula um provedor de dados para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="href">A rota do provedor de dados.</param>
    /// <param name="keys">Nomes dos campos chaves.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetProvider(this Field field, string href, IEnumerable<string> keys)
    {
      if (keys == null)
      {
        keys = Enumerable.Empty<string>();
      }

      field.Provider = new FieldProvider();
      field.Provider.Href = href;
      field.Provider.Keys = new NameCollection(keys);
      return field;
    }

    /// <summary>
    /// Víncula um provedor de dados para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="provider">Provedor de dados do campo.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetProvider(this Field field, FieldProvider provider)
    {
      field.Provider = provider;
      return field;
    }

    /// <summary>
    /// Víncula um provedor de dados para o campo.
    /// </summary>
    /// <param name="field">O campo a ser modificado.</param>
    /// <param name="options">Configurador do provedor.</param>
    /// <returns>A própria instância do campo modificado.</returns>
    public static Field SetProvider(this Field field, Action<FieldProvider> options)
    {
      field.Provider = new FieldProvider();
      options?.Invoke(field.Provider);
      return field;
    }

    #endregion

    #region Set defaults via reflection

    /// <summary>
    /// Infere valores padrão para as propriedades a partir de um campo ou propriedade
    /// de objeto.
    /// </summary>
    /// <param name="field">O campo de uma ação modificado.</param>
    /// <param name="member">O campo ou propriedade de um objeto.</param>
    /// <returns>O próprio campo da ação modificado.</returns>
    public static Field SetDefaults(this Field field, MemberInfo member)
    {
      return ExtractDefaults(field, member);
    }

    /// <summary>
    /// Infere valores padrão para as propriedades a partir de um campo ou propriedade
    /// de objeto.
    /// </summary>
    /// <param name="field">O campo de uma ação modificado.</param>
    /// <param name="member">O campo ou propriedade de um objeto.</param>
    /// <returns>O próprio campo da ação modificado.</returns>
    public static Field SetDefaults(this Field field, ParameterInfo member)
    {
      return ExtractDefaults(field, member);
    }

    private static Field ExtractDefaults(this Field field, object member)
    {
      Type type;

      if (member is ParameterInfo)
      {
        type = ((ParameterInfo)member).ParameterType;
      }
      else if (member is FieldInfo)
      {
        type = ((FieldInfo)member).FieldType;
      }
      else
      {
        type = ((PropertyInfo)member).PropertyType;
      }

      SetDefaultFor<CategoryAttribute>(field, member, type);
      SetDefaultFor<PlaceholderAttribute>(field, member, type);
      SetDefaultFor<ReadOnlyAttribute>(field, member, type);
      SetDefaultFor<PatternAttribute>(field, member, type);
      SetDefaultFor<MultilineAttribute>(field, member, type);
      SetDefaultFor<AllowManyAttribute>(field, member, type);
      SetDefaultFor<AllowRangeAttribute>(field, member, type);
      SetDefaultFor<AllowWildcardAttribute>(field, member, type);

      SetDefaultTitle(field, member, type);
      SetDefaultType(field, member, type);
      SetDefaultDataType(field, member, type);
      SetDefaultRequired(field, member, type);
      SetDefaultProvider(field, member, type);
      SetDefaultRange(field, member, type);
      SetDefaultValue(field, member, type);

      return field;
    }

    private static void SetDefaultTitle(Field field, object member, Type type)
    {
      var attr = member._GetAttribute<TitleAttribute>();
      if (attr != null)
      {
        field.Title = attr.Title;
      }
      else
      {
        field.Title = Conventions.MakeName(member);
      }
    }

    private static void SetDefaultType(Field field, object member, Type type)
    {
      var hiddenAttr = member._GetAttribute<HiddenAttribute>();
      var typeAttr = member._GetAttribute<FieldTypeAttribute>();
      if (hiddenAttr?.Hidden == true)
      {
        field.Type = FieldTypeNames.Hidden;
      }
      else if (typeAttr != null)
      {
        field.Type = typeAttr.FieldType;
      }
      else
      {
        field.SetType(type);
      }
    }

    private static void SetDefaultDataType(Field field, object member, Type type)
    {
      var attr = member._GetAttribute<DataTypeAttribute>();
      if (attr != null)
      {
        field.DataType = attr.DataType;
      }
      else
      {
        field.SetDataType(type);
      }
    }

    private static void SetDefaultRequired(Field field, object member, Type type)
    {
      var attr = member._GetAttribute<RequiredAttribute>();
      if (attr != null)
      {
        field.Required = attr.Required;
      }
      else
      {
        field.Required = type.IsValueType && !Is.Nullable(type);
      }
    }

    private static void SetDefaultValue(Field field, object member, Type type)
    {
      var attr1 = member._GetAttribute<DefaultValueAttribute>();
      var attr2 = member._GetAttribute<System.ComponentModel.DefaultValueAttribute>();
      var value = attr1?.DefaultValue ?? attr2?.Value;
      if (value != null)
      {
        field.Value = Change.To(value, type);
      }
    }

    private static void SetDefaultProvider(Field field, object member, Type type)
    {
      var attr = member._GetAttribute<ProviderAttribute>();
      if (attr != null)
      {
        field.Provider = new FieldProvider()._CopyFrom(attr);
      }
    }

    private static void SetDefaultRange(Field field, object member, Type type)
    {
      var attr = member._GetAttribute<RangeAttribute>();
      if (attr != null)
      {
        field.MinLength = attr.Min;
        field.MaxLength = attr.Max;
      }
    }

    private static void SetDefaultFor<TAttribute>(Field field, object member, Type type)
      where TAttribute : Attribute
    {
      var attr = member._GetAttribute<TAttribute>();
      if (attr != null)
      {
        field._CopyFrom(attr);
      }
    }

    #endregion
  }
}