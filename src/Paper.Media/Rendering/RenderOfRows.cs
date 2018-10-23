﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Paper.Media.Utilities.Types;
using Paper.Media.Design;
using Paper.Media.Design.Papers;
using Paper.Media.Routing;
using Toolset;
using Toolset.Reflection;

namespace Paper.Media.Rendering
{
  static class RenderOfRows
  {
    /// <summary>
    /// Realiza a consulta de registros specificados no Paper e estoca os registros
    /// obtidos no cache indicado.
    /// </summary>
    /// <param name="paper">A instância de IPaper que contém as consultas a dados.</param>
    /// <param name="cache">O cache para estocagem dos registros consultados.</param>
    public static void CacheData(IPaper paper, IContext context)
    {
      if (paper._Has("GetRows"))
      {
        var data = paper._Call("GetRows");
        if (data != null)
        {
          var dataWrapper = DataWrapperEnumerable.Create(data);
          context.Cache.Set(CacheKeys.Rows, dataWrapper);
        }
      }
    }

    public static void Render(IPaper paper, IContext context, Entity entity)
    {
      // Os dados foram renderizados anteriormente e estocados no cache
      var rows = context.Cache.Get<DataWrapperEnumerable>(CacheKeys.Rows);
      if (rows == null)
        return;

      entity.AddClass(Class.Rows);

      AddRowsAndLinks(paper, entity, context, rows);
      AddRowHeaders(paper, entity, context, rows);
    }

    private static void AddRowsAndLinks(IPaper paper, Entity entity, IContext context, DataWrapperEnumerable rows)
    {
      var keys = rows.EnumerateKeys().ToArray();
      foreach (DataWrapper row in rows)
      {
        var rowEntity = new Entity();
        rowEntity.AddRel(Rel.Row);

        foreach (var key in keys)
        {
          var value = row.GetValue(key);
          rowEntity.AddProperty(key, value);
        }

        var linkRenderers = paper._Call<IEnumerable<ILink>>("GetRowLinks", row.DataSource);
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

    private static void AddRowHeaders(IPaper paper, Entity entity, IContext context, DataWrapperEnumerable rows)
    {
      // Adicionando os campos autodetectados
      //
      var keys = rows.EnumerateKeys().ToArray();
      foreach (var key in keys)
      {
        var header = rows.GetHeader(key);
        entity.AddRowHeader(header);
      }

      // Adicionando os campos personalizados
      //
      var headers = paper._Call<IEnumerable<HeaderInfo>>("GetRowHeaders", rows.DataSource);
      if (headers != null)
      {
        if (!(headers is IList))
        {
          headers = headers.ToArray();
        }

        entity.AddRowHeaders(headers);

        // Ocultando as colunas não personalizadas
        //
        entity.ForEachRowHeader((e, h) =>
        {
          h.Hidden = !headers.Any(x => x.Name.EqualsIgnoreCase(h.Name));
        });
      }

      // Adicionando informação de ordenação
      //
      var sort = paper._Get<Sort>("Sort");
      if (sort != null)
      {
        entity.ForEachRowHeader((e, h) =>
          AddRowHeaderSortInfo(paper, context, sort, e, h)
        );
      }
    }

    private static void AddRowHeaderSortInfo(
        IPaper paper
      , IContext context
      , Sort sort
      , Entity headerEntity
      , HeaderInfo headerInfo
      )
    {
      var isSortable = (sort.Contains(headerInfo.Name) == true);
      var field = sort.GetSortedField(headerInfo.Name);
      if (isSortable)
      {
        var order = field?.Order ?? SortOrder.Unordered;

        //
        // Registrando a ordenação atual do campo
        //
        headerInfo.Order = order;

        //
        // Criando um link para ordernar este o campo
        //

        // Se o campo estiver em ordem ascendente o link será descendente
        // senao, o link ser ascendente
        var isDescend = (order == SortOrder.Ascending);

        var fieldName = headerInfo.Name.ChangeCase(TextCase.CamelCase);
        var sortValue = isDescend ? $"{fieldName}:desc" : fieldName;
        var sortTitle = isDescend ? "Ordenar Decrescente" : "Ordenar Crescente";

        var route = new Route(context.RequestUri)
          .UnsetArgs("sort", "sort[]")
          .SetArg("sort[]", sortValue);
        
        headerEntity.AddLink(route, sortTitle, Rel.HeaderLink, Rel.PrimaryLink);
      }
    }
  }
}