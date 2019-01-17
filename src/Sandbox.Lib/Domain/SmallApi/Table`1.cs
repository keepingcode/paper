using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolset;
using Toolset.Collections;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;

namespace Sandbox.Lib.Domain.SmallApi
{
  public abstract class Table<TEntity, TPk, TFilter> : Table
    where TEntity : class, new()
    where TFilter : class, new()
  {
    public Type PkType => typeof(TPk);
    public Type FilterType => typeof(TFilter);

    static Table()
    {
      TableInfo = ExtractTableInfo<TEntity>();
      FilterInfo = ExtractFilterInfo<TFilter>();
    }

    public static TableInfo TableInfo { get; }
    public static FilterInfo FilterInfo { get; }

    private static TableInfo ExtractTableInfo<T>()
    {
      var type = typeof(T);

      var attribute = type.GetCustomAttributes(false).OfType<TableAttribute>().SingleOrDefault();
      var tableName = (attribute?.Schema != null)
        ? $"{attribute.Schema}.{attribute?.Name ?? type.Name}"
        : attribute?.Name ?? type.Name;

      var pkInfo = (
        from property in type.GetProperties()
        from attr in property.GetCustomAttributes(true).OfType<PkAttribute>()
        select new
        {
          property.Name,
          attr.IsAutoIncrement
        }
      ).FirstOrDefault();

      var fieldNames =
        from property in type.GetProperties()
        let attributes = property.GetCustomAttributes(true)
        where !attributes.OfType<IgnoreAttribute>().Any()
        select property.Name;

      return new TableInfo
      {
        TableName = tableName,
        PkName = pkInfo.Name,
        PkAutoIncrement = pkInfo.IsAutoIncrement,
        FieldNames = fieldNames.ToArray()
      };
    }

    private static FilterInfo ExtractFilterInfo<T>()
    {
      var type = typeof(T);

      var rowNumberName = (
        from property in type.GetProperties()
        from attribute in property.GetCustomAttributes(true).OfType<RowNumberAttribute>()
        select property.Name
      ).FirstOrDefault();

      var fieldNames =
        from property in type.GetProperties()
        let attributes = property.GetCustomAttributes(true)
        where !attributes.OfType<IgnoreAttribute>().Any()
        select property.Name;

      return new FilterInfo
      {
        RowNumberName = rowNumberName,
        FieldNames = fieldNames.ToArray()
      };
    }

    public static Ret<TEntity> Find(TPk id)
    {
      try
      {
        using (var scope = new SequelScope())
        {
          var entity =
            @"select *
                from @{p_table} with (nolock)
               where @{p_pk} matches @p_id"
              .AsSql()
              .Set("p_table", TableInfo.TableName)
              .Set("p_pk", TableInfo.PkName)
              .Set("p_id", id)
              .SelectOneGraph<TEntity>();
          return entity;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret<TEntity[]> Find(TFilter filter = null)
    {
      try
      {
        using (var scope = new SequelScope())
        {
          var parameters = filter._GetMap(FilterInfo.FieldNames);

          var comparisons = string.Join(" and ",
            FilterInfo.FieldNames
              .Except(FilterInfo.RowNumberName)
              .Select(x => $"{x} matches if set @{x}")
          );

          var rowNumber = (FilterInfo.RowNumberName != null)
            ? filter._Get(FilterInfo.RowNumberName)
            : null;

          var entities =
            @"select *
                from (select *
                           , row_number() over (order by @{p_pk} asc) as row_number
                        from @{p_table} with (nolock)
                       where @{p_comparisons}
                     ) as T
               where row_number matches if set @p_row_number"
              .AsSql()
              .Set("p_table", TableInfo.TableName)
              .Set("p_pk", TableInfo.PkName)
              .Set("p_comparisons", comparisons)
              .Set("p_row_number", rowNumber)
              .Set(parameters)
              .SelectGraphArray<TEntity>();
          return entities;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret Insert(TEntity entity)
    {
      try
      {
        using (var scope = new SequelScope())
        using (var tx = scope.CreateTransactionScope())
        {
          var updatableFields = TableInfo.PkAutoIncrement
            ? TableInfo.FieldNames.Except(TableInfo.PkName).ToArray()
            : TableInfo.FieldNames.ToArray();
          
          var parameters = entity._GetMap(updatableFields);
          var fields = string.Join(", ", parameters.Keys);
          var values = parameters.Values;

          var done =
            @"insert into @{p_table} (@{p_fields}) values (@{p_values})
              ;
              select @@rowcount"
              .AsSql()
              .Set("p_table", TableInfo.TableName)
              .Set("p_pk", TableInfo.PkName)
              .Set("p_fields", fields)
              .Set("p_values", values)
              .Set(parameters)
              .SelectOne<bool>();

          tx.Complete();
          return done;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret InsertOrUpdate(TEntity entity)
    {
      try
      {
        using (var scope = new SequelScope())
        using (var tx = scope.CreateTransactionScope())
        {
          var updatableFields = TableInfo.PkAutoIncrement
            ? TableInfo.FieldNames.Except(TableInfo.PkName).ToArray()
            : TableInfo.FieldNames.ToArray();

          var parameters = entity._GetMap(updatableFields);
          var assertions = string.Join(", ", parameters.Keys.Select(x => $"{x} = @{x}"));
          var fields = string.Join(", ", parameters.Keys);
          var values = parameters.Values;
          var id = entity._Get(TableInfo.PkName);

          var affectedId =
            @"declare @x_affected_id int = null
              ;
              update @{p_table}
                 set @{p_assertions}
               where @{p_pk} matches @p_id
              ;
              if @@rowcount > 0
              begin
                set @x_affected_id = @p_id
              end
              ;
              insert into @{p_table} (@{p_fields})
              select @{p_values}
               where not exists (select 1 from @{p_table} where @{p_pk} matches @p_id)
              ;
              if @@rowcount > 0
              begin
                set @x_affected_id = scope_identity()
              end
              ;
              select @x_affected_id"
              .AsSql()
              .Set("p_table", TableInfo.TableName)
              .Set("p_pk", TableInfo.PkName)
              .Set("p_assertions", assertions)
              .Set("p_fields", fields)
              .Set("p_values", values)
              .Set("p_id", id)
              .Set(parameters)
              .SelectOne();

          tx.Complete();

          var done = affectedId != null;
          if (done)
          {
            entity._Set(TableInfo.PkName, affectedId);
          }

          return done;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret Update(TEntity entity, params string[] fields)
    {
      try
      {
        using (var scope = new SequelScope())
        using (var tx = scope.CreateTransactionScope())
        {
          if (fields.Length == 0)
          {
            fields = TableInfo.FieldNames.Except(TableInfo.PkName).ToArray();
          }

          var parameters = entity._GetMap(fields);
          var assertions = string.Join(", ", parameters.Keys.Select(x => $"{x} = @{x}"));

          var id = entity._Get(TableInfo.PkName);

          var done =
            @"update @{TableName}
                 set @{assertions}
               where @{PkName} matches @id
              ;
              select @@rowcount"
              .AsSql()
              .Set("TableName", TableInfo.TableName)
              .Set("PkName", TableInfo.PkName)
              .Set("assertions", assertions)
              .Set("id", id)
              .Set(parameters)
              .SelectOne<bool>();

          tx.Complete();
          return done;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret Delete(TEntity entity)
    {
      try
      {
        var id = entity._Get<TPk>(TableInfo.PkName);
        return Delete(TableInfo, id, null);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret Delete(TPk id)
    {
      try
      {
        return Delete(TableInfo, id, null);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret Delete(TFilter filter)
    {
      try
      {
        return Delete(TableInfo, default(TPk), filter);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static Ret Delete(TableInfo tableInfo, TPk id, TFilter filter)
    {
      using (var scope = new SequelScope())
      using (var tx = scope.CreateTransactionScope())
      {
        var idValue = default(TPk).Equals(id) ? null : (object)id;

        var parameters = filter?._GetMap(FilterInfo.FieldNames);

        var comparisons = string.Join(" and ",
          FilterInfo.FieldNames
            .Except(FilterInfo.RowNumberName)
            .Select(x => $"{x} matches if set @{x}")
        );

        var rowNumber = (FilterInfo.RowNumberName != null)
          ? filter._Get(FilterInfo.RowNumberName)
          : null;

        var done =
          @"delete from @{p_table} where @{p_pk} in (
              select @{p_pk}
                from (select @{p_pk}
                           , row_number() over (order by @{p_pk} asc) as row_number
                        from @{p_table}
                       where (@p_id is set and @{p_pk} matches @p_id)
                          or (@p_id is not set and @{p_comparisons})
                     ) as T
               where row_number matches if set @p_row_number
            )
            ;
            select @@rowcount
            "
            .AsSql()
            .Set("p_table", tableInfo.TableName)
            .Set("p_pk", tableInfo.PkName)
            .Set("p_comparisons", comparisons)
            .Set("p_row_number", rowNumber)
            .Set("p_id", idValue)
            .Set(parameters)
            .ApplyTemplate()
            .Echo()
            .SelectOne<bool>();
        
        tx.Complete();
        return done;
      }
    }

    public Ret Insert()
    {
      return Insert((TEntity)(object)this);
    }

    public Ret InsertOrUpdate()
    {
      return InsertOrUpdate((TEntity)(object)this);
    }

    public Ret Update(params string[] fields)
    {
      return Update((TEntity)(object)this, fields);
    }

    public Ret Delete()
    {
      return Delete((TEntity)(object)this);
    }

    public override string ToString()
    {
      try
      {
        var id = this._Get(TableInfo.PkName);
        return $"{TableInfo.TableName}.{TableInfo.PkName}={id}";
      }
      catch
      {
        return base.ToString();
      }
    }
  }
}