using System;
using System.Collections.Generic;
using System.Text;
using Toolset;

namespace Paper.Media
{
  public static class ClassExtensions
  {
    /// <summary>
    /// Obtém o nome padronizado da classe.
    /// O nome obtido equivale àquele declarado nas constantes de
    /// ClassNames.
    /// </summary>
    /// <param name="clazz">O nome da classe.</param>
    /// <returns>O nome padronizado da classe.</returns>
    public static string GetName(this Class clazz)
    {
      return clazz.ToString().ChangeCase(TextCase.CamelCase);
    }

    /// <summary>
    /// Determina se a coleção de nomes contém classes de metadados.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <param name="media">O objeto de media alvo.</param>
    /// <returns>Verdadeiro se a coleção de nomes contém uma classe de metadado.</returns>
    public static bool HasMetaClass<TMediaObject>(this TMediaObject media)
      where TMediaObject : IMediaObject
    {
      return ClassNames.HasMetaClass(media.Class);
    }

    /// <summary>
    /// Determina se a coleção de nomes contém classes de usuário.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <param name="media">O objeto de media alvo.</param>
    /// <returns>Verdadeiro se a coleção de nomes contém uma classe de metadado.</returns>
    public static bool HasUserClass<TMediaObject>(this TMediaObject media)
      where TMediaObject : IMediaObject
    {
      return ClassNames.HasUserClass(media.Class);
    }

    /// <summary>
    /// Determina se a classe representa uma classe de metadado.
    /// 
    /// Uma classe de metadado segue a convenção camelCase e possui função interna à plataforma do Paper.
    /// </summary>
    /// <param name="media">O objeto de media alvo.</param>
    /// <returns>Verdadeiro se o tipo corresponde a uma classe de metadado; Falso caso contrário.</returns>
    public static IEnumerable<string> GetMetaClass<TMediaObject>(this TMediaObject media)
      where TMediaObject : IMediaObject
    {
      return ClassNames.GetMetaClass(media.Class);
    }

    /// <summary>
    /// Determina se a classe representa uma classe de usuário.
    /// 
    /// Uma classe de usuário segue a convenção PascalCase e possui função apenas para o aplicativo usuário
    /// da plataforma do Paper.
    /// </summary>
    /// <param name="media">O objeto de media alvo.</param>
    /// <returns>Verdadeiro se o tipo corresponde a uma classe de usuario; Falso caso contrário.</returns>
    public static IEnumerable<string> GetUserClass<TMediaObject>(this TMediaObject media)
      where TMediaObject : IMediaObject
    {
      return ClassNames.GetUserClass(media.Class);
    }
  }
}