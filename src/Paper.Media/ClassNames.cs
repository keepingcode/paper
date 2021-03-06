using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.CodeDom;

namespace Paper.Media
{
  /// <summary>
  /// Classes conhecidas de entidades.
  /// </summary>
  public static class ClassNames
  {
    #region Estruturais básicas

    /// <summary>
    /// Nome de classe que representa um registro.
    /// </summary>
    public const string Record = "record";

    /// <summary>
    /// Nome de classe que representa dados de um formulário de edição.
    /// </summary>
    public const string Form = "form";

    /// <summary>
    /// Nome de classe para ume entidade que se comporta como um cabeçalho.
    /// </summary>
    public const string Header = "header";

    /// <summary>
    /// Nome de classe para uma entidade que se comporta como status de execução.
    /// </summary>
    public const string Status = "status";

    /// <summary>
    /// Nome de classe para uma entidade que se comporta como erro.
    /// </summary>
    public const string Error = "error";

    /// <summary>
    /// Nome de classe para uma ação que se comporta como filtro de lista.
    /// </summary>
    public const string Filter = "filter";

    /// <summary>
    /// Nome de classe para uma ação, entidade ou link que se comporta como um hiperlink.
    /// </summary>
    public const string Hyperlink = "hyperlink";

    /// <summary>
    /// Nome de classe para uma entidade que transporta um valor literal apenas, como um 
    /// texto, número, etc.
    /// </summary>
    public const string Literal = "literal";

    /// <summary>
    /// Classe de uma entidade que representa um propriedade ou coluna de dados.
    /// </summary>
    public const string Field = "field";

    #endregion

    #region Estruturais avançadas

    /// <summary>
    /// Nome de classe para ume entidade que representa a configuração de um site.
    /// </summary>
    public const string Blueprint = "blueprint";

    /// <summary>
    /// Nome de classe para ume entidade que se comporta como uma coleção de registros.
    /// </summary>
    public const string Table = "table";

    /// <summary>
    /// Nome de classe para uma entidade que se comporta como lista.
    /// </summary>
    public const string List = "list";

    /// <summary>
    /// Nome de classe para uma entidade que representa um item de lista.
    /// </summary>
    public const string Item = "item";

    /// <summary>
    /// Nome de classe para ume entidade que se comporta como uma coleção de cards.
    /// </summary>
    public const string Cards = "cards";

    /// <summary>
    /// Nome de classe para ume entidade que se comporta como um registro de coleção de cards.
    /// </summary>
    public const string Card = "card";

    #endregion

    #region Extensões

    #endregion

    #region Métodos

    /// <summary>
    /// Determina o nome de classe da entidade.
    /// </summary>
    /// <param name="typeOrInstance">O tipo ou a instância da entidade.</param>
    /// <returns>O nome de classe mais apropriado.</returns>
    public static string GetClassName(object typeOrInstance)
    {
      // O algoritmo é o mesmo aplicado ao tipo de dados.
      return DataTypeNames.FromType(typeOrInstance);
    }

    /// <summary>
    /// Determina se a classe representa uma classe de metadado.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <param name="class">A classe verificada.</param>
    /// <returns>Verdadeiro se o tipo corresponde a uma classe de metadado; Falso caso contrário.</returns>
    public static bool IsMetaClass(string @class)
    {
      return char.IsLower(@class.FirstOrDefault());
    }

    /// <summary>
    /// Determina se a classe representa uma classe de usuário.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <param name="class">A classe verificada.</param>
    /// <returns>Verdadeiro se o tipo corresponde a uma classe de usuario; Falso caso contrário.</returns>
    public static bool IsUserClass(string @class)
    {
      return !char.IsLower(@class.FirstOrDefault());
    }

    /// <summary>
    /// Determina se a coleção de nomes contém classes de metadados.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <param name="classes">A classe verificada.</param>
    /// <returns>Verdadeiro se a coleção de nomes contém uma classe de metadado.</returns>
    public static bool HasMetaClass(NameCollection classes)
    {
      return classes?.Any(IsMetaClass) == true;
    }

    /// <summary>
    /// Determina se a coleção de nomes contém classes de usuário.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <param name="classes">A classe verificada.</param>
    /// <returns>Verdadeiro se a coleção de nomes contém uma classe de metadado.</returns>
    public static bool HasUserClass(NameCollection classes)
    {
      return classes?.Any(IsUserClass) == true;
    }

    /// <summary>
    /// Determina se a classe representa uma classe de metadado.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <param name="classes">A classe verificada.</param>
    /// <returns>Verdadeiro se o tipo corresponde a uma classe de metadado; Falso caso contrário.</returns>
    public static IEnumerable<string> GetMetaClass(NameCollection classes)
    {
      return classes?.Where(IsMetaClass) ?? Enumerable.Empty<string>();
    }

    /// <summary>
    /// Determina se a classe representa uma classe de usuário.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <param name="classes">A classe verificada.</param>
    /// <returns>Verdadeiro se o tipo corresponde a uma classe de usuario; Falso caso contrário.</returns>
    public static IEnumerable<string> GetUserClass(NameCollection classes)
    {
      return classes?.Where(IsUserClass) ?? Enumerable.Empty<string>();
    }

    #endregion
  }
}