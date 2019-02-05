Action
======

Action é o procedimento de alteração de dados do Paper.

Um Action contém:

1.  Um form para a coleta de dados;
2.  O algoritmo de envio dos dados coletados para um rota via POST.


Formato dos Dados: Edição Mista
-------------------------------

Na edição mista o provedor mescla a entidade afetada com campos de edição e trata os campos como campos de edição.

Neste formato temos:

-   Campos de edição estruturados com a classe: form

Exemplo:

    // entity
    {
      class: [ "form" ],
      properties: {
        form: {
          formField: value,
          ...
        }
      }
    }

    // payload
    {
      form: {
        formField: value,
        ...
      }
    }

    // form-data
    form[formField]=value
    ...


Formato dos Dados: Edição em Linha
----------------------------------

Na edição em linha o provedor evidencia a entidade afetada e os campos de edição.

Neste formato temos:

-   Campos de edição estruturados com a classe: form
-   Campos da entidade estruturados com a classe: data

Exemplo:

    // entity
    {
      class: [ form, data, Class ],
      properties: {
        dataField: value,
        ...
        form: {
          formField: value,
          ...
        }
      }
    }

    // payload
    {
      form: {
        formField: value,
        ...
      },
      data: {
        @class: Class,
        dataField: value,
        ...
      }
    }

    // form-data
    form[formField]=value
    ...
    data[@class]=Class
    data[dataField]=value
    ...

Formato dos Dados: Edição em Lote
---------------------------------

Na edição em lote o provedor evidencia as entidades afetadas e os campos de edição.
Essa forma de edição permite a aplicação de uma mesma alteração às várias entidades afetadas.

Neste formato temos:

-   Campos de edição estruturados com a classe: form
-   Campos das entidades estruturados com a classe: rows

Exemplo:

    // entity
    {
      class: [ form, rows ],
      properties: {
        form: {
          formField: value,
          ...
        }
      },
      entities: [
        {
          class: [ data, Class ]
          properties: {
            dataField: value,
            ...,
          }
        }
      ]
    }

    // payload
    {
      form: {
        formField: value,
        ...
      },
      rows: [
        {
          @class: Class,
          dataField: value,
          ...
        }
      ]
    }

    // form-data
    form[formField]=value
    ...
    rows[0][@class]=Class
    rows[0][dataField]=value
    ...
    
Formato dos Dados: Edição em Linha Alternativa
----------------------------------------------

Na edição em linha alternativa o provedir mescla a entidade afetada com os campos de edição e trata os campos como campos de dados.

Neste formato temos:

-   Campos de dados estruturados com a classe: data

Exemplo:

    // entity
    {
      class: [ data, Class ],
      properties: {
        dataField: value,
        ...
      }
    }

    // payload
    {
      data: {
        @class: Class,
        dataField: value,
        ...
      }
    }

    // form-data
    data[@class]=Class
    data[dataField]=value
    ...
    
Formato dos Dados: Edição em Bloco
----------------------------------

Na edição em bloco o provedor mescla as entidades afetadas com os campos de edição.
Essa forma de edição permite a aplicação de alterações diferentes para cada entidade afetada.

Neste formato temos:

-   Campos de dados estruturados com a classe: rows

Exemplo:

    // entity
    {
      class: [ rows, Class ],
      entities: [
        {
          class: [ data ]
          properties: {
            dataOrFormField: value,
            ...
          }
        }
      ]
    }

    // payload
    {
      rows: [
        {
          @class: Class,
          dataField: value,
          ...
        }
      ]
    }

    // form-data
    rows[0][@class]=value
    rows[0][dataField]=value
    ...



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

(OBSOLETO: NAO SERA DESSA FORMA)

Action é identificada na rota na forma:

    /rota/:action

Como em:

    /user/:edit

Um GET na rota da ação produz como resposta a própria entidade relacionada.
Um POST na rota da ação produz a execução da ação correspondente.

Portanto:

-   provedorGET /user/:editprovedor se torna provedorGET /userprovedor
-   provedorPOST /user/:editprovedorprovedor executa a ação de edição do usuário.


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

