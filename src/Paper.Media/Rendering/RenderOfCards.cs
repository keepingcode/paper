﻿using System.Collections.Generic;
using System.Linq;
using Paper.Media.Utilities.Types;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Paper.Media.Routing;
using Toolset.Reflection;

namespace Paper.Media.Rendering
{
  static class RenderOfCards
  {
    /// <summary>
    /// Realiza a consulta de registros specificados no Paper e estoca os registros
    /// obtidos no cache indicado.
    /// </summary>
    /// <param name="paper">A instância de IPaper que contém as consultas a dados.</param>
    /// <param name="cache">O cache para estocagem dos registros consultados.</param>
    public static void CacheData(IPaper paper, IContext context)
    {
      if (paper._Has("GetCards"))
      {
        var data = paper._Call("GetCards");
        if (data != null)
        {
          var dataWrapper = DataWrapperEnumerable.Create(data);
          context.Cache.Set(CacheKeys.Cards, dataWrapper);
        }
      }
    }

    public static void Render(IPaper paper, IContext context, Entity entity)
    {
      // Os dados foram renderizados anteriormente e estocados no cache
      var cards = context.Cache.Get<DataWrapperEnumerable>(CacheKeys.Cards);
      if (cards == null)
        return;

      entity.AddClass(Class.Cards);

      AddRowsAndLinks(paper, context, entity, cards);
    }

    private static void AddRowsAndLinks(IPaper paper, IContext context, Entity entity, DataWrapperEnumerable cards)
    {
      var keys = cards.EnumerateKeys().ToArray();
      foreach (DataWrapper card in cards)
      {
        var rowEntity = new Entity();
        rowEntity.AddRel(Rel.Card);

        foreach (var key in keys)
        {
          var value = card.GetValue(key);
          rowEntity.AddProperty(key, value);
        }

        var linkRenderers = paper._Call<IEnumerable<ILink>>("GetCardLinks", card.DataSource);
        if (linkRenderers != null)
        {
          foreach (var linkRenderer in linkRenderers)
          {
            var link = linkRenderer.RenderLink(context);
            if (link != null)
            {
              link.Rel?.Remove(RelNames.Link);
              var isSelf = (link.Rel?.Contains(RelNames.Self) == true);
              if (!isSelf)
              {
                link.AddRel(RelNames.RowLink);
              }
              rowEntity.AddLink(link);
            }
          }
        }

        entity.AddEntity(rowEntity);
      }
    }
    
  }
}