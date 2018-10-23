using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolset.Collections;
using Toolset.Types;

namespace Paper.Media.Utilities.Types
{
  /// <summary>
  /// Argumentos de URI interpretados segundo o padrão de URI do Paper.
  /// 
  /// O padrão de URI do Paper suporta a representação de valores, boolianos,
  /// intervalos, vetores e objetos.
  /// 
  /// Notação:
  /// -   Valor
  ///     -   key=value
  /// -   Booliano falso:
  ///     -   key=0
  /// -   Booliano verdadeiro:
  ///     -   key
  ///     -   key=1
  /// -   Intervalo:
  ///     -   key.min=value&key.max=value
  /// -   Intervalo maior ou igual a:
  ///     -   key.min=value
  /// -   Intervalo menor ou igual a:
  ///     -   key.max=value
  /// -   Vetor
  ///     -   key[]=value&key[]=value
  /// -   Objeto
  ///     -   key.propriedade=value
  ///     -   key.propriedade.subpropriedade=value
  ///     
  /// Por exemplo, considere os argumentos de URI:
  /// 
  /// -   ?f=json&user.id=10&date.min=2018-01-01&published&sort[]=name&sort[]=date:desc
  ///     
  /// Segundo a notação do Paper estes seriam os argumentos:
  /// 
  /// -   f = "json"
  /// -   user = new UriArgs { id = 10 }
  /// -   date = new Range { min = Date(2018,01,01), max = null }
  /// -   published = true
  /// -   sort = new[] { "name", "date:desc" }
  /// 
  /// </summary>
  public class UriArgs
  {
    private readonly HashMap<Var> args;
    private readonly List<string> keys;

    public UriArgs()
    {
      this.args = new HashMap<Var>();
      this.keys = new List<string>();
    }

    /// <summary>
    /// Construtor dos argumentos de URI.
    /// </summary>
    /// <param name="queryString">
    /// A porção de argumentos da URI iniciando com uma interrogação ("?").
    /// Exemplo:
    ///     ?a=x&b[]=x&c.x=y&d
    /// </param>
    public UriArgs(string queryString)
    {
      this.args = new HashMap<Var>();
      this.keys = new List<string>();
      ParseQueryString(queryString);
    }

    public object this[int index]
    {
      get => Get(keys.ElementAtOrDefault(index));
      set
      {
        var key = keys.ElementAtOrDefault(index);
        if (key == null)
          throw new IndexOutOfRangeException($"Índice: {index}");

        Set(key, value);
      }
    }

    public object this[string key]
    {
      get => Get(key);
      set => Set(key, value);
    }

    private object Get(string key)
    {
      if (key == null)
        return null;
      
      var var = args[key];
      return var?.Value;
    }

    private void Set(string key, object value)
    {
      if (key == null)
        throw new NullReferenceException("A chave do argumento não deve ser nula.");

      if (!keys.Contains(key))
      {
        keys.Add(key);
      }

      var var = new Var(value);
      args[key] = var;
    }

    public string ToUriComponent()
    {
      var args = EnumerateKeyPairs(this);
      var queryString = "?" + string.Join("&", args);
      return queryString;
    }

    private IEnumerable<string> EnumerateKeyPairs(UriArgs uriArgs)
    {
      foreach (var key in uriArgs.keys)
      {
        var arg = uriArgs.args[key];
        if (arg.IsNull)
          continue;

        switch (arg.Kind)
        {
          default:
            {
              yield return $"{key}={MakeString(arg.Value)}";
              break;
            }
          case VarKind.Range:
            {
              if (arg.Range.Min != null)
              {
                var value = MakeString(arg.Range.Min);
                yield return $"{key}.min={value}";
              }
              if (arg.Range.Max != null)
              {
                var value = MakeString(arg.Range.Max);
                yield return $"{key}.max={value}";
              }
              break;
            }
          case VarKind.List:
            {
              foreach (var item in arg.List)
              {
                var value = MakeString(item);
                if (!string.IsNullOrEmpty(value))
                {
                  yield return $"{key}[]={value}";
                }
              }
              break;
            }
            //case VarKinds.Graph:
            //  {
            //    if (arg.Value is UriArgs subArgs)
            //    {
            //      var keyPairs = EnumerateKeyPairs(subArgs);
            //      foreach (var keyPair in keyPairs)
            //      {
            //        yield return $"{key}.{keyPair}";
            //      }
            //      break;
            //    }
            //    else
            //    {
            //      throw new NotImplementedException("Argumento do tipo objeto não é suportado.");
            //    }
            //  }
            //case VarKinds.Map:
            //  throw new NotImplementedException("Argumento do tipo dicionário não é suportado.");
        }
      }
    }

    private void ParseQueryString(string queryString)
    {
      if (queryString.StartsWith("?"))
      {
        queryString = queryString.Substring(1);
      }

      var args =
        from pair in queryString.Split('&')
        let tokens = pair.Split('=')
        let key = tokens.First()
        let value = string.Join("=", tokens.Skip(1))
        select new { key, value };

      foreach (var arg in args)
      {
        Set(arg.key, arg.value);
      }
    }

    #region Métodos auxiliares

    /// <summary>
    /// Converte o valor para o formato de string para URI segundo o padrão do Paper.
    /// </summary>
    /// <param name="value">O valor a ser convertido.</param>
    /// <returns>A string obtida.</returns>
    public static string MakeString(object value)
    {
      if (value is null)
        return "";

      if (value is Date date)
      {
        value = date.Value;
      }
      if (value is Time time)
      {
        value = time.Value;
      }

      if (value is bool bit)
      {
        return bit ? "1" : "0";
      }

      if (value is DateTime dateTime)
      {
        if (Date.IsDateOnly(dateTime))
        {
          return dateTime.ToString("yyyy-MM-dd");
        }
        if (Time.IsTimeOnly(dateTime))
        {
          return dateTime.ToString("HH:mm:ss");
        }
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
      }

      return value.ToString();
    }

    #endregion
  }
}