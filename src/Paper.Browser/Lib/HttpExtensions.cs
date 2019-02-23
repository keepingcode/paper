using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Paper.Media;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Collections;
using Toolset.Net;

namespace Paper.Browser.Lib
{
  public static class HttpExtensions
  {
    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData: (byte[])null
        , upType: MimeTypeNames.Json
        , upCharset: "UTF-8"
        , downType: MimeTypeNames.Siren
        , downCharset: "UTF-8"
      );
    }

    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      , string downType
      , string downCharset
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData: (byte[])null
        , upType: MimeTypeNames.Json
        , upCharset: "UTF-8"
        , downType: downType
        , downCharset: downCharset
      );
    }

    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      , string downType
      , Encoding downCharset
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData: (byte[])null
        , upType: MimeTypeNames.Json
        , upCharset: "UTF-8"
        , downType: downType
        , downCharset: downCharset.BodyName
      );
    }

    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      , Entity upData
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData
        , upType: MimeTypeNames.Json
        , upCharset: "UTF-8"
        , downType: MimeTypeNames.Siren
        , downCharset: "UTF-8"
      );
    }

    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      , Entity upData
      , string downType
      , string downCharset
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData
        , upType: MimeTypeNames.Json
        , upCharset: "UTF-8"
        , downType: downType
        , downCharset: downCharset
      );
    }

    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      , Entity upData
      , string downType
      , Encoding downCharset
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData
        , upType: MimeTypeNames.Json
        , upCharset: "UTF-8"
        , downType: downType
        , downCharset: downCharset.BodyName
      );
    }

    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      , byte[] upData
      , string upType
      , string upCharset
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData
        , upType
        , upCharset
        , downType: MimeTypeNames.Siren
        , downCharset: "UTF-8"
      );
    }

    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      , byte[] upData
      , string upType
      , Encoding upCharset
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData
        , upType
        , upCharset: upCharset.BodyName
        , downType: MimeTypeNames.Siren
        , downCharset: "UTF-8"
      );
    }


    public static async Task<Ret<Result>> RequestAsync(
        this Http http
      , string uri
      , string method
      , byte[] upData
      , string upType
      , Encoding upCharset
      , string downType
      , Encoding downCharset
      )
    {
      return await http.RequestAsync(
          uri
        , method
        , upData
        , upType
        , upCharset: upCharset.BodyName
        , downType: downType
        , downCharset: downCharset.BodyName
      );
    }
  }
}