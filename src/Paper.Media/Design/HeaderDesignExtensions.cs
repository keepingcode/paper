using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paper.Media.Data;
using Toolset.Collections;

namespace Paper.Media.Design
{
  public static class HeaderDesignExtensions
  {
    public static HeaderDesign SetTitle(this HeaderDesign designer, string title)
    {
      designer.Title = title;
      return designer;
    }

    public static HeaderDesign SetDataType(this HeaderDesign designer, DataType dataType)
    {
      designer.DataType = dataType.GetName();
      return designer;
    }

    public static HeaderDesign SetDataType(this HeaderDesign designer, string dataType)
    {
      designer.DataType = dataType;
      return designer;
    }

    public static HeaderDesign SetDataType(this HeaderDesign designer, Type dataType)
    {
      designer.DataType = Conventions.MakeDataType(dataType);
      return designer;
    }

    public static HeaderDesign SetHidden(this HeaderDesign designer, bool hidden)
    {
      designer.Hidden = hidden;
      return designer;
    }

    public static HeaderDesign SetOrder(this HeaderDesign designer, SortOrder? order)
    {
      designer.Order = order;
      return designer;
    }

    public static HeaderDesign WithHeaderEntity(this HeaderDesign designer, Action<Entity> options)
    {
      options.Invoke(designer.HeaderEntity);
      return designer;
    }

    #region Rel

    /// <summary>
    /// Adiciona relacionamentos à classe.
    /// </summary>
    /// <typeparam name="HeaderDesign">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos adicionados.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static HeaderDesign AddRel(this HeaderDesign header, params string[] rels)
    {
      header.HeaderEntity.AddRel(rels);
      return header;
    }

    /// <summary>
    /// Adiciona relacionamentos à classe.
    /// </summary>
    /// <typeparam name="HeaderDesign">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos adicionados.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static HeaderDesign AddRel(this HeaderDesign header, IEnumerable<string> rels)
    {
      header.HeaderEntity.AddRel(rels);
      return header;
    }

    /// <summary>
    /// Adiciona relacionamentos à classe.
    /// </summary>
    /// <typeparam name="HeaderDesign">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos adicionados.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static HeaderDesign AddRel(this HeaderDesign header, params Rel[] rels)
    {
      header.HeaderEntity.AddRel(rels);
      return header;
    }

    /// <summary>
    /// Adiciona relacionamentos à classe.
    /// </summary>
    /// <typeparam name="HeaderDesign">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos adicionados.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static HeaderDesign AddRel(this HeaderDesign header, IEnumerable<Rel> rels)
    {
      header.HeaderEntity.AddRel(rels);
      return header;
    }

    /// <summary>
    /// Redefine os relacionamentos da classe.
    /// </summary>
    /// <typeparam name="HeaderDesign">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos definidos.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static HeaderDesign SetRel(this HeaderDesign header, params string[] rels)
    {
      header.HeaderEntity.SetRel(rels);
      return header;
    }

    /// <summary>
    /// Redefine os relacionamentos da classe.
    /// </summary>
    /// <typeparam name="HeaderDesign">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos definidos.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static HeaderDesign SetRel(this HeaderDesign header, IEnumerable<string> rels)
    {
      header.HeaderEntity.SetRel(rels);
      return header;
    }

    /// <summary>
    /// Redefine os relacionamentos da classe.
    /// </summary>
    /// <typeparam name="HeaderDesign">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos definidos.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static HeaderDesign SetRel(this HeaderDesign header, params Rel[] rels)
    {
      header.HeaderEntity.SetRel(rels);
      return header;
    }

    /// <summary>
    /// Redefine os relacionamentos da classe.
    /// </summary>
    /// <typeparam name="HeaderDesign">O tipo da entidade.</typeparam>
    /// <param name="entity">A entidade modificada.</param>
    /// <param name="rels">Os relacionamentos definidos.</param>
    /// <returns>A própria instância da entidade para encadeamento.</returns>
    public static HeaderDesign SetRel(this HeaderDesign header, IEnumerable<Rel> rels)
    {
      header.HeaderEntity.SetRel(rels);
      return header;
    }

    #endregion
  }
}