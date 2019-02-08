using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media.Design
{
  /// <summary>
  /// Extensões de desenho de propriedades de objetos Entity.
  /// </summary>
  public static class PropertyExtensions
  {
    #region Extensões para Entity

    /// <summary>
    /// Adiciona uma propriedade à entidade.
    /// </summary>
    /// <param name="holder">A entidade a ser modificada.</param>
    /// <param name="name">O nome da propriedade.</param>
    /// <param name="value">O valor da propriedade.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static THolder AddProperty<THolder>(this THolder holder, string name, object value)
      where THolder : IPropertyHolder
    {
      if (holder.Properties == null)
      {
        holder.Properties = new PropertyCollection();
      }
      holder.Properties.AddProperty(name, value);
      return holder;
    }

    /// <summary>
    /// Adiciona à entidade uma propriedade do tipo <see cref="PropertyCollection"/>
    /// para suportar propriedades complexas.
    /// </summary>
    /// <param name="holder">A entidade a ser modificada.</param>
    /// <param name="name">O nome da propriedade.</param>
    /// <param name="property">
    /// Uma função para construção da instância de <see cref="PropertyCollection"/>.
    /// </param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static THolder AddProperty<THolder>(this THolder holder, string name, Action<PropertyCollection> itemBuilder, params Action<PropertyCollection>[] otherItemBuilders)
      where THolder : IPropertyHolder
    {
      if (holder.Properties == null)
      {
        holder.Properties = new PropertyCollection();
      }
      holder.Properties.AddProperty(name, itemBuilder);
      otherItemBuilders.ForEach(b => holder.Properties.AddProperty(name, b));
      return holder;
    }

    /// <summary>
    /// Adiciona uma propriedade à entidade.
    /// </summary>
    /// <typeparam name="TValue">Tipo do valor da propriedade.</typeparam>
    /// <param name="holder">A entidade a ser modificada.</param>
    /// <param name="name">O nome da propriedade.</param>
    /// <param name="builder">
    /// Construtor de valor da propriedade.
    /// O construtor deve retornar o novo valor da propriedade.
    /// </param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static THolder AddProperty<THolder, TValue>(this THolder holder, string name, Func<TValue, TValue> builder)
      where THolder : IPropertyHolder
      where TValue : class
    {
      if (holder.Properties == null)
      {
        holder.Properties = new PropertyCollection();
      }
      holder.Properties.AddProperty<TValue>(name, builder);
      return holder;
    }

    /// <summary>
    /// Adiciona uma propriedade à entidade.
    /// </summary>
    /// <typeparam name="TValue">Tipo do valor da propriedade.</typeparam>
    /// <param name="holder">A entidade a ser modificada.</param>
    /// <param name="name">O nome da propriedade.</param>
    /// <param name="builder">Construtor de valor da propriedade.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static THolder AddProperty<THolder, TValue>(this THolder holder, string name, Action<TValue> builder)
      where THolder : IPropertyHolder
      where TValue : class
    {
      if (holder.Properties == null)
      {
        holder.Properties = new PropertyCollection();
      }
      holder.Properties.AddProperty<TValue>(name, builder);
      return holder;
    }

    /// <summary>
    /// Copia as propriedads do objeto indicado para as propriedades da entidade.
    /// </summary>
    /// <param name="holder">A entidade a ser modificada.</param>
    /// <param name="graph">O objeto que terá suas propriedades copiadas.</param>
    /// <param name="select">Nome das propriedades consideradas.</param>
    /// <param name="except">Nome das propriedades excluídas.</param>
    /// <returns>A própria instância da entidade modificada.</returns>
    public static THolder AddProperties<THolder>(this THolder holder, object graph, string[] select = null, string[] except = null)
      where THolder : IPropertyHolder
    {
      if (holder.Properties == null)
      {
        holder.Properties = new PropertyCollection();
      }

      var properties =
        from property in (graph as IEnumerable<Property> ?? Property.UnwrapProperties(graph))
        where @select == null || property.Name.EqualsAnyIgnoreCase(@select)
        where except == null || !property.Name.EqualsAnyIgnoreCase(except)
        select property;

      holder.Properties.AddMany(properties);
      return holder;
    }

    #endregion

    #region Extensões para Property

    /// <summary>
    /// Copia as propriedades do objeto indicado para a coleção de propriedads.
    /// </summary>
    /// <param name="propertyCollection">A coleção de propriedades a ser modificada.</param>
    /// <param name="graph">O objeto que terá suas propriedads copiadas.</param>
    /// <returns>A própria instância da coleção de propriedades modificada.</returns>
    public static PropertyCollection AddProperty(this PropertyCollection propertyCollection, object graph)
    {
      propertyCollection.Add(graph);
      return propertyCollection;
    }

    /// <summary>
    /// Adiciona uma propriedade à coleção de propriedades.
    /// </summary>
    /// <param name="propertyCollection">A coleção de propriedades a ser modificada.</param>
    /// <param name="name">O nome da propriedade.</param>
    /// <param name="value">O valor da propriedade.</param>
    /// <returns>A própria instância da coleção de propriedades modificada.</returns>
    public static PropertyCollection AddProperty(this PropertyCollection propertyCollection, string name, object value)
    {
      Property property = EnsureProperty(propertyCollection, name); 
      property.Value = value;
      return propertyCollection;
    }

    /// <summary>
    /// Adiciona à coleção de propriedades uma propriedade do tipo <see cref="PropertyCollection"/>
    /// para suportar propriedades complexas.
    /// </summary>
    /// <param name="propertyCollection">A coleção de propriedades a ser modificada.</param>
    /// <param name="name">O nome da propriedade.</param>
    /// <param name="propertyCollection">
    /// Uma função para construção da instância de <see cref="PropertyCollection"/>.
    /// </param>
    /// <returns>A própria instância da coleção de propriedades modificada.</returns>
    public static PropertyCollection AddProperty(this PropertyCollection propertyCollection, string name, Action<PropertyCollection> builder)
    {
      Property property = EnsureProperty(propertyCollection, name);

      var collection = property.Value as PropertyCollection;
      if (collection == null)
      {
        property.Value = collection = new PropertyCollection();
      }

      builder.Invoke(collection);

      return propertyCollection;
    }

    /// <summary>
    /// Adiciona uma proprieedade à coleção de propriedades.
    /// </summary>
    /// <typeparam name="T">Tipo do valor da propriedade.</typeparam>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="name">O nome da propriedade.</param>
    /// <param name="builder">
    /// Construtor de valor da propriedade.
    /// O construtor deve retornar o novo valor da propriedade.
    /// </param>
    /// <returns>A própria instância da coleção de propriedades modificada.</returns>
    public static PropertyCollection AddProperty<T>(this PropertyCollection propertyCollection, string name, Func<T, T> builder)
      where T : class
    {
      Property property = EnsureProperty(propertyCollection, name);

      var value = property.Value as T;
      if (value == null)
      {
        if (typeof(T).IsArray)
        {
          var itemType = typeof(T).GetElementType();
          property.Value = value = (T)(object)Array.CreateInstance(itemType, 0);
        }
        else
        {
          property.Value = value = Activator.CreateInstance<T>();
        }
      }

      property.Value = builder.Invoke(value);

      return propertyCollection;
    }

    /// <summary>
    /// Adiciona uma proprieedade à coleção de propriedades.
    /// </summary>
    /// <typeparam name="T">Tipo do valor da propriedade.</typeparam>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="name">O nome da propriedade.</param>
    /// <param name="builder">Construtor de valor da propriedade.</param>
    /// <returns>A própria instância da coleção de propriedades modificada.</returns>
    public static PropertyCollection AddProperty<T>(this PropertyCollection propertyCollection, string name, Action<T> builder)
      where T : class
    {
      Property property = EnsureProperty(propertyCollection, name);

      var value = property.Value as T;
      if (value == null)
      {
        if (typeof(T).IsArray)
        {
          var itemType = typeof(T).GetElementType();
          property.Value = value = (T)(object)Array.CreateInstance(itemType, 0);
        }
        else
        {
          property.Value = value = Activator.CreateInstance<T>();
        }
      }

      builder.Invoke(value);

      return propertyCollection;
    }

    private static Property EnsureProperty(PropertyCollection propertyCollection, string name)
    {
      var property = new Property { Value = propertyCollection };

      var names = name.Split('/', '\\', '.');
      foreach (var currentName in names)
      {
        var parent = property.Value as PropertyCollection;
        if (parent == null)
        {
          property.Value = parent = new PropertyCollection();
        }

        property = parent[currentName];
        if (property == null)
        {
          parent[currentName] = property = new Property { Name = currentName };
        }
      }

      return property;
    }

    #endregion
  }
}