using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Toolset;
using Toolset.Collections;

namespace Paper.Media.Rendering
{
  public class AcceptHeader
  {
    public AcceptHeader(IHeaders headers, IQueryArgs args)
    {
      ParseAccept(headers, args);
    }

    /// <summary>
    /// The Accept request HTTP header advertises which content types, expressed as MIME types, the
    /// client is able to understand. Using content negotiation, the server then selects one of the
    /// proposals, uses it and informs the client of its choice with the Content-Type response header.
    /// Browsers set adequate values for this header depending on the context where the request is
    /// done: when fetching a CSS stylesheet a different value is set for the request than when
    /// fetching an image, video or a script.
    /// 
    /// Syntax
    /// 
    /// -   Accept: <MIME_type>/<MIME_subtype>
    /// -   Accept: <MIME_type>/*
    /// -   Accept: */*
    /// 
    /// Multiple types, weighted with the quality value syntax:
    /// 
    /// -   Accept: text/html, application/xhtml+xml, application/xml;q=0.9, image/webp, */*;q=0.8
    /// 
    /// Directives
    /// 
    /// -   <MIME_type>/<MIME_subtype>
    ///     A single, precise MIME type, like text/html.
    /// -   <MIME_type>/*
    ///     A MIME type, but without any subtype.
    ///     image/* will match image/png, image/svg, image/gif and any other image types.
    /// -   */*
    ///     Any MIME type
    /// -   ;q= (q-factor weighting)
    ///     Any value used is placed in an order of preference expressed using relative quality
    ///     value called the weight.
    ///     
    /// Examples
    /// 
    /// -   Accept: text/html
    /// -   Accept: image/*
    /// -   Accept: text/html, application/xhtml+xml, application/xml;q=0.9, */*;q=0.8
    /// </summary>
    public ICollection<string> MimeTypes { get; set; }

    /// <summary>
    /// The Accept-Charset request HTTP header advertises which character set the client is
    /// able to understand. Using content negotiation, the server then selects one of the
    /// proposals, uses it and informs the client of its choice within the Content-Type
    /// response header. Browsers usually don't set this header as the default value for
    /// each content type is usually correct and transmitting it would allow easier
    /// fingerprinting.
    /// 
    /// If the server cannot serve any matching character set, it can theoretically send back
    /// a 406 (Not Acceptable) error code. But, for a better user experience, this is rarely
    /// done and the more common way is to ignore the Accept-Charset header in this case.
    /// 
    /// In early versions of HTTP/1.1, a default charset (ISO-8859-1) was defined. This is no
    /// more the case and now each content type may have its own default.
    /// 
    /// Syntax
    /// 
    /// -   Accept-Charset: <charset>
    /// 
    /// Multiple types, weighted with the quality value syntax:
    /// 
    /// -   Accept-Charset: utf-8, iso-8859-1;q=0.5
    /// 
    /// Directives
    /// 
    /// -   <charset>
    ///     A character set like utf-8 or iso-8859-15.
    /// -   *
    ///     Any charset not mentioned elsewhere in the header; '*' being used as a wildcard.
    /// -   ;q= (q-factor weighting)
    ///     Any value is placed in an order of preference expressed using a relative quality
    ///     value called the weight.
    /// 
    /// Examples
    /// 
    /// -   Accept-Charset: iso-8859-1
    /// -   Accept-Charset: utf-8, iso-8859-1;q=0.5
    /// -   Accept-Charset: utf-8, iso-8859-1;q=0.5, *;q=0.1
    /// </summary>
    public ICollection<Encoding> Encodings { get; set; }

    /// <summary>
    /// The Accept-Encoding request HTTP header advertises which content encoding, usually a
    /// compression algorithm, the client is able to understand. Using content negotiation,
    /// the server selects one of the proposals, uses it and informs the client of its choice
    /// with the Content-Encoding response header.
    /// 
    /// Even if both the client and the server supports the same compression algorithms, the
    /// server may choose not to compress the body of a response, if the identity value is
    /// also acceptable. Two common cases lead to this:
    /// 
    /// The data to be sent is already compressed and a second compression won't lead to
    /// smaller data to be transmitted. This may be the case with some image formats;
    /// The server is overloaded and cannot afford the computational overhead induced by the
    /// compression requirement. Typically, Microsoft recommends not to compress if a server
    /// uses more than 80% of its computational power.
    /// As long as the identity value, meaning no encoding, is not explicitly forbidden, by
    /// an identity;q=0 or a *;q=0 without another explicitly set value for identity, the
    /// server must never send back a 406 Not Acceptable error.
    /// 
    /// Notes:
    /// 
    /// -   An IANA registry maintains a complete list of official content encoding:
    ///     http://www.iana.org/assignments/http-parameters/http-parameters.xml#http-parameters-1
    /// -   Two others content encoding, bzip and bzip2, are sometimes used, though not standard.
    ///     They implement the algorithm used by these two UNIX programs. Note that the first
    ///     one was discontinued due to patent licensing problems.
    ///     
    /// Syntax
    /// 
    /// -   Accept-Encoding: gzip
    /// -   Accept-Encoding: compress
    /// -   Accept-Encoding: deflate
    /// -   Accept-Encoding: br
    /// -   Accept-Encoding: identity
    /// -   Accept-Encoding: *
    /// 
    /// Multiple algorithms, weighted with the quality value syntax:
    /// 
    /// -   Accept-Encoding: deflate, gzip;q=1.0, *;q=0.5
    /// 
    /// Directives
    /// 
    /// -   gzip
    ///     A compression format using the Lempel-Ziv coding (LZ77), with a 32-bit CRC.
    /// -   compress
    ///     A compression format using the Lempel-Ziv-Welch (LZW) algorithm.
    /// -   deflate
    ///     A compression format using the zlib structure, with the deflate compression algorithm.
    /// -   br
    ///     A compression format using the Brotli algorithm.
    /// -   identity
    ///     Indicates the identity function (i.e. no compression, nor modification). This value is always considered as acceptable, even if not present.
    /// -   *
    ///     Matches any content encoding not already listed in the header. This is the default value if the header is not present. It doesn't mean that any algorithm is supported; merely that no preference is expressed.
    /// -   ;q= (qvalues weighting)
    ///     Any value is placed in an order of preference expressed using a relative quality value called weight.
    ///     
    /// Examples
    /// 
    /// -   Accept-Encoding: gzip
    /// -   Accept-Encoding: gzip, compress, br
    /// -   Accept-Encoding: br;q=1.0, gzip;q=0.8, *;q=0.1
    /// </summary>
    public ICollection<string> Compressions { get; set; }

    public string BestMimeType
      => SelectBestMatch(MimeTypes,
        new[] { "application/vnd.siren+json", "application/json", "text/json", "application/xml", "text/xml" }
      ) ?? "text/json";

    public Encoding BestEncoding
      => SelectBestMatch(Encodings, new[] { Encoding.UTF8 })
      ?? Encodings.FirstOrDefault()
      ?? Encoding.UTF8;

    public string BestCompression
      => SelectBestMatch(Compressions, new[] { "*" })
      ?? Compressions.FirstOrDefault()
      ?? "";

    public string SelectBestMatch(ICollection<string> terms, string[] searchTerms)
    {
      return terms.FirstOrDefault(term => term.EqualsAnyIgnoreCase(searchTerms));
    }

    public Encoding SelectBestMatch(ICollection<Encoding> terms, Encoding[] searchTerms)
    {
      return terms.FirstOrDefault(term => searchTerms.Contains(term));
    }

    private void ParseAccept(IHeaders headers, IQueryArgs args)
    {
      var accept = headers[HeaderNames.Accept] ?? "";
      var acceptCharset = headers[HeaderNames.AcceptCharset] ?? "";
      var acceptEncoding = headers[HeaderNames.AcceptEncoding] ?? "";

      var mimeTypes = (
        from token in accept.Split(',').NonNullOrWhitespace()
        let part = token.Split(';').NonNullOrWhitespace()
        let type = part.First().Trim()
        let weight = part.Skip(1).FirstOrDefault()?.Trim().Replace("q=", "")
        orderby weight descending
        select type
      ).ToList();

      var encodings = (
        from token in acceptCharset.Split(',').NonNullOrWhitespace()
        let part = token.Split(';').NonNullOrWhitespace()
        let mime = part.First().Trim()
        let weight = part.Skip(1).FirstOrDefault()?.Trim().Replace("q=", "")
        orderby weight descending
        select GetEncoding(mime)
      ).ToList();

      var compressions = (
        from token in acceptEncoding.Split(',').NonNullOrWhitespace()
        let part = token.Split(';').NonNullOrWhitespace()
        let type = part.First().Trim()
        let weight = part.Skip(1).FirstOrDefault()?.Trim().Replace("q=", "")
        orderby weight descending
        select type
      ).ToList();

      var format = args["f"];
      if (format?.IsValue == true)
      {
        var modifier = format.Value.ToString();

        var tokens = modifier.Split(';');
        var type = tokens.First();
        var charset = tokens.Skip(1).FirstOrDefault()?.Split('=').Last();

        if (type.ContainsIgnoreCase("json"))
        {
          mimeTypes.Insert(0, "application/vnd.siren+json");
        }
        else if (type.ContainsIgnoreCase("xml"))
        {
          mimeTypes.Insert(0, "application/xml");
        }

        if (type.ContainsIgnoreCase(".zip"))
        {
          compressions.Insert(0, "deflate");
        }
        else if (type.ContainsIgnoreCase(".gz"))
        {
          compressions.Insert(0, "gzip");
        }

        if (charset != null)
        {
          encodings.Add(GetEncoding(charset));
        }
      }

      this.MimeTypes = mimeTypes;
      this.Encodings = encodings;
      this.Compressions = compressions;
    }

    private Encoding GetEncoding(string mime)
    {
      try
      {
        return (mime == "*") ? Encoding.UTF8 : Encoding.GetEncoding(mime);
      }
      catch
      {
        return Encoding.UTF8;
      }
    }
  }
}