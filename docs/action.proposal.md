Action
======

Action é o procedimento de alteração de dados do Paper.


Formato da URI
--------------

A URI de ação é formada pela URI do recurso mais o nome da ação em PascalCase precedida por dois-pontos.  
Exemplo:

    http://host.com/My/Resource/:SaveMyResource


Estrutura da classe "form"
--------------------------

Os dados do formulário são dispostos na propriedade especial "__form":

    {
      "class": [ "form" ],
      "properties": {
        "campo1": "valor1",
        "campo2": "valor2"
      }
    }

Um form pode ser acompanhado de um ou vários registros afetados.
Neste caso a relação do registro com o "form" precisa ser explicitada.

    {
      "class": [ "form" ],
      "properties": {
        "campo1": "valor1",
        "campo2": "valor2"
      },
      "entities": [
        {
          "class": [ "record", "MyClass" ],
          "rel": [ "record" ],
          "properties": {
            "id": "1",
            "name": "Foo"
            "__headers": {
              "record": [ "id", "name" ]
            }
          }
        },
        {
          "class": [ "record", "MyClass" ],
          "rel": [ "record" ],
          "properties": {
            "id": "2",
            "name": "Bar"
            "__headers": {
              "record": [ "id", "name" ]
            }
          }
        }
      ]
    }
    

Estrutura do Payload
--------------------

Embora o formulário seja representado internamente pelo servidor como um estrutura de entidade da
classe "form" é esperado que o envio do formulário realizado pelo aplicativo cliente seja feito
pela estrutura Payload.

A estrutura Payload representa apenas os dados do formulário e os registros afetados.
O tipo de classe de cada registro pode ser indicado pela propriedade especiao "@class".

    {
      "form": { ... }
      "records": [
        {
          "@class": "ClassName",
          ...
        }
      ]
    }

    // Representação em "multipart/form-data"
    form.field=value
    records[index].@class=ClassName
    records[index].field=value

Por exemplo:

    {
      "form": {
        "field1": "value1",
        "field2": "value2"
      },
      "records": [
        {
          "@class": "MyClass",
          "id": "1",
          "name": "Foo"
        },
        {
          "@class": "MyClass",
          "id": "2",
          "name": "Bar"
        }
      ]
    }

    // Representação em "multipart/form-data"
    form.field1=value1
    form.field2=value2
    records[0].@class=MyClass
    records[0].id=1
    records[0].name=foo
    records[1].@class=MyClass
    records[1].id=2
    records[1].name=Bar


O Payload suporta uma sintaxe alternativa para o caso em que apenas um registro é afetado,
embora na tradução do Payload para Entity o registro é lançado na coleção de registros relacionados.

    {
      "form": { ... }
      "record": {
        "@class": "ClassName",
        ...
      }
    }

Por exemplo:

    {
      "form": {
        "field1": "value1",
        "field2": "value2"
      },
      "record": {
        "@class": "MyClass",
        "id": "1",
        "name": "Foo"
      }
    }

    // Representação em "multipart/form-data"
    form.field1=value1
    form.field2=value2
    record.@class=MyClass
    record.id=1
    record.name=foo


Mimetypes
---------

Mimetypes suportados no envio de dados:

-   text/json
-   text/xml
-   text/csv
-   application/json
-   application/xml
-   application/vnd.siren+json
-   application/vnd.siren+xml
-   application/x-www-form-urlencoded
-   multipart/form-data


Roteamento
----------

Action é identificada na rota na forma:

    /Rota/:Action

Como em:

    /User/1/:Edit

Uma requisição via GET produz como resposta uma requisição na URL base da ação.  
Por exemplo:

    GET /User/1/:Edit
    produz como resposta o mesmo que:
    GET /User/1

O agente da requisição pode usar a porção :Action para selecionar a ação específica dentro
da coleção de ações da entidade obtida.


Roteamento no PaperBot
----------------------

Para o roteamento interno do PaperBot é recomendado a implementação de dois métodos:

-   OpenRoute(route, entity)
    Para abertura da página.

-   OpenRoute(route)
    Para download da entidade refernete e posterior invocação de OpenRoute(route, entity).

OpenRoute deve interpretar a porção da ação na rota e carregar o form correspondente.

Por exemplo:

-   OpenRoute(provedor/userprovedor)
    Baixa e renderiza os dados do usuário

-   OpenRoute(provedor/userprovedor, { ... })
    Renderiza os dados do usuário indicado.

-   OpenRoute(provedor/user/:editprovedor)
    Baixa e renderiza o form de edição do usuário

-   OpenRoute(provedor/user/:editprovedor, { ... })
    Renderiza o form de edição do usuário indicado.

-   OpenRoute(provedor/users/:editprovedor, [{ ... }, { ... }, ...])
    Renderiza o form de edição dos usuários indicados.

A edição em lote geralmente depende dos itens selecionados previamente numa grade ou numa
lista. Neste caso é recomendável que o PaperBot guarde referência aos itens selecionados
em caso de reload da rota corrente ou que a action seja cancelada e o bot navegue de volta
para a página de seleção.

