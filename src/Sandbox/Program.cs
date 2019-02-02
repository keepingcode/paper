using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Paper.Media;
using Paper.Media.Design;
using Paper.Media.Serialization;
using Toolset;
using Toolset.Collections;
using Toolset.Data;
using Toolset.Reflection;
using Toolset.Sequel;
using Toolset.Serialization;
using Toolset.Serialization.Graph;
using Toolset.Serialization.Json;
using Toolset.Serialization.Xml;
using Toolset.Xml;

namespace Sandbox
{
  class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      try
      {
        #region
        var json = @"
          {
            ""data"": {
              ""@class"": ""Users"",
              ""total"": 4
            },
            ""rows"": [
              {
                ""@class"": ""User"",
                ""id"": 1,
                ""name"": ""One""
              },
              {
                ""@class"": ""User"",
                ""id"": 2,
                ""name"": ""Two""
              },
              {
                ""@class"": ""Profile"",
                ""id"": 1,
                ""name"": ""1st""
              },
              {
                ""@class"": ""Profile"",
                ""id"": 2,
                ""name"": ""2nd""
              }
            ]
          }
        ";
        #endregion

        //var xml = @"
        //  <Payload>
        //    <Data>
        //      <Id>10</Id>
        //      <Name>Ten</Name>
        //    </Data>
        //    <Numbers IsArray=""true"">
        //      <Data>
        //        <Id>10</Id>
        //        <Name>Ten</Name>
        //      </Data>
        //      <Val>1</Val>
        //      <Val>2</Val>
        //      <Int IsArray=""true"">
        //        <Val>3</Val>
        //        <Val>4</Val>
        //      </Int>
        //      <String>
        //        <Val>Sample</Val>
        //      </String>
        //      <Boo></Boo>
        //      <Data>
        //        <Id>10</Id>
        //        <Name>Ten</Name>
        //      </Data>
        //    </Numbers>
        //    <Data>
        //      <Id>10</Id>
        //      <Name>Ten</Name>
        //    </Data>
        //  </Payload>
        //";

        var xml = @"
        <Payload>
          <Data Class=""Users"">
            <Total>4</Total>
          </Data>
          <Rows IsArray=""true"">
            <Row Class=""User"">
              <Id>1</Id>
              <Name>One</Name>
            </Row>
            <Row Class=""User"">
              <Id>2</Id>
              <Name>Two</Name>
            </Row>
            <Row Class=""Profile"">
              <Id>1</Id>
              <Name>1st</Name>
            </Row>
            <Row Class=""Profile"">
              <Id>2</Id>
              <Name>2nd</Name>
            </Row>
          </Rows>
        </Payload>
        ";

        var buffer = new StringWriter();
        var settings = new XmlSerializationSettings();

        using (var reader = new XmlDocumentReader(XmlReader.Create(new StringReader(xml)), settings))
        using (var writer = new GraphWriter<Payload>())
        {
          reader.CopyTo(writer);

          Debug.WriteLine(writer.Graphs.FirstOrDefault().ToEntity().ToXElement());
        }
        
      }
      catch (Exception ex)
      {
        ex.Trace();
      }
    }
  }
}