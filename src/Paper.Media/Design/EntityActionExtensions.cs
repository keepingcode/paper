using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Paper.Media.Utilities;
using Toolset;
using Toolset.Collections;
using Toolset.Net;

namespace Paper.Media.Design
{
  /// <summary>
  /// Extensões de desenho de ações de objetos Entity.
  /// </summary>
  public static class EntityActionExtensions
  {
    #region Extensões de Entity

    /// <summary>
    /// Adiciona uma ação à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="action">A instância da ação.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddActions(this Entity entity, params EntityAction[] actions)
    {
      foreach (var action in actions)
      {
        AddAction(entity, action);
      }
      return entity;
    }

    /// <summary>
    /// Adiciona uma ação à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="action">A instância da ação.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddActions(this Entity entity, IEnumerable<EntityAction> actions)
    {
      foreach (var action in actions)
      {
        AddAction(entity, action);
      }
      return entity;
    }

    /// <summary>
    /// Adiciona uma ação à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="action">A instância da ação.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddAction(this Entity entity, EntityAction action)
    {
      if (string.IsNullOrEmpty(action.Name))
      {
        action.Name = MakeActionName(entity.Actions);
      }

      var actions = entity.WithActions();
      actions.RemoveWhen(x => x.Name.EqualsIgnoreCase(action.Name));
      actions.Add(action);

      return entity;
    }

    /// <summary>
    /// Adiciona uma ação à entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="name">Nome da ação.</param>
    /// <param name="options">Função de construção da ação.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static Entity AddAction(this Entity entity, string name, Action<EntityAction> options)
    {
      var action = GetOrAddAction(entity.WithActions(), name);
      options.Invoke(action);
      return entity;
    }

    /// <summary>
    /// Obtém a ação com o nome indicado ou cria uma nova.
    /// A ação criada é automaticamente inserida na lista de ações conhecidas.
    /// </summary>
    /// <param name="actions">A lista de ações conhecidas.</param>
    /// <param name="actionName">O nome da ação.</param>
    /// <returns>A ação obtida ou criada.</returns>
    private static EntityAction GetOrAddAction(EntityActionCollection actions, string actionName)
    {
      if (string.IsNullOrEmpty(actionName))
      {
        actionName = MakeActionName(actions);
      }

      var action = actions.FirstOrDefault(x => x.Name.EqualsIgnoreCase(actionName));
      if (action == null)
      {
        action = new EntityAction
        {
          Name = actionName,
          Title = actionName.Replace("Action", "Ação").ChangeCase(TextCase.ProperCase)
        };
        actions.Add(action);
      }

      return action;
    }

    /// <summary>
    /// Cria um nome único para uma ação.
    /// </summary>
    /// <param name="actions">A coleção das ações conhecidas.</param>
    /// <returns>O nome único para a ação.</returns>
    private static string MakeActionName(EntityActionCollection actions)
    {
      string actionName = null;

      var index = actions.Count + 1;
      do
      {
        actionName = $"Action{index}";
      } while (actions.Any(x => x.Name.EqualsIgnoreCase(actionName)));

      return actionName;
    }

    #endregion

    #region With*

    public static FieldCollection WithFields(this EntityAction action)
    {
      return action.Fields ?? (action.Fields = new FieldCollection());
    }

    #endregion

    #region Básicos

    public static EntityAction SetName(this EntityAction action, string name)
    {
      action.Name = name;
      return action;
    }

    /// <summary>
    /// Define o tipo (MimeType) do documento retornado pelo link.
    /// Como "text/xml", "application/pdf", etc.
    /// </summary>
    /// <param name="action">A ação a ser modificada.</param>
    /// <param name="mediaType">O mime type do link.</param>
    /// <returns>A própria instância do link modificada.</returns>
    public static EntityAction SetType(this EntityAction action, string mediaType)
    {
      action.Type = mediaType;
      return action;
    }

    /// <summary>
    /// Define o endpoint de destino da ação.
    /// Quando omitido a URL da ação é a mesma URL da sua entidade.
    /// </summary>
    /// <param name="action">A ação a ser modificada.</param>
    /// <param name="href">A URL de destino da ação.</param>
    /// <returns>A própria instância da ação modificada.</returns>
    public static EntityAction SetHref(this EntityAction action, Href href)
    {
      action.Href = href;
      return action;
    }

    /// <summary>
    /// Define o método HTTP da ação.
    /// Por padrão o método usado é POST.
    /// Os valores possíveis são:
    /// -   GET
    /// -   POST
    /// -   PUT
    /// -   PATCH
    /// -   DELETE
    /// </summary>
    /// <param name="action">A ação a ser modificada.</param>
    /// <param name="method">O método HTTP.</param>
    /// <returns>A própria instância da ação modificada.</returns>
    public static EntityAction SetMethod(this EntityAction action, string method)
    {
      action.Method = method;
      return action;
    }

    /// <summary>
    /// Define o método HTTP da ação.
    /// Por padrão o método usado é POST.
    /// </summary>
    /// <param name="action">A ação a ser modificada.</param>
    /// <param name="method">O método HTTP.</param>
    /// <returns>A própria instância da ação modificada.</returns>
    public static EntityAction SetMethod(this EntityAction action, Method method)
    {
      action.Method = method.GetName();
      return action;
    }

    #endregion

    #region AddField<T>

    public static EntityAction AddField<T>(this EntityAction action, Expression<Func<T, object>> keySelector, Action<Field> options = null)
    {
      ExtractInfo(
          keySelector
        , out string fieldName
        , out string fieldTitle
        , out string fieldDataTtype
      );
      DoAddField(
          action
        , fieldName
        , fieldTitle
        , fieldDataTtype
        , options
        , false
      );
      return action;
    }

    public static EntityAction AddField<T>(this EntityAction action, Expression<Func<T, object>> keySelector, string title)
    {
      ExtractInfo(keySelector, 
        out string fieldName, 
        out string fieldTitle, 
        out string fieldDataTtype
      );
      DoAddField(
          action
        , fieldName
        , title ?? fieldTitle
        , fieldDataTtype
        , null
        , false
      );
      return action;
    }

    public static EntityAction AddFieldMulti<T>(this EntityAction action, Expression<Func<T, object>> keySelector, Action<Field> options = null)
    {
      ExtractInfo(
          keySelector
        , out string fieldName
        , out string fieldTitle
        , out string fieldDataTtype
      );
      DoAddField(
          action
        , fieldName
        , fieldTitle
        , fieldDataTtype
        , options
        , true
      );
      return action;
    }

    public static EntityAction AddFieldMulti<T>(this EntityAction action, Expression<Func<T, object>> keySelector, string title)
    {
      ExtractInfo(keySelector,
        out string fieldName,
        out string fieldTitle,
        out string fieldDataTtype
      );
      DoAddField(
          action
        , fieldName
        , title ?? fieldTitle
        , fieldDataTtype
        , null
        , true
      );
      return action;
    }

    #endregion

    #region AddFieldsFrom<T>

    public static EntityAction AddFieldsFrom<T>(this EntityAction action)
    {
      foreach (var property in typeof(T).GetProperties())
      {
        var fieldName = Conventions.MakeName(property);
        var fieldTitle = Conventions.MakeTitle(property);
        var fieldDataType = Conventions.MakeDataType(property);

        DoAddField(
            action
          , fieldName
          , fieldTitle
          , fieldDataType
          , null
        , false
        );
      }
      return action;
    }

    public static EntityAction AddFieldsMultiFrom<T>(this EntityAction action)
    {
      foreach (var property in typeof(T).GetProperties())
      {
        var fieldName = Conventions.MakeName(property);
        var fieldTitle = Conventions.MakeTitle(property);
        var fieldDataType = Conventions.MakeDataType(property);

        DoAddField(
            action
          , fieldName
          , fieldTitle
          , fieldDataType
          , null
        , true
        );
      }
      return action;
    }

    #endregion

    #region AddField

    public static EntityAction AddField(this EntityAction action, Field field)
    {
      action.WithFields().Add(field);
      return action;
    }

    public static EntityAction AddField(this EntityAction action, string name, Action<Field> options)
    {
      DoAddField(
          action
        , name
        , null
        , null
        , options
        , false
      );
      return action;
    }

    public static EntityAction AddFieldMulti(this EntityAction action, string name, Action<Field> options)
    {
      DoAddField(
          action
        , name
        , null
        , null
        , options
        , true
      );
      return action;
    }

    #endregion

    #region GetField

    public static Field GetField(this EntityAction action, string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        return null;

      var field = action.Fields?.FirstOrDefault(
        x => x.Name.EqualsIgnoreCase(fieldName)
      );
      return field;
    }

    public static Field GetField(this FieldCollection fields, string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        return null;

      var field = fields.FirstOrDefault(
        x => x.Name.EqualsIgnoreCase(fieldName)
      );
      return field;
    }

    #endregion

    #region Algoritmos de apoio

    private static void DoAddField(
        EntityAction action
      , string name
      , string title
      , string dataType
      , Action<Field> options
      , bool allowMulti
      )
    {
      var field = GetOrAddField(action.WithFields(), name);

      field.SetTitle(title ?? name.ChangeCase(TextCase.ProperCase));
      field.SetDataType(dataType ?? DataTypeNames.String);

      if (options != null)
      {
        options.Invoke(field);
      }
      else
      {
        // Aplicando padroes extendidos
        if (allowMulti)
        {
          switch (field.DataType)
          {
            case DataTypeNames.Boolean:
              break;

            case DataTypeNames.Integer:
            case DataTypeNames.Decimal:
            case DataTypeNames.Datetime:
            case DataTypeNames.Date:
            case DataTypeNames.Time:
              field.SetAllowMany();
              field.SetAllowRange();
              break;

            case DataTypeNames.String:
              field.SetAllowMany();
              field.SetAllowWildcards();
              break;

            case DataTypeNames.Record:
              field.SetAllowMany();
              break;
          }
        }
      }
    }

    /// <summary>
    /// Obtém o campo com o nome indicado ou cria um novo.
    /// O campo criado é automaticamente inserido na lista de campos conhecidos.
    /// </summary>
    /// <param name="fields">A lista de campos conhecidos.</param>
    /// <param name="fieldName">O nome do campo.</param>
    /// <returns>O campo obtido ou criado.</returns>
    private static Field GetOrAddField(FieldCollection fields, string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
      {
        fieldName = MakeFieldName(fields);
      }

      var field = fields.FirstOrDefault(x => x.Name.EqualsIgnoreCase(fieldName));
      if (field == null)
      {
        field = new Field
        {
          Name = fieldName,
          Title = fieldName.Replace("Field", "Campo").ChangeCase(TextCase.ProperCase)
        };
        fields.Add(field);
      }

      return field;
    }

    /// <summary>
    /// Cria um nome único para um campo.
    /// </summary>
    /// <param name="fields">A coleção dos campos conhecidos.</param>
    /// <returns>O nome único para o campo</returns>
    private static string MakeFieldName(FieldCollection fields)
    {
      string fieldName = null;

      var index = fields.Count + 1;
      do
      {
        fieldName = $"Field{index}";
      } while (fields.Any(x => x.Name.EqualsIgnoreCase(fieldName)));

      return fieldName;
    }

    private static void ExtractInfo<T>(
        Expression<Func<T, object>> keySelector
      , out string fieldName
      , out string fieldTitle
      , out string fieldDataType)
    {
      var expression = ExpressionUtils.FindMemberExpression(keySelector);
      if (expression == null)
        throw new Exception("A expressão não é válida. Apenas um seletor de propriedade de objeto é suportado.");

      var memberType =
        (expression.Member as PropertyInfo)?.PropertyType
        ?? (expression.Member as FieldInfo)?.FieldType
        ?? typeof(string);

      fieldName = Conventions.MakeName(expression.Member.Name);
      fieldTitle = Conventions.MakeTitle(expression.Member.Name);
      fieldDataType = Conventions.MakeDataType(memberType);
    }

    #endregion
  }
}