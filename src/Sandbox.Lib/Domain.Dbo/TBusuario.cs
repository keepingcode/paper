using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolset;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;

namespace Sandbox.Lib.Domain.Dbo
{
  public class TBusuario
  {
    private static string[] _Get;

    public int DFid_usuario { get; set; }
    public string DFsenha { get; set; }
    public string DFnome_usuario { get; set; }
    public int DFnivel_usuario { get; set; }
    public int DFcod_empresa { get; set; }
    public int? DFid_pessoa { get; set; }
    public bool DFativo_inativo { get; set; }
    public int? DFid_centro_distribuicao { get; set; }
    public int? DFcod_setor_usuario { get; set; }
    public DateTime? DFdata_expiracao_senha { get; set; }
    public bool DFsenha_expira { get; set; }
    public string DFpapel { get; set; }
    public int? DFcod_papel { get; set; }

    public class Filter
    {
      public Var<int> DFid_usuario { get; set; }
      public VarString DFnome_usuario { get; set; }
      public Var<int> DFnivel_usuario { get; set; }
      public Var<int> DFcod_empresa { get; set; }
      public Var<bool> DFativo_inativo { get; set; }

      public Var<int> DFrow_number { get; set; }
    }

    public static Ret<TBusuario> Find(int DFid_usuario)
    {
      try
      {
        using (var db = new Db())
        {
          var usuario =
            @"select *
              from TBusuario
             where DFid_usuario matches @DFid_usuario"
              .AsSql()
              .Set("DFid_usuario", DFid_usuario)
              .SelectOneGraph<TBusuario>();
          return usuario;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret<TBusuario[]> Find(Filter filter = null)
    {
      try
      {
        using (var db = new Db())
        {
          var usuarios =
              @"select *
                from (select *
                           , row_number() over (order by DFid_usuario asc) as DFrow_number
                        from TBusuario
                       where DFid_usuario matches if set @DFid_usuario
                         and DFnome_usuario matches if set @DFnome_usuario
                         and DFnivel_usuario matches if set @DFnivel_usuario
                         and DFcod_empresa matches if set @DFcod_empresa
                         and DFativo_inativo matches if set @DFativo_inativo
                     ) as T
               where DFrow_number matches if set @DFrow_number"
              .AsSql()
              .Set(
                "DFrow_number", filter?.DFrow_number,
                "DFid_usuario", filter?.DFid_usuario,
                "DFnome_usuario", filter?.DFnome_usuario,
                "DFnivel_usuario", filter?.DFnivel_usuario,
                "DFcod_empresa", filter?.DFcod_empresa,
                "DFativo_inativo", filter?.DFativo_inativo
              )
              .ApplyTemplate()
              .Echo()
              .SelectGraphArray<TBusuario>();
          return usuarios;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static Ret Update(TBusuario usuario, params string[] fields)
    {
      try
      {
        if (fields.Length == 0)
        {
          fields = usuario._GetMethodNames().ToArray();
        }

        using (var db = new Db())
        using (var tx = db.CreateTransactionScope())
        {
          var parameters = usuario._GetMap(fields);
          var assertions = string.Join(", ", parameters.Keys.Select(x => $"{x} = @{x}"));

          var done =
            @"update TBusuario
                 set @{assertions}
               where DFid_usuario matches @DFid_usuario
              ;
              select @@rowcount"
              .AsSql()
              .Set("DFid_usuario", usuario.DFid_usuario)
              .Set("assertions", assertions)
              .Set(parameters)
              .ApplyTemplate()
              .Echo()
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
  }
}