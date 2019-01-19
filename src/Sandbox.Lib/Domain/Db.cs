using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolset;
using Toolset.Sequel;

namespace Sandbox.Lib.Domain
{
  public class Db : SequelScope
  {
    public const int Version = 1;

    private static string connectionString =
      "server=serversql2k8.processa.com;database=DBguga;uid=sl;pwd=123;";

    public Db()
      : base(connectionString)
    {
    }

    static Db()
    {
      using (var scope = new SequelScope(connectionString))
      {
        var version =
          @"select DFvalor from TBsis_config where DFchave = 'versao'"
            .AsSql()
            .TrySelectOne<int>();

        if (version == 1)
          return;

        using (var tx = scope.CreateTransactionScope())
        {
          //
          // DDL
          //

          @"create table TBsis_config (
              DFchave nvarchar(256) not null
                constraint pk_TBsis_config primary key clustered,
              DFvalor nvarchar(4000) null
            )
            ;
            insert into TBsis_config (DFchave, DFvalor) values ('versao', '1')
            ;
            create table TBgrupo_empresa (
              DFid_grupo_empresa int not null identity(1,1)
                constraint pk_TBgrupo_empresa_DFid_grupo_empresa primary key clustered,
              DFnome nvarchar(256) not null
            )
            ;
            create table TBempresa (
              DFid_empresa int not null
                constraint pk_TBempresa primary key clustered,
              DFrazao_social nvarchar(256) not null,
              DFnome_fantasia nvarchar(256) not null,
              DFcnpj nvarchar(18) not null,
              DFativo bit not null
                constraint df_TBempresa_DFativo default (1),
              DFid_grupo_empresa int null
                constraint fk_TBempresa_TBgrupo_empresa
                    foreign key references TBgrupo_empresa(DFid_grupo_empresa)
                  on delete set null
            )
            ;
            create index ix_TBempresa_DFid_grupo_empresa on TBempresa (DFid_grupo_empresa)
            ;
            create table TBpapel_usuario (
              DFid_papel_usuario int not null identity(1,1)
                constraint pk_TBpapel_usuario primary key clustered,
              DFnome nvarchar(256) not null
            )
            ;
            create table TBusuario (
              DFid_usuario int not null identity(1,1)
                constraint pk_TBusuario primary key clustered,
              DFlogin nvarchar(100) not null
                constraint uk_TBusuario_DFlogin unique,
              DFnome nvarchar(100) not null,
              DFativo bit not null
                constraint df_TBusuario_DFativo default (1),
              DFsenha nvarchar(12) not null,
              DFid_empresa_padrao int null
                constraint fk_TBusuario_TBempresa
                    foreign key references TBempresa(DFid_empresa)
                  on delete set null,
              DFid_papel_usuario int null
                constraint fk_TBusuario_TBpapel_usuario
                    foreign key references TBpapel_usuario(DFid_papel_usuario)
                  on delete set null
            )
            ;
            create index ix_TBusuario_DFid_empresa_padrao on TBusuario (DFid_empresa_padrao)
            ;
            create index ix_TBusuario_DFid_papel_usuario  on TBusuario (DFid_papel_usuario)
            ;
            create table TBpostagem (
              DFid_postagem int not null identity(1,1)
                constraint pk_TBpostagem primary key clustered,
              DFid_autor int not null
                constraint fk_TBpostagem_TBusuario
                    foreign key references TBusuario(DFid_usuario)
                on delete cascade,
              DFtitulo nvarchar(100) not null,
              DFtexto nvarchar(max) not null,
              DFdata_publicacao datetime not null
                constraint df_TBpostagem_DFdata_publicacao default (getdate()),
              DFid_postagem_referente int null
                constraint fk_TBpostagem_TBpostagem
                    foreign key references TBpostagem(DFid_postagem),
              DFtipo as (case when coalesce(DFid_postagem_referente, 0) = 0 then 'P' else 'C' end)
            )
            ;
            create index ix_TBpostagem_DFid_autor on TBpostagem(DFid_autor)
            ;
            create index ix_TBpostagem_DFdata_publicacao on TBpostagem(DFdata_publicacao desc)
            ;
            create index ix_TBpostagem_DFid_postagem_referente on TBpostagem(DFid_postagem_referente)
            ;
            create index ix_TBpostagem_DFtipo on TBpostagem(DFtipo)
            ;
            create table TBclassificacao (
              DFid_classificacao int not null identity(1,1)
                constraint pk_TBclassificacao primary key clustered,
              DFid_autor int not null
                constraint fk_TBclassificacao_TBusuario
                    foreign key references TBusuario (DFid_usuario),
              DFid_postagem_referente int not null
                constraint fk_TBclassificacao_TBpostagem
                    foreign key references TBpostagem (DFid_postagem)
                  on delete cascade,
              DFclassificacao int not null
                constraint ck_TBclassificacao_DFclassificacao
                      check (DFclassificacao = -1 or DFclassificacao = 1)
            )"
            .AsSql()
            .Execute();

          //
          // DEMONSTRACAO
          //

          @"insert into TBgrupo_empresa (DFnome) values ('GRUPO SEM FRONTEIRAS')
            declare @id_grupo_empresa int = scope_identity()
            
            insert into TBempresa (DFid_empresa, DFrazao_social, DFnome_fantasia, DFcnpj, DFativo, DFid_grupo_empresa)
            values (1, 'SEM FRONTEIRAS VAREJISTA', 'SUPERMERCADO SEM FRONTEIRAS', '01.234.567/0001-95', 1, @id_grupo_empresa)
                 , (2, 'SEM FRONTEIRAS ATACADISTA', 'CENTRAL SEM FRONTEIRAS', '01.234.567/0002-76', 1, @id_grupo_empresa)
            
            insert into TBpapel_usuario (DFnome) values ('ADMIN')
            declare @id_papel_admin int = scope_identity()
            
            insert into TBpapel_usuario (DFnome) values ('USUARIO')
            declare @id_papel_usuario int = scope_identity()
            
            insert into TBusuario (DFlogin, DFnome, DFativo, DFsenha, DFid_empresa_padrao, DFid_papel_usuario)
            values ('admin', 'ADMINISTRADOR', 1, 'qwer0987', 1, @id_papel_admin)
                 , ('fulano', 'FULANO', 1, '123', 1, @id_papel_usuario)
                 , ('beltrano', 'BELTRANO', 1, '123', 1, @id_papel_usuario)
                 , ('cicrano', 'CICRANO', 1, '123', 1, @id_papel_usuario)
                 , ('alano', 'ALANO', 2, '123', 1, @id_papel_usuario)
                 , ('mengano', 'MENGANO', 2, '123', 1, @id_papel_usuario)
                 , ('zutano', 'ZUTANO', 2, '123', 1, @id_papel_usuario)
                 , ('citano', 'CITANO', 2, '123', 1, @id_papel_usuario)
                 , ('perengano', 'PERENGANO', 2, '123', 1, @id_papel_usuario)"
            .AsSql()
            .Execute();

          tx.Complete();
        }
      }
    }
  }
}