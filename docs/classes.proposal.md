Estrutura das Classes 
=====================

Glossário
---------

Tamanhos
:   -   xs - Extra Small
    -   sm - Small
    -   md - Medium
    -   lg - Large
    -   xl - Extra Large

Meta Class
:   Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.

    Como em: record

User Class
:   Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    da plataforma do Paper.

    Como em: MyApp.MyRecord


Estruturação de registros
-------------------------

Registro Simples

    {
      class [ "record" ]
      title ...
      properties {
        campo1 ...
        campo2 ...
        __headers {
          record [
            campo1
            campo2
          ]
        }
      }
      entities [
        {
          class [ "header" ]
          title "Campo 1"
          rel [ "record" ]
          properties {
            name "campo1"
            ...
          }
        }
        {
          class [ "header" ]
          title "Campo 2"
          rel [ "record" ]
          properties {
            name "campo2"
            ...
          }
        }
      ]
    }

Registros Referenciados

    {
      "entities": [
        {
          "class": [ "record" ]
          "href": ...
        }
        {
          "class": [ "record" ]
          "href": ...
        }
        ...
      ]
    }

Registros Embarcados

    {
      entities [
        {
          class [ "record" ]
          title ...
          properties {
            campo1 ...
            campo2 ...
            __headers {
              record [
                campo1
                campo2
              ]
            }
          }
          entities [
            {
              class [ "header" ]
              title "Campo 1"
              rel [ "record" ]
              properties {
                name: "campo1"
                ...
              }
            }
            {
              class [ "header" ]
              title "Campo 2"
              rel [ "record" ]
              properties {
                name "campo2"
                ...
              }
            }
          ]
        }
        ...
      ]
    }

Estruturação da Apresentação de Registros
-----------------------------------------

Apresentação Tabular  

-   Quando os registros associados à tabela têm o mesmo tipo.
-   É permitido que os registros matenham seus campos, mas são os campos de tabela aqueles usados na apresentação.
-   Se a entidade representa um registro este registro é apresentado como dados de cabeçalho da tabela.

Normas

-   Os registros da tabela devem ser da mesma classe.
-   Os registros devem se relacionar à tabela com o relacionamento "item".
-   Os registros devem ser embarcados em vez de referenciados.
-   Os campos são listados como cabeçalho da tabela.

    {
      class [ "table" ]
      title ...
      entities {
        {
          class [ "record" ]
          title ...
          rel [ "item" ]
          properties {
            campo1 ...
            campo2 ...
          }
          entities [
            {
              class [ "header" ]
              title "Campo 1"
              rel [ "record" ]
              properties {
                name: "campo1"
                ...
              }
            }
            {
              class [ "header" ]
              title "Campo 2"
              rel [ "record" ]
              properties {
                name "campo2"
                ...
              }
            }
          ]
        }
        {
          class [ "header" ]
          title "Campo 1"
          rel [ "item" ]
          properties {
            name "campo1"
            ...
          }
        }
        {
          class [ "header" ]
          title "Campo 2"
          rel [ "table" ]
          properties {
            name "campo2"
            ...
          }
        }
        ...
      }
    }
    
Estruturação da Apresentação de Listas
--------------------------------------

Apresentação de Lista

-   Em uma lista os registros podem variar em classe.
-   Cada classe define suas próprias propriedades de campos.
-   É permitido declarar uma coleção de campos padrão para ser aplicada aos registros que não
    declararem suas colunas.

Normas

-   A apresentação em lista usa como definição de campos os campos declarados em cada registro.

    {
      class [ "list", "record" ]
      title ...
      properties
        campo3 ...
        campo4 ...
        __headers {
          record [
            campo3
            campo4
          ]
          list [
            campo1
            campo2
          ]
        }
      entities {
        {
          class [ "record" ]
          title ...
          rel [ "list" ]
          properties {
            campo1 ...
            campo2 ...
          }
          entities [
            {
              class [ "header" ]
              title "Campo 1"
              rel [ "record" ]
              properties {
                name: "campo1"
                ...
              }
            }
            {
              class [ "header" ]
              title "Campo 2"
              rel [ "record" ]
              properties {
                name "campo2"
                ...
              }
            }
          ]
        }
        {
          class [ "header" ]
          title "Campo 1"
          rel [ "list" ]
          properties {
            name "campo1"
            ...
          }
        }
        {
          class [ "header" ]
          title "Campo 2"
          rel [ "list" ]
          properties {
            name "campo2"
            ...
          }
        }
        ...
      }
    }
