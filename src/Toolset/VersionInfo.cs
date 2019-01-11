using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Toolset
{
  /// <summary>
  /// Informação de versão de software.
  /// </summary>
  public class VersionInfo
  {
    /// <summary>
    /// O primeiro número da versão.
    /// -   X.*.*
    /// </summary>
    public int Major { get; set; }

    /// <summary>
    /// O número do meio da versão.
    /// -   *.X.*
    /// </summary>
    public int Minor { get; set; }

    /// <summary>
    /// O último número da versão.
    /// -   *.*.X
    /// </summary>
    public int Patch { get; set; }

    /// <summary>
    /// O nome de pré-lançamento da versão, como alfa, beta, trunk, etc;
    /// </summary>
    public PreReleaseInfo PreRelease { get; set; }

    /// <summary>
    /// O número de revisão do código fonte no repositório.
    /// </summary>
    public int Revision { get; set; }

    /// <summary>
    /// Produz o nome completo da versão, na forma:
    /// -   X.X.X-sufixoX_rX
    /// Sendo:
    /// -   X
    ///         Um número qualquer.
    /// -   X.X.X
    ///         Obrigatório.
    ///         Número de versão do aplicativo.
    /// -   sufixoX
    ///         Opcional.
    ///         O nome da versão, como alfa, beta, trunk, etc.
    ///         Seguido opcionalmente de um número de revisão do sufixo.
    /// -   rX
    ///         Opcional.
    ///         O número de revisão no repositório de código fonte.
    /// </summary>
    public override string ToString()
    {
      return ToString(Suffixes.All);
    }

    /// <summary>
    /// Produz o nome completo da versão, na forma:
    /// -   X.X.X-sufixoX_rX
    /// Ou apenas a parte escolhida.
    /// Sendo:
    /// -   X
    ///         Um número qualquer.
    /// -   X.X.X
    ///         Obrigatório.
    ///         Número de versão do aplicativo.
    /// -   sufixoX
    ///         Opcional.
    ///         O nome da versão, como alfa, beta, trunk, etc.
    ///         Seguido opcionalmente de um número de revisão do sufixo.
    /// -   rX
    ///         Opcional.
    ///         O número de revisão no repositório de código fonte.
    /// </summary>
    /// <param name="parts">
    /// As partes extras de versão escolhidas.
    /// </param>
    public string ToString(Suffixes parts)
    {
      var text = string.Join(".", Major, Minor, Patch);
      if (parts.HasFlag(Suffixes.PreRelease) && PreRelease != null)
      {
        text += $"-{PreRelease}";
      }
      if (parts.HasFlag(Suffixes.Revision) && Revision > 0)
      {
        text += $"_r{Revision}";
      }
      return text;
    }

    /// <summary>
    /// Extrai informação de versão do texto indicado.
    /// 
    /// É esperado que o texto tenha a forma:
    /// -   X.X.X-sufixoX_rX
    /// Sendo:
    /// -   X
    ///         Um número qualquer.
    /// -   X.X.X
    ///         Obrigatório.
    ///         Número de versão do aplicativo.
    /// -   sufixoX
    ///         Opcional.
    ///         O nome da versão, como alfa, beta, trunk, etc.
    ///         Seguido opcionalmente de um número de revisão do sufixo.
    /// -   rX
    ///         Opcional.
    ///         O número de revisão no repositório de código fonte.
    /// </summary>
    /// <param name="version">O texto a ser analisado.</param>
    /// <returns>A informação de versão extraída.</returns>
    public static VersionInfo Parse(string version)
    {
      var instance = new VersionInfo();

      // É permitido usar o caracter # para comentar o texto.
      var lines =
        from line in version.Split('\n', '\r')
        let sentence = line.Split('#').First().Trim()
        where !string.IsNullOrEmpty(sentence)
        select sentence;
      version = string.Join("_", lines);

      // REGEX para o padrão: X.X.X-sufixo_rX
      var regex = new Regex(@"^(\d+)(?:\.(\d+))?(?:\.(\d+))?(?:-([a-zA-Z\d]+))?(?:_r(\d+))?");
      var match = regex.Match(version);
      if (match.Success)
      {
        instance.Major = int.TryParse(match.Groups[1].Value, out int major) ? major : 0;
        instance.Minor = int.TryParse(match.Groups[2].Value, out int minor) ? minor : 0;
        instance.Patch = int.TryParse(match.Groups[3].Value, out int patch) ? patch : 0;
        instance.PreRelease = match.Groups[4].Value;
        instance.Revision = int.TryParse(match.Groups[5].Value, out int rev) ? rev : 0;
      }
      return instance;
    }

    public static implicit operator VersionInfo(string version)
    {
      return Parse(version);
    }

    public static implicit operator string(VersionInfo version)
    {
      return version.ToString();
    }

    /// <summary>
    /// Opções de detalhamento da versão impressa.
    /// </summary>
    [Flags]
    public enum Suffixes
    {
      /// <summary>
      /// Nenhum sufixo adicional.
      /// </summary>
      None = 0,

      /// <summary>
      /// Acrescenta o sufixo de pré-release, se existir.
      /// </summary>
      PreRelease = 1,

      /// <summary>
      /// Acrescenta o sufixo de revisão do código fonte, se existir.
      /// </summary>
      Revision = 2,

      /// <summary>
      /// Acrescenta todos os sufixos.
      /// </summary>
      All = PreRelease | Revision
    }

    /// <summary>
    /// O nome de pré-lançamento da versão, como alfa, beta, trunk, etc;
    /// </summary>
    public class PreReleaseInfo
    {
      private string _name;
      private int? _revision;

      private PreReleaseInfo(string value)
      {
        this.Value = value.Trim();
      }

      /// <summary>
      /// O nome de pré-lançamento da versão, como alfa, beta, trunk, etc;
      /// </summary>
      public string Value { get; }

      /// <summary>
      /// O nome de revisao do pré-release.
      /// Como o nome "beta" em:
      ///   2.5.3-beta1
      /// </summary>
      public string Name => _name ?? (_name = Regex.Replace(Value, @"(.*?)(\d+)?$", "$1"));

      /// <summary>
      /// O número de revisao do pré-release.
      /// Como o número 1 em:
      ///   2.5.3-beta1
      /// </summary>
      public int Revision
      {
        get
        {
          if (_revision == null)
          {
            var part = Regex.Replace(Value, @"(.*?)(\d+)?$", "$2");
            _revision = (int.TryParse(part, out int rev)) ? rev : 0;
          }
          return _revision.Value;
        }
      }

      public override string ToString()
      {
        return Value;
      }

      public static implicit operator PreReleaseInfo(string value)
      {
        return string.IsNullOrWhiteSpace(value) ? null : new PreReleaseInfo(value);
      }

      public static implicit operator string(PreReleaseInfo value)
      {
        return value?.Value;
      }
    }
  }
}
