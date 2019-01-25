# Paper Framework

Paper Framework é uma plataforma de softwares para automação da interação entre
um usuário humano e um sistema.

A plataforma contém três partes:

1.  Paper Browser.
    Uma implementação de aplicativo cliente para uma plataforma qualquer.
2.  Paper Bookshelf.
    O servidor do Paper para publicação das páginas interativas.
3.  Paper Provider API.
    Uma biblioteca de funções para integração dos sistemas interessados em 
    publicar páginas interativas no Paper Framework.

Neste documento estão descritos os conceitos e as regras de implementação destas
três partes.


# Glossário

## Entities

Um Entity é um objeto de hipermídia para transporte de dados e metadados entre
as partes da plataforma.

Estrutura geral de um Entity:

    {
      class: [ ... ] ,
      rel: [ ... ],
      title: ... ,
      properties: [ ... ] ,
      entities: [ ... ] ,
      actions: [ ... ] ,
      links: [ ... ]
    }

O campo *class* é uma lista de nomes das classes representadas pelo Entity.
É esta classe que determina como as partes da plataforma interpretam o conteúdo
do Entity.

O campo *rel* é uma lista de nomes dos relacionamentos entre a entidade e outros
objetos que compartilham um contexto.

O campo *title* é um nome amigável de identificação do Entity.

O campo *properties* é uma lista das propriedades do Entity.

O campo *entities* é uma lista de instâncias de Entity vinculados ao Entity.
Um Entity nesta lista pode representar uma instância de Entity propriamente dita
ou uma URI de referência a partir da qual uma instância do Entity pode ser
obtida.

O campo *actions* é uma lista de ações que se pode exercer em cima do Entity.
Um action descreve os parâmetros de execução da ação e os campos de edição com
suas regras de comportamento.

O campo *links* é uma lista de URIs de referência para descoberta dos recursos
relacionados ao Entity.

## Papers

Um Paper é um objeto que descreve as propriedades e os comportamentos de uma
página interativa.

O Paper Browser utiliza as informações obtidas do Paper para determinar a melhor
forma de apresentar os dados da página e interagir com o usuário do sistema.

O Paper é trafegado entre as partes da plataforma em uma instância de Entity sob
a classe "paper".

Exemplo:

    {
      class: [ "paper", ... ] ,
      ...
    }

## Bookshelf

Um Bookshelf é um índice de rotas de acesso aos Papers disponíveis no sistema.
O Paper Bookshelf expõe o Bookshelf em uma URI de referência usada pelo Paper
Browser para navegação entre as instâncias conhecidas de Papers.

## Catalogs

Um Catalog é um objeto contendo um mapeamento de rotas para instâncias de Paper.

Quando um Catalog é adicionado a um Bookshelf todas as rotas mapeadas nele são
publicadas no índice de navegação do Bookshelf.
Quando um Catalog é removido de um Bookshelf todas as rotas mapeadas por ele são
removidas do índice de navegação do Bookshelf.

Os serviços interessados em publicar páginas interativas na plataforma devem
registrar suas instâncias de Catalog por meio de um dos métodos de registro
disponíveis na API de utilização.

O Bookshelf checa periodicamente os serviços vinculados às instâncias de Catalog
para determinar quais estão realmente online,
e portanto, navegáveis.

A API de utilização mantém uma checagem periódica do registro da instância de
Catalog no Bookshelf para garantir que o serviço
está sempre accessivel.

## Pipelines

Um Pipeline é um objeto que implementa uma regra de rederização de Papers.

Durante uma requisição o Paper Bookshelf identifica uma rota de renderização de
Paper e invoca uma instância de Pipeline capaz
de renderizar o Paper.

O algoritmo de renderização do Pipeline pode ainda produzir como resposta uma
URI para redirecionamento da requisição ou um onteúdo binário qualquer para
download do cliente, como um PDF, uma imagem, um HTML.

É comum a identificação de vários Pipelines capazes de renderizar uma rota.
Estes Pipelines são encadeados em ordem de prioridade e executados em sequência
até que um deles produza uma resposta.

A prioridade do Pipeline é definida pelo tamanho da rota a qual ele atende.
Quanto maior o tamanho da rota maior a sua prioridade.

Por exemplo, o Pipeline que atende a rota `/Api/1/Users/1` tem prioridade sobre
um Pipeline que atende à rota `/Api/1`, que por sua vez tem prioridade sobre o
Pipeline que atende a rota `/`.

## Paper Browser

Uma implementação de aplicativo para navegação entre as páginas interativas.

Existem diferentes implementações do Paper Browser para atender as diferentes
plataformas, dispositivos
e tecnologia.

As implementações suportam um subset básico de recursos do Paper Framework mais
um subset particular de funcionalidades suportadas pelo seu ambiente alvo.

## Paper Bookshelf

Um serviço de publicação das páginas interativas usadas pelas instâncias de
Paper Browser para navegação.

## Paper Provider

Um aplicativo qualquer, externo ao Paper Framework, interessado em publicar
páginas interativas no Paper Framework.

O aplicativo monta suas instâncias de Catalog contendo as rotas para os Papers
publicados por ele e as registra no Paper Bookshelf utilizando o
Paper Provider API.

## Paper Provider API

Uma biblioteca de funções do Paper Framework para uso de aplicativos externos
interessados em publicar páginas interativas na plataforma.




