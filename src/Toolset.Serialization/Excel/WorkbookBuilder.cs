using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.ObjectModel;
using System.IO.Compression;

namespace Toolset.Serialization.Excel
{
  public sealed class WorkbookBuilder
  {
    private const string ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

    private readonly XmlWriterSettings xmlSettings;
    private readonly ExcelSerializationSettings settings;
    private readonly List<Sheet> sheets;

    public WorkbookBuilder()
      : this(null)
    {
      // nada a fazer aqui. use o outro construtor.
    }

    public WorkbookBuilder(ExcelSerializationSettings settings)
    {
      this.sheets = new List<Sheet>();
      this.settings = settings ?? new ExcelSerializationSettings();
      this.xmlSettings = new XmlWriterSettings
      {
        Indent = this.settings.Indent,
        IndentChars = this.settings.IndentChars,
        Encoding = Encoding.UTF8,
        OmitXmlDeclaration = false
      };
    }

    public ReadOnlyCollection<Sheet> Sheets
    {
      get { return readonlySheets ?? (readonlySheets = new ReadOnlyCollection<Sheet>(sheets)); }
    }
    private ReadOnlyCollection<Sheet> readonlySheets;

    public SheetBuilder CreateSheet()
    {
      return CreateSheet(null);
    }

    public SheetBuilder CreateSheet(string sheetLabel)
    {
      var sheetIndex = (sheets.Count + 1);

      var id = sheetIndex;
      var name = "sheet" + id;
      var zipEntryName = "worksheets/" + name + ".xml";
      var tempFilename = Path.GetTempFileName();
      
      var sheet = new Sheet
      {
        Id = id,
        Name = name,
        Label = sheetLabel ?? ("Planilha" + sheetIndex),
        ZipEntryName = zipEntryName,
        TempFilename = tempFilename
      };

      sheets.Add(sheet);

      return new SheetBuilder(sheet, xmlSettings);
    }

    public void Write(Stream output)
    {
      using (var zip = new ZipArchive(output, ZipArchiveMode.Create, leaveOpen: true))
      {
        using (var stream = zip.CreateEntry("_rels/.rels").Open())
        {
          var xml = XDocument.Parse(@"
            <Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
            	<Relationship Id=""workbook"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"" Target=""xl/workbook.xml""/>
            	<Relationship Id=""app"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties"" Target=""docProps/app.xml""/>
            	<Relationship Id=""core"" Type=""http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties"" Target=""docProps/core.xml""/>
            </Relationships>
          ");
          var writer = XmlWriter.Create(stream, xmlSettings);
          xml.WriteTo(writer);
          writer.Flush();
          stream.Flush();
        }

        using (var stream = zip.CreateEntry("docProps/app.xml").Open())
        {
          var xml = XDocument.Parse($@"<?xml version=""1.0"" standalone=""yes""?>
            <Properties xmlns=""http://schemas.openxmlformats.org/officeDocument/2006/extended-properties"" xmlns:vt=""http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes""/>
          ");
          var writer = XmlWriter.Create(stream, xmlSettings);
          xml.WriteTo(writer);
          writer.Flush();
          stream.Flush();
        }

        using (var stream = zip.CreateEntry("docProps/core.xml").Open())
        {
          var xml = XDocument.Parse($@"<?xml version=""1.0"" standalone=""yes""?>
            <cp:coreProperties xmlns:cp=""http://schemas.openxmlformats.org/package/2006/metadata/core-properties"" xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:dcterms=""http://purl.org/dc/terms/"" xmlns:dcmitype=""http://purl.org/dc/dcmitype/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""/>
          ");
          var writer = XmlWriter.Create(stream, xmlSettings);
          xml.WriteTo(writer);
          writer.Flush();
          stream.Flush();
        }

        using (var stream = zip.CreateEntry("xl/_rels/workbook.xml.rels").Open())
        {
          var xml = XDocument.Parse($@"
            <Relationships xmlns=""http://schemas.openxmlformats.org/package/2006/relationships"">
            	<Relationship Id=""styles"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles"" Target=""styles.xml""/>
              {Join(sheets, x => $@"<Relationship Id=""{x.Name}"" Type=""http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"" Target=""{x.ZipEntryName}""/>")}
            </Relationships>
          ");
          var writer = XmlWriter.Create(stream, xmlSettings);
          xml.WriteTo(writer);
          writer.Flush();
          stream.Flush();
        }

        foreach (var sheet in sheets)
        {
          var zipEntry = sheet.ZipEntryName;
          var filename = sheet.TempFilename;
          using (var stream = zip.CreateEntry($"xl/{zipEntry}").Open())
          {
            using (var file = File.OpenRead(filename))
            {
              file.CopyTo(stream);
            }
            stream.Flush();
          }
        }

        using (var stream = zip.CreateEntry("xl/styles.xml").Open())
        {
          var dateFormat = settings.DateTimeFormat;
          var xml = XDocument.Parse($@"
            <styleSheet xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"">
              <numFmts count=""2"">
                <numFmt numFmtId=""164"" formatCode=""GENERAL"" />
                <numFmt numFmtId=""165"" formatCode=""{dateFormat}"" />
              </numFmts>
              <cellStyleXfs count=""2"">
                <xf numFmtId=""164""/>
              </cellStyleXfs>
              <cellXfs count=""2"">
                <xf numFmtId=""164"" xfId=""0""/>
                <xf numFmtId=""165"" xfId=""0""/>
              </cellXfs>
            </styleSheet>
          ");
          var writer = XmlWriter.Create(stream, xmlSettings);
          xml.WriteTo(writer);
          writer.Flush();
          stream.Flush();
        }

        using (var stream = zip.CreateEntry("xl/workbook.xml").Open())
        {
          var xml = XDocument.Parse($@"
            <workbook xmlns=""http://schemas.openxmlformats.org/spreadsheetml/2006/main"" xmlns:r=""http://schemas.openxmlformats.org/officeDocument/2006/relationships"">
            	<sheets>
                {Join(sheets, x => $@"<sheet name=""{x.Label}"" sheetId=""{x.Id}"" r:id=""{x.Name}""/>")}
            	</sheets>
            </workbook>
          ");
          var writer = XmlWriter.Create(stream, xmlSettings);
          xml.WriteTo(writer);
          writer.Flush();
          stream.Flush();
        }

        using (var stream = zip.CreateEntry("[Content_Types].xml").Open())
        {
          var xml = XDocument.Parse($@"
            <Types xmlns=""http://schemas.openxmlformats.org/package/2006/content-types"">
            	<Default Extension=""rels"" ContentType=""application/vnd.openxmlformats-package.relationships+xml""/>
            	<Default Extension=""xml"" ContentType=""application/xml""/>
            	<Override PartName=""/docProps/app.xml"" ContentType=""application/vnd.openxmlformats-officedocument.extended-properties+xml""/>
            	<Override PartName=""/docProps/core.xml"" ContentType=""application/vnd.openxmlformats-package.core-properties+xml""/>
            	<Override PartName=""/xl/workbook.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml""/>
            	<Override PartName=""/xl/styles.xml"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml""/>
              {Join(sheets, x => $@"<Override PartName=""/xl/{x.ZipEntryName}"" ContentType=""application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml""/>")}
            </Types>
          ");
          var writer = XmlWriter.Create(stream, xmlSettings);
          xml.WriteTo(writer);
          writer.Flush();
          stream.Flush();
        }
      }
    }

    private string Join<T>(List<T> list, Func<T, string> predicate)
    {
      return string.Join("", list.Select(predicate));
    }
  }
}