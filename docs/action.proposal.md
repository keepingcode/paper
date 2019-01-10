Action
======

Action é o procedimento de alteração de dados do Paper.

Um Action contém:

1.  Um form para a coleta de dados;
2.  O algoritmo de envio dos dados coletados para um rota via POST.

Os dados enviados contém duas partes:

1.  Os dados coletados do form;
2.  A entidade afetada pela action.

Exemplo dos dados envidados para o servidor via POST:

    {
      "data": {
        "active": true
      },
      "target": {
        "login": "fulano"
        ...
      }
    }


Edição em bloco
---------------

Em uma lista uma action é acionada para vários itens selecionados da seguinte forma:

1.  Primeiro seleciona-se vários itens na lista.
    -   As actions disponíveis são exibidas na barra de topo do app.
2.  Em seguida aciona-se a ação desejada na barra de topo.
3.  O form da action é exibido para coleta de dados.
4.  Os dados coletados no form mais as entidades afetadas são enviadas para o servidor via POST.

Exemplo dos dados envidados para o servidor via POST:

    {
      "data": {
        "active": true
      },
      "target": [
        {
          "login": "fulano"
          ...
        },
        {
          "login": "beltrano"
          ...
        },
        ...
      ]
    }


Roteamento
----------

Action é identificada na rota na forma:

    /rota/:action

Como em:

    /user/:edit

Um GET na rota da ação produz como resposta a própria entidade relacionada.
Um POST na rota da ação produz a execução da ação correspondente.

Portanto:

-   "GET /user/:edit" se torna "GET /user"
-   "POST /user/:edit"" executa a ação de edição do usuário.

Roteamento no PaperBot
----------------------

Para o roteamento interno do PaperBot é recomendado a implementação de dois métodos:

-   OpenRoute(route, entity)
    Para abertura da página.

-   OpenRoute(route)
    Para download da entidade refernete e posterior invocação de OpenRoute(route, entity).

OpenRoute deve interpretar a porção da ação na rota e carregar o form correspondente.

Por exemplo:

-   OpenRoute("/user")
    Baixa e renderiza os dados do usuário

-   OpenRoute("/user", { ... })
    Renderiza os dados do usuário indicado.

-   OpenRoute("/user/:edit")
    Baixa e renderiza o form de edição do usuário

-   OpenRoute("/user/:edit", { ... })
    Renderiza o form de edição do usuário indicado.

-   OpenRoute("/users/:edit", [{ ... }, { ... }, ...])
    Renderiza o form de edição dos usuários indicados.

A edição em lote geralmente depende dos itens selecionados previamente numa grade ou numa
lista. Neste caso é recomendável que o PaperBot guarde referência aos itens selecionados
em caso de reload da rota corrente ou que a action seja cancelada e o bot navegue de volta
para a página de seleção.

